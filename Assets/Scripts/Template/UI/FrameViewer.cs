
using TMPro;
using UnityEngine;


namespace Template.UI
{
    public class FrameViewer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI frameText;
        [SerializeField] private float speed = 0.1f;
        
        private float _deltaTime;
        private float _frame;
        
        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * speed;
            _frame = 1.0f / _deltaTime;
            
            SetText(_frame);
        }

        private void SetText(float frame)
        {
            frameText.text = $"FPS : {frame:F0}";
        }
    }
}