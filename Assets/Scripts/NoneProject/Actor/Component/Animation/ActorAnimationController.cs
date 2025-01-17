using NoneProject.Common;

namespace NoneProject.Actor.Component.Animation
{
    // Scripted by Raycast
    // 2025.01.14
    // Model의 Animation을 처리하는 클래스입니다.
    public class ActorAnimationController
    {
        private readonly SPUM_Prefabs _model;
        
        public ActorAnimationController(SPUM_Prefabs model)
        {
            _model = model;
            SetAnimationState(ActorState.Idle);
        }

        public void SetAnimationState(ActorState actorState)
        {
            _model.PlayAnimation(actorState);
        }
    }
}