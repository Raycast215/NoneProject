
using Template.BehaviorTree.Common;


namespace NoneProject.Actor.BT
{
    public class MoveNode : ActionNode
    {
        public override NodeState Evaluate()
        {
            // Actor가 사망 상태인 경우 다음 노드 실행.
            // if (Actor.Stat.isDead)
            //     return nodeState = NodeState.Success;
            
            // 대기 상태.
            Actor.Idle();
            
            // 목적지까지 이동 완료. 다음 노드 실행.
            if (Actor.CheckMoveFinished())
                return nodeState = NodeState.Success;

            // 목적지까지 이동 진행중.
            Actor.Move();
            return nodeState = NodeState.Running;
        }
    }
}