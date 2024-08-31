
using NoneProject.Common;
using NoneProject.GameSystem.Stage;
using Template.Utility;
using UnityEngine;


namespace NoneProject.Actor
{
    public class EnemyController : ActorController
    {
        [SerializeField] private SPUM_Prefabs animationController;

        public void Init(float toHeight)
        {
            var posX = Util.GetScreenWidth();
            transform.localPosition = new Vector3(posX, toHeight, 0.0f);

            // Test Stat
            Stat.targetPosX = 1.0f;
            Stat.moveSpeed = 10.0f;
            Stat.attackDelay = 1.0f;
        }
        
        public override void Move()
        {
            var posX = Stat.moveSpeed * Time.deltaTime;
            var posZ = Util.GetPosYDepth(transform.position.y);
            transform.Translate(Vector3.left * posX);
            transform!.position.Set(transform.position.x, transform.position.y, posZ);
            Animations.PlayAnimation(ActorState.Run);
        }

        public override void Idle()
        {
            if (ActorListHolder.PlayerList.Count <= 0 || ActorListHolder.PlayerList is null)
                Animations.PlayAnimation(ActorState.Idle);
        }

        public override void Attack()
        {
            
        }

        public override bool CheckMoveFinished()
        {
            return transform.localPosition.x <= Stat.targetPosX;
        }

        public override bool CheckStateOfAttack()
        {
            return ActorListHolder.PlayerList.Count > 0;
        }
        
        protected override void Subscribed()
        {
            Animations.OnIdleAnimation += () => animationController.PlayAnimation(ActorState.Idle);
            Animations.OnRunAnimation += () => animationController.PlayAnimation(ActorState.Run);
            Animations.OnDeathAnimation += () => animationController.PlayAnimation(ActorState.Death);
            Animations.OnAttackAnimation += () => animationController.PlayAnimation(ActorState.Attack_Normal);
        }
    }
}