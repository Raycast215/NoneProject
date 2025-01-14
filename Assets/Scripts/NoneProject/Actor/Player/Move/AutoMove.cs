using System;
using NoneProject.Interface;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Player.Move
{
    // Scripted by Raycast
    // 2025.01.15
    // 랜덤으로 이동할 위치를 잡아주고, 자동으로 이동하기 위한 클래스입니다.
    public class AutoMove : IMovable
    {
        public event Action<Vector2> OnMoveVecUpdated;

        private readonly Transform _transform;
        private readonly float _checkDistance;
        private readonly float _autoMoveVecOffset;
        private Vector2 _autoTargetPosition;
        private bool _isAutoMove;
        
        public AutoMove(Transform transform)
        {
            _transform = transform;
            _checkDistance = GameManager.Instance.Const.CheckDistance;
            _autoMoveVecOffset = GameManager.Instance.Const.AutoMoveVecOffset;
            _autoTargetPosition = Vector2.zero;
            _isAutoMove = false;
        }

        public void Move(Vector2 moveVec = new Vector2())
        {
            // Auto가 비활성화 된 경우 예외.
            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;

            if (_isAutoMove)
            {
                // 목표 위치까지 거리 체크.
                if (Util.CheckDistance(_autoTargetPosition, _transform.position, _checkDistance))
                {
                    _isAutoMove = false;
                    return;
                }
                
                // Auto로 이동할 방향 벡터를 구함.
                var autoVec = ((Vector3)_autoTargetPosition - _transform.position).normalized;
                
                // 이동 벡터를 업데이트하는 이벤트 실행. 
                OnMoveVecUpdated?.Invoke(autoVec);
                return;
            }
            
            // Auto로 이동할 위치를 구함.
            _autoTargetPosition = Util.GetRandomDirVec(_transform.position, _autoMoveVecOffset, _autoMoveVecOffset);
            _isAutoMove = true;
        }
    }
}