
using System.Collections.Generic;
using Template.BehaviorTree.Common;
using Template.BehaviorTree.Interface;
using UnityEngine;


namespace NoneProject.Actor.BT
{
    public class SelectorNode : MonoBehaviour, INode
    {
        // 인스펙터 확인용.
        [SerializeField] private NodeState nodeState;
        [SerializeField] private List<SequenceNode> sequenceNodeList;

        public void Init(ActorController ac)
        {
            sequenceNodeList.ForEach(x => x.Init(ac));
        }
        
        public NodeState Evaluate()
        {
            if (sequenceNodeList == null || sequenceNodeList.Count == 0)
                return nodeState = NodeState.Failure;
            
            foreach (var actionNode in sequenceNodeList)
            {
                switch (actionNode.Evaluate())
                {
                    case NodeState.Running:
                        return nodeState = NodeState.Running;
                    
                    case NodeState.Success:
                        return nodeState = NodeState.Success;
                }
            }

            // 다음 Node로 이동.
            return nodeState = NodeState.Failure;
        }
    }
}