using System.Collections.Generic;
using NoneProject.Actor.Component.Model;
using NoneProject.Actor.Component.Move;
using NoneProject.Common;
using NoneProject.Interface;

namespace NoneProject.Actor.Enemy
{
    // Scripted by Raycast
    // 2025.01.16
    // Enemy를 관리하는 클래스입니다.
    public class EnemyController : ActorBase
    {
        private readonly Dictionary<MovePattern, IMovable> _movePatternDic = new Dictionary<MovePattern, IMovable>();
        private ModelController _modelController;
        private IMovable _mover;
        
        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;
            
            if (gameObject.activeInHierarchy is false)
                return;

            _mover?.Move(MoveSpeed);
        }
        
        public void SetPattern(MovePattern toPattern)
        {
            // 이전에 사용한 패턴이 있는 경우.
            if (_movePatternDic.TryGetValue(toPattern, out var pattern))
            {
                _mover = pattern;
                return;
            }

            switch (toPattern)
            {
                case MovePattern.Random:
                    _mover = new MoveRandomVector(Rigidbody2D);
                    break;
            }
            
            _movePatternDic.Add(toPattern, _mover);
            _mover.CompleteMove(_ => _modelController.SetAnimationState(ActorState.Run));
            _mover.CompleteMove(SetScaleDirection);
        }

#region Override Methods

        protected override void Initialize()
        {
            _modelController = new ModelController(this);
            MoveSpeed = 1.0f;
            IsInitialized = true;
        }

#endregion
    }
}