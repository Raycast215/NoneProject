
using Template.BehaviorTree.Common;


namespace Template.BehaviorTree.Interface
{
    public interface INode
    {
        public NodeState Evaluate();
    }
}