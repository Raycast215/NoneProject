
using UnityEngine;


namespace NoneProject.GameSystem.Map
{
    public class MapScroller : MonoBehaviour
    {
        private const float SkyMoveSpeed = 0.01f;
        
        [SerializeField] private Transform[] bgTransforms;
        [SerializeField] private float[] scrollSpeeds;
        [SerializeField] private MeshRenderer[] meshRenderers;
        [SerializeField] private MeshRenderer skyMeshRenderer;

        private float _moveValue;
        private float _moveSpeed;
        private float _skyMoveValue;

        private void Start()
        {
            Init();
        }

        public void SetSpeed(float toMoveSpeed)
        {
            _moveValue += toMoveSpeed * Time.deltaTime;
            _skyMoveValue = 0;
        }
        
        public void MoveScroll()
        {
            var isReturn = _moveValue <= 0;
            
            if (isReturn)
                return;
            
            for (var i = 0; i < bgTransforms.Length; i++)
            {
                var posX = _moveValue * scrollSpeeds[i];
                meshRenderers[i].material.mainTextureOffset = new Vector2(posX, 0.0f);
            }

            var scrollPosX = _skyMoveValue += (Time.unscaledDeltaTime * -SkyMoveSpeed);
            skyMeshRenderer.material.mainTextureOffset = new Vector2(scrollPosX, 0.0f);
        }
        
        private void Init()
        {
            for (var i = 0; i < bgTransforms.Length; i++)
            {
                meshRenderers[i] = bgTransforms[i].GetComponent<MeshRenderer>();
            }
        }
    }
}