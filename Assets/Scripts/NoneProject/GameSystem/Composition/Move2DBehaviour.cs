using UnityEngine;

namespace NoneProject.GameSystem.Composition
{
    public class Move2DBehaviour
    {
        private readonly Rigidbody2D _rigidbody;
        private float _speed;
        
        public Move2DBehaviour(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void Move()
        {
            var vec = Time.deltaTime * _speed * Vector2.up;
            
            _rigidbody.MovePosition(vec);
        }
    }
}