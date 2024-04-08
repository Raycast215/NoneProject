
using System;
using NoneProject.Common;


namespace NoneProject.Actor.Animation
{
    public class ActorAnimation
    {
        public event Action OnIdleAnimation = delegate {  }; 
        public event Action OnRunAnimation = delegate {  }; 
        public event Action OnDeathAnimation = delegate {  }; 
        public event Action OnAttackAnimation = delegate {  }; 

        private ActorState _state;
        
        public void PlayAnimation(ActorState toState, bool isReplay = false)
        {
            if (isReplay is false && _state == toState)
                return;
            
            switch (toState)
            {
                case ActorState.Idle:
                    _state = ActorState.Idle;
                    OnIdleAnimation?.Invoke();
                    break;
                
                case ActorState.Run:
                    _state = ActorState.Run;
                    OnRunAnimation?.Invoke();
                    break;
                
                case ActorState.Death:
                    _state = ActorState.Death;
                    OnDeathAnimation?.Invoke();
                    break;
                
                case ActorState.Attack_Normal:
                    _state = ActorState.Attack_Normal;
                    OnAttackAnimation?.Invoke();
                    break;
            }
        }
    }
}