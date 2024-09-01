using NoneProject.Common;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    
    public class PlayerController : MonoBehaviour
    {
        private SPUM_Prefabs _model;
        private PlayerMoveController _moveController;

        private void Awake()
        {
            _model = GetComponent<SPUM_Prefabs>();
            _moveController = GetComponent<PlayerMoveController>();

            Subscribed();
        }

        public void Initialized()
        {
            _model.PlayAnimation(ActorState.Idle);
            _moveController.Initialized(transform);
        }

        public void UpdateInputMove(Vector2 input)
        {
            _moveController.UpdateMove(input);
        }

        public void UpdateAutoMove(bool isAutoPlay)
        {
            _moveController.AutoMove(isAutoPlay);
        }

        private void Subscribed()
        {
            _moveController.OnPlayerAnimationStateChanged += state => _model.PlayAnimation(state);
        }
    }
}