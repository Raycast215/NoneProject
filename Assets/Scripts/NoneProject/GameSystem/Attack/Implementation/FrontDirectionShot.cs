using NoneProject.GameSystem.Composition;
using UnityEngine;

namespace NoneProject.GameSystem.Attack.Implementation
{
    public class FrontDirectionShot : AttackBase
    {
        private Rigidbody2D _rigidbody;
        
        public override void Initialized()
        {
            var key = "TestKey";
            
            AssetRenderer = new AssetRender(key);
            MoveBehaviour = new Move2DBehaviour(_rigidbody);
            
            MoveBehaviour.SetSpeed(1.0f);
            MoveBehaviour.Move();
        }
    }
}