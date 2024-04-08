
using System.Collections.Generic;
using Template.BehaviorTree.Common;
using Template.BehaviorTree.Interface;
using UnityEngine;


namespace NoneProject.Actor.BT
{
    public class SequenceNode : MonoBehaviour, INode
    {
        // 인스펙터 확인용.
        [SerializeField] private NodeState nodeState;
        [SerializeField] private List<ActionNode> actionNodeList;

        public void Init(ActorController ac)
        {
            actionNodeList.ForEach(x => x.Init(ac));
        }
        
        public NodeState Evaluate()
        {
            if (actionNodeList == null || actionNodeList.Count == 0)
                return nodeState = NodeState.Failure;

            foreach (var actionNode in actionNodeList)
            {
                switch (actionNode.Evaluate())
                {
                    case NodeState.Running:
                        return nodeState = NodeState.Running;

                    // 다음 Node로 이동.
                    case NodeState.Success:
                        continue;

                    case NodeState.Failure:
                        return nodeState = NodeState.Failure;
                }
            }
            
            return nodeState = NodeState.Success;
        }
    }
}