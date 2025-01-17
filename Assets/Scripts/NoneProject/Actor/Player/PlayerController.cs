using NoneProject.Actor.Component.Attack;
using NoneProject.Actor.Component.Model;
using UnityEngine;

namespace NoneProject.Actor.Player
{
    // Scripted by Raycast
    // 2024.09.01
    // Player의 로직을 처리하는 클래스입니다.
    public class PlayerController : ActorBase
    {
        [SerializeField] private Transform directionPoint;
        
        private PlayerMoveController _moveController;
        private ModelController _modelController;
        private AttackForward _testAttack;
        private float _testTime;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;

            _testTime += Time.deltaTime;

            if (_testTime > 1.0f)
            {
                _testAttack.Attack("Projectile_IceBolt", transform.position, directionPoint.position);
                _testTime = 0.0f;
            }
            
            if (GameManager.Instance.InGame.IsAutoMove is false)
                return;
            
            _moveController.Move(MoveSpeed, Vector2.zero);
        }

        public void Set()
        {
            MoveSpeed = 2.0f;
        }
        
#region Override Methods

        public override void Move(Vector2 moveVec)
        {
            _moveController.Move(MoveSpeed, moveVec);
        }
        
        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            _moveController = new PlayerMoveController(Rigidbody2D);
            _testAttack = new AttackForward(transform);
            
            Subscribe();
            Set();

            IsInitialized = true;
        }
        
        protected override void Subscribe()
        {
            _moveController.OnDirectionUpdated += dir => directionPoint.localPosition = dir;
            _moveController.OnAnimationStateChanged += state => _modelController.SetAnimationState(state);
            _moveController.Subscribe();
        }
        
#endregion
    }
}