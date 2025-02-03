using System;
using System.Collections.Generic;
using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move;
using NoneProject.Actor.Data;
using NoneProject.Common;
using NoneProject.Interface;
using Template.BehaviourTree;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy를 관리하는 클래스입니다.
    public class EnemyController : ActorBase, IHit, IKnockBack
    {
        public event Func<string, EnemyStatData> OnStatUpdated;
        public event Action OnDied;
        
        private readonly Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private ModelController _modelController;
        private IMovable _mover;
        private ActorBase _target;
        private NodeRunner _nodeRunner;
        private EnemyStatController _statController;
        private SortingGroup _sortingGroup;
        private int _layerIndex;
        
        public void UpdateEnemy()
        {
            if (IsInitialized is false)
                return;
            
            if (gameObject.activeInHierarchy is false)
                return;
            
            if (_statController is null)
                return;
            
            _nodeRunner?.OperateNode();
        }

        public void ClearEvent()
        {
            OnStatUpdated = null;
            OnDied = null;
        }
        
        public void SetData(string enemyID)
        {
            var statData = OnStatUpdated?.Invoke(enemyID);
            
            _statController = new EnemyStatController(statData);
            _sortingGroup.sortingOrder = _layerIndex;
        }
        
        public void SetPattern(MovePattern toPattern)
        {
            // 이전에 사용한 패턴이 있는 경우.
            if (_movePatternDic.TryGetValue(toPattern, out var pattern))
            {
                _mover = pattern;
                return;
            }

            switch (toPattern)
            {
                case MovePattern.Random:
                    //_mover = new MoveRandomVector(Rigidbody2D);
                    _mover = new MoveForward(Rigidbody2D);
                    break;
            }
            
            _movePatternDic.Add(toPattern, _mover);
            //_mover.CompleteMove(_ => _modelController.SetAnimationState(ActorState.Run));
            _mover.CompleteMove(SetScaleDirection);
        }

        private NodeState CheckDetectPlayer()
        {
            var playerPos = Manager.PlayerManager.Instance.Player.transform.position;

            if (Vector3.Distance(transform.position, playerPos) > _statController.DetectRange)
            {
                _target = null;
                _modelController.SetAnimationState(ActorState.Idle);
                return NodeState.Running;
            }
            
            _target = Manager.PlayerManager.Instance.Player;
            return NodeState.Success;
        }

        private NodeState MoveToTarget()
        {
            var curPos = transform.position;
            var targetPos = _target.transform.position;
            var moveVec = (targetPos - curPos).normalized;
            var distance = Vector3.Distance(curPos, targetPos);

            if (distance < _statController.AttackRange)
                return NodeState.Success;

            SetScaleDirection(moveVec);
            _modelController.SetAnimationState(ActorState.Run);
            _mover?.SetMoveVec(moveVec);
            _mover?.Move(_statController.MoveSpeed);
            return NodeState.Running;
        }

        private NodeState Attack()
        {
            if (_target)
            {
                _modelController.SetAnimationState(ActorState.Attack_Normal);
                return NodeState.Running;
            }
            
            return NodeState.Failure;
        }
        
        private INode InitializeNode()
        {
            var nodeList = new List<INode>()
            {
                new ActionNode(CheckDetectPlayer),
                new ActionNode(MoveToTarget),
                new ActionNode(Attack)
            };
            
            return new SequenceNode(nodeList);
        }
        
        public void Hit(float damage)
        {
            if (_statController is null)
                return;
            
            _statController.SetCurrentHp(-damage);

            if (_statController.CurrentHp <= 0.0f)
            {
                Debug.Log("Dead");
                OnDied?.Invoke();
            }
        }
        
        public void KnockBack(float power, Vector2 casterDirection)
        {
            if (_statController is null)
                return;
            
            var randomValue = Random.Range(0.0f, 1.0f);
            var isKnockBack = randomValue <= _statController.KnockBackRate;
            
            Debug.Log($"value : {randomValue} / rate : { _statController.KnockBackRate} / {isKnockBack}");

            if (isKnockBack is false) 
                return;
            
            var dir = power * casterDirection;
                
            Rigidbody2D.transform.Translate(dir);
        }

#region Override Methods

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            _nodeRunner = new NodeRunner(InitializeNode());
            _sortingGroup = transform.GetComponentInChildren<SortingGroup>();
            _layerIndex = GameManager.Instance.Const.EnemyLayerIndex;

            IsInitialized = true;
        }

#endregion
    }
}