
using NoneProject.Actor.BT;
using NoneProject.Actor.Data;
using UnityEngine;


namespace NoneProject.Actor
{
    public abstract class ActorController : MonoBehaviour
    {
        [SerializeField] protected SelectorNode selector;
        
        public ActorStat Stat { get; private set; }
        public NodeRunner NodeRunner { get; private set; }

        protected virtual void Awake()
        {
            NodeRunner = new NodeRunner(selector, this);

            Stat = new ActorStat();
            
            Subscribed();
        }

        public abstract void Move();

        public abstract void Idle();

        public virtual void Attack() { }

        public abstract bool CheckMoveFinished();
        
        public virtual bool CheckStateOfAttack() { return false; }
        
        protected virtual void Subscribed() { }
    }
}