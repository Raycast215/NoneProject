using System;
using System.Collections.Generic;
using NoneProject.Actor.Component.Move;
using NoneProject.Actor.Component.Rotate;
using NoneProject.Actor.Data;
using NoneProject.Common;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Projectile
{
    // Scripted by Raycast
    // 2025.01.17
    // Projectile을 관리하는 클래스입니다.
    public class ProjectileController : ActorBase
    {
        public event Func<string, ProjectileStatData> OnStatUpdated;
        public event Action OnReleased;
        
        private readonly Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private ProjectileStatController _statController;
        private RotateAngle _rotate;
        private IMovable _mover;
        private Vector2 _moveVec;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;
            
            if (gameObject.activeInHierarchy is false)
                return;
            
            _mover?.Move(_statController.MoveSpeed);
        }

        public void ClearEvent()
        {
            OnStatUpdated = null;
            OnReleased = null;
        }
        
        public void Set(string projectileID, MovePattern movePattern, Vector2 startPos, Vector2 moveVec, Transform caster = null)
        {
            var stat = OnStatUpdated?.Invoke(projectileID);

            _moveVec = moveVec;
            _statController = new ProjectileStatController(stat);

            SetMovePattern(movePattern);
            SetPosition(startPos);
            
            _rotate.SetDirection(_moveVec);
            _mover.SetMoveVec(_moveVec);
        }
        
        private void SetMovePattern(MovePattern toPattern)
        {
            // 이전에 사용한 패턴이 있는 경우.
            if (_movePatternDic.TryGetValue(toPattern, out var pattern))
            {
                _mover = pattern;
                return;
            }

            switch (toPattern)
            {
                case MovePattern.Forward:
                    _mover = new MoveForward(Rigidbody2D);
                    break;
            }
            
            _movePatternDic.Add(toPattern, _mover);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") is false) 
                return;
            
            if (other.TryGetComponent(out IHit hit))
            {
                hit.Hit(_statController.Damage);
            }
            
            if (other.TryGetComponent(out IKnockBack knockBack))
            {
                knockBack.KnockBack(_statController.KnockBackPower, _moveVec);
            }
            
            OnReleased?.Invoke();
        }

        #region Override Methods

        protected override void Initialize()
        {
            _rotate = new RotateAngle(transform);
            
            IsInitialized = true;
        }

#endregion
    }
}