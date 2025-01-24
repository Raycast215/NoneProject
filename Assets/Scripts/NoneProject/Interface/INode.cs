using NoneProject.Common;

namespace NoneProject.Interface
{
    public delegate NodeState NodeStateCallback();
    
    // Scripted by Raycast
    // 2025.01.24
    // BT에서 사용할 인터페이스입니다.
    public interface INode
    {
        public NodeState Evaluate();
    }
}