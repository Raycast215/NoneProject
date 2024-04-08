
using NoneProject.Actor.BT;
using NoneProject.Data;
using NoneProject.GameSystem.Map;
using UnityEngine;


namespace NoneProject.Actor
{
    public class MapController : ActorController
    {
        [SerializeField] private MapScroller mapScroller;
        
        private StageNode _stageNode;
        
        protected override void Awake()
        {
            base.Awake();
            
            _stageNode = transform.GetComponentInChildren<StageNode>();
        }

        public void Set(Stage toStageData, Transform parent)
        {
            _stageNode.Set(toStageData, parent);
        }
        
        public override void Move()
        {
            //mapScroller.SetSpeed(0);
        }

        public override void Idle()
        {
            
        }

        public override bool CheckMoveFinished()
        {
            return true;
        }
    }
}