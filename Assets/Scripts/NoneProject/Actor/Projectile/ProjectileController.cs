using System.Collections.Generic;
using NoneProject.Actor.Component.Move;
using NoneProject.Actor.Component.Rotate;
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
        private readonly Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private RotateAngle _rotate;
        private Transform _caster;
        private IMovable _mover;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;
            
            if (gameObject.activeInHierarchy is false)
                return;
            
            _mover?.Move(MoveSpeed);
        }

        public void Set(MovePattern movePattern, Vector2 startPos, Transform caster)
        {
            _caster = caster;

            SetMovePattern(movePattern);
            SetPosition(startPos);
            
            _rotate.SetAngle(_caster.position);
            _mover.SetMoveVec(GetMoveVector());
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
        
        private Vector2 GetMoveVector()
        {
            // 목표 대상이 없는 경우 시전자 기준으로 방향 벡터 반환
            return (transform.position - _caster.position).normalized;
        }

#region Override Methods

        protected override void Initialize()
        {
            _rotate = new RotateAngle(transform);
            MoveSpeed = 3.0f;
            IsInitialized = true;
        }

#endregion
    }
}