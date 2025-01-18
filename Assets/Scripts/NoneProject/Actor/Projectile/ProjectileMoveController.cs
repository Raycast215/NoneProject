using System;
using System.Collections.Generic;
using NoneProject.Actor.Component.Move;
using NoneProject.Actor.Component.Move.Pattern;
using NoneProject.Actor.Component.Rotate;
using NoneProject.Common;
using NoneProject.Interface;
using UnityEngine;

namespace NoneProject.Actor.Projectile
{
    public class ProjectileMoveController : MoveController
    {
        private readonly MoveForward _move;
        private readonly RotateAngle _rotate;
        private Transform _targetTransform;
        private Vector2 _casterPos;

        private Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private IMovable _selectMover;
        
        public ProjectileMoveController(Rigidbody2D rigidbody2D)
        {
            Rigidbody = rigidbody2D;
            _move = new MoveForward(Rigidbody);
            _rotate = new RotateAngle(Rigidbody.transform);
            
            _movePatternDic.Add(MovePattern.Forward, new MoveForward(Rigidbody));
        }

        public void Set(MovePattern pattern, Transform caster, Transform target = null)
        {
            // 1, 단일 방향으로 진행.
            // 2. 타겟의 방향으로 진행.
            // 3. 타겟의 위치를 추적하며 진행.
        }
        
        public void Set(Vector2 casterPos, Transform targetTransform = null)
        {
            _casterPos = casterPos;
            _targetTransform = targetTransform;
            
            _rotate.SetAngle(_casterPos);
        }
        
        private Vector2 GetMoveVector()
        {
            if (_targetTransform)
            {
                // 목표 대상이 있는 경우 목표 위치의 방향 벡터 반환.
                return (_targetTransform.position - Rigidbody.transform.position).normalized;
            }

            // 목표 대상이 없는 경우 시전자 기준으로 방향 벡터 반환
            return ((Vector2)Rigidbody.transform.position - _casterPos).normalized;
        }

#region Override Methods
        
        public override void Move(float moveSpeed, Vector2 moveVec)
        {
            _move.Move(moveSpeed, GetMoveVector());
        }

        public override void Subscribe()
        {
          
        }
        
#endregion
    }
}