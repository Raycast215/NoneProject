using System;
using NoneProject.Interface;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Component.Move.Pattern
{
    // Scripted by Raycast
    // 2025.01.15
    // 랜덤으로 위치를 잡아 이동하는 패턴 클래스입니다.
    public class MoveRandomVector : IMovable
    {
        public event Action<Vector2> OnMoveVecUpdated;
        public event Action<Vector2> OnDirectionUpdated;

        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _checkDistance;
        private readonly float _moveVecRange;
        private Vector2 _targetPosition;
        private bool _isAutoMove;
        
        public MoveRandomVector(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
            _checkDistance = GameManager.Instance.Const.CheckDistance;
            _moveVecRange = GameManager.Instance.Const.RandomMoveVecRange;
            _targetPosition = Vector2.zero;
            _isAutoMove = false;
        }
        
        public void Move(float moveSpeed, Vector2 moveVec = new Vector2())
        {
            var position = _rigidbody2D.transform.position;
            // 이동할 방향 벡터를 구함.
            var normalVec = ((Vector3)_targetPosition - position).normalized;
            
            // 이동 방향에 따른 이벤트 실행.
            OnDirectionUpdated?.Invoke(normalVec);
            
            if (_isAutoMove)
            {
                // 목표 위치까지 거리 체크.
                if (Util.CheckDistance(_targetPosition, _rigidbody2D.transform.position, _checkDistance))
                {
                    _isAutoMove = false;
                    return;
                }
                
                // 움직일 거리 계산.
                var moveDir = normalVec * (moveSpeed * Time.deltaTime);
                // 실제 이동할 위치값.
                var movePos = position + moveDir;
            
                // 이동 실행.
                _rigidbody2D.MovePosition(movePos);
                
                // 이동 벡터를 업데이트하는 이벤트 실행. 
                OnMoveVecUpdated?.Invoke(normalVec);
                return;
            }
            
            // 다음으로 이동할 위치를 구함.
            _targetPosition = Util.GetRandomDirVec(_rigidbody2D.transform.position, _moveVecRange, _moveVecRange);
            _isAutoMove = true;
        }
    }
}