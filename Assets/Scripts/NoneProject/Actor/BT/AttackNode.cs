
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using Template.BehaviorTree.Common;


namespace NoneProject.Actor.BT
{
    public class AttackNode : ActionNode
    {
        private bool _isPlating;

        public override NodeState Evaluate()
        {
            // Actor가 사망 상태인 경우 노드 실패 처리.
            if (Actor.Stat.isDead)
                return nodeState = NodeState.Failure;

            // 공격 가능한 상태라면 공격 행동 실행.
            if (Actor.CheckStateOfAttack())
            {
                Attack();
                return nodeState = NodeState.Running;
            }

            // 공격 가능한 대상이 없는 경우 대기 상태 및 노드 실패 처리.
            Actor.Animations.PlayAnimation(ActorState.Idle);
            return nodeState = NodeState.Failure;
        }

        private async void Attack()
        {
            if (_isPlating)
                return;
            
            _isPlating = true;
            Actor.Attack();
            Actor.Animations.PlayAnimation(ActorState.Attack_Normal, true);

            if (Cts.IsCancellationRequested)
                return;
            
            // Actor의 공격 딜레이 만큼 대기.
            await UniTask.WaitForSeconds(Actor.Stat.attackDelay, cancellationToken: Cts.Token);
            
            _isPlating = false;
        }

        private void OnDestroy()
        {
            Cts?.Cancel();
            Cts?.Dispose();
        }
    } 
}