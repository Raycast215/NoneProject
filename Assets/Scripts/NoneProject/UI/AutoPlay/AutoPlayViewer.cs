using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NoneProject.UI.AutoPlay
{
    // Scripted by Raycast
    // 2024.09.01
    // AutoPlay의 UI를 표시하는 클래스입니다.
    
    [Serializable]
    public class AutoPlayViewer
    {
        public Button GetButton => button;
        public TextMeshProUGUI GetTmp => tmp;
        
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private Image checkIcon;
        [SerializeField] private Button button;

        public void SetActiveIcon(bool isActive)
        {
            checkIcon.gameObject.SetActive(isActive);
        }
    }
}