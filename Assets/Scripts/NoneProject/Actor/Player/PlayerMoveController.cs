using System;
using NoneProject.Common;
using Template.Utility;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 이동로직을 관리하는 클래스입니다.
    
    public class PlayerMoveController : MoveController
    {
        private const float CheckDistanceValue = 0.5f;
        private const float RandomRangeValue = 20.0f;

        public event Action<ActorState> OnPlayerAnimationStateChanged = delegate {  };

        private Vector2 _autoPos;
        private bool _isMove;
        
        public void AutoMove(bool isAuto)
        {
            if (isAuto is false)
                return;

            if (_isMove)
            {
                if (CheckDistance(transform.position, _autoPos, CheckDistanceValue))
                {
                    _isMove = false;
                    return;
                }
                
                Move(_autoPos.normalized);
                return;
            }
            
            _autoPos = Util.GetRandomDirVec(transform.position, RandomRangeValue, RandomRangeValue);
            _isMove = true;
        }
        
#region Override Methods

        public override void UpdateMove(Vector2 dirVec)
        {
            if (dirVec == Vector2.zero)
            {
                OnPlayerAnimationStateChanged?.Invoke(ActorState.Idle);
                return;
            }
             
            Move(dirVec);
        }
        
        protected override void Move(Vector2 dirVec)
        {
            SetDirection(dirVec);
            OnPlayerAnimationStateChanged?.Invoke(ActorState.Run);
            transform.position += (Vector3)dirVec * Time.deltaTime;
        }

#endregion
    }
}