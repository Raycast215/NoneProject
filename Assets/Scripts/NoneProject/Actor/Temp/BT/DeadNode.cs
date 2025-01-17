
using NoneProject.Common;
using Template.BehaviorTree.Common;


namespace NoneProject.Actor.BT
{
    public class DeadNode : ActionNode
    {
        public override NodeState Evaluate()
        {
            // Actor가 사망 상태가 아닌 경우 노드 실패 처리.
            if (Actor.Stat.isDead is false) 
                return nodeState = NodeState.Failure;
            
            // Actor가 사망 상태인 경우 노드 진행중.
            return nodeState = NodeState.Running;
        }
    }
}