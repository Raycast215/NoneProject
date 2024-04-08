
using System.Threading;
using Template.BehaviorTree.Common;
using Template.BehaviorTree.Interface;
using UnityEngine;


namespace NoneProject.Actor.BT
{
    public abstract class ActionNode : MonoBehaviour, INode
    {
        // 인스펙터 확인용.
        [SerializeField] protected NodeState nodeState;
        
        protected ActorController Actor;
        protected CancellationTokenSource Cts;

        public void Init(ActorController ac)
        {
            Actor = ac;
            Cts = new CancellationTokenSource();
        }

        public abstract NodeState Evaluate();
    }
}