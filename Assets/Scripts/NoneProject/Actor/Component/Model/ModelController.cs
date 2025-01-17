using NoneProject.Common;

namespace NoneProject.Actor.Component.Model
{
    // Scripted by Raycast
    // 2025.01.18
    // Actor중에서 Model Prefab을 사용하는 경우 해당 Model의 애니메이션을 사용하는 클래스입니다.
    public class ModelController
    {
        private readonly SPUM_Prefabs _model;

        public ModelController(ActorBase actor)
        {
            _model = actor.GetComponent<SPUM_Prefabs>();
            
            SetAnimationState(ActorState.Idle);
        }
        
        public void SetAnimationState(ActorState actorState)
        {
            _model.PlayAnimation(actorState);
        }
    }
}