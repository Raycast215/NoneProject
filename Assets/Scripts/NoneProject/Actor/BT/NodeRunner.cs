

namespace NoneProject.Actor.BT
{
    public class NodeRunner
    {
        public bool IsRun { get; private set; }
        
        private readonly SelectorNode _node;
        private readonly ActorController _actor;

        public NodeRunner(SelectorNode node, ActorController ac)
        {
            // 상위 노드 저장.
            _node = node;
            // Actor 저장.
            _actor = ac;
            // Actor 전달.
            _node.Init(ac);
        }
        
        public void OperateNode()
        {
            var isReturn = _actor is null || _node is null || IsRun is false;
            
            if (isReturn)
                return;
            
            // 노드 평가 실행.
            _node.Evaluate();
        }

        public void SetRunState(bool isRunState)
        {
            IsRun = isRunState;
        }
    }
}