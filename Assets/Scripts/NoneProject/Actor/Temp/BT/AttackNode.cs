
using Cysharp.Threading.Tasks;
using NoneProject.Common;
using Template.BehaviorTree.Common;


namespace NoneProject.Actor.BT
{
    public class AttackNode : ActionNode
    {
        private bool _isPlaying;

        public override NodeState Evaluate()
        {
            // Actor가 사망 상태인 경우 노드 실패 처리.
            if (Actor.Stat.isDead)
                return nodeState = NodeState.Failure;

            // 공격 가능한 상태라면 공격 행동 실행.
            if (Actor.CheckStateOfAttack())
            {
                Attack().Forget();
                return nodeState = NodeState.Running;
            }

            // 공격 가능한 대상이 없는 경우 대기 상태 및 노드 실패 처리.
            return nodeState = NodeState.Failure;
        }

        private async UniTaskVoid Attack()
        {
            if (_isPlaying)
                return;
            
            _isPlaying = true;
            Actor.Attack();

            // Actor의 공격 딜레이 만큼 대기.
            await UniTask.WaitForSeconds(Actor.Stat.attackDelay, cancellationToken: Cts.Token);
            
            _isPlaying = false;
        }

        private void OnDestroy()
        {
            Cts?.Cancel();
            Cts?.Dispose();
        }
    } 
}