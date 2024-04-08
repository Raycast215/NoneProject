

namespace Template.BehaviorTree.Common
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    
    public delegate NodeState OnUpdated();
}