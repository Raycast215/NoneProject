using System.Collections.Generic;
using NoneProject.Actor.BT;
using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move;
using NoneProject.Common;
using NoneProject.Interface;
using NoneProject.Manager;
using UnityEngine;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy를 관리하는 클래스입니다.
    public class EnemyController : ActorBase
    {
        private readonly Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private ModelController _modelController;
        private IMovable _mover;
        private ActorBase _target;
        private NodeRunner _nodeRunner;
        private EnemyStat _stat;

        private void FixedUpdate()
        {
            UpdateEnemy();
        }

        public void UpdateEnemy()
        {
            if (IsInitialized is false)
                return;
            
            if (gameObject.activeInHierarchy is false)
                return;
            
            if (_stat is null)
                return;
            
            _nodeRunner?.OperateNode();
        }

        public void SetData(string enemyID)
        {
            var statData = StatDataManager.Instance.GetEnemyStatData(enemyID);
            
            _stat = new EnemyStat(statData);
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

            if (Vector3.Distance(transform.position, playerPos) > _stat.DetectRange)
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

            if (distance < _stat.AttackRange)
                return NodeState.Success;

            SetScaleDirection(moveVec);
            _modelController.SetAnimationState(ActorState.Run);
            _mover?.SetMoveVec(moveVec);
            _mover?.Move(_stat.MoveSpeed);
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

#region Override Methods

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            _nodeRunner = new NodeRunner(InitializeNode());
            
            IsInitialized = true;
        }

#endregion
    }
}