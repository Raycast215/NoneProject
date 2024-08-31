using NoneProject.GameSystem.Input;
using NoneProject.GameSystem.Stage;
using Template.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NoneProject.GameSystem
{
    // Scripted by Raycast
    // 2024.09.01
    // InGame에서 필요한 시스템을 관리하고 실행하는 클래스입니다.
    
    public class InGame : MonoBehaviour
    {
        private const string ActorHolder = "Actor Holder";

        private InGameTouch _inGameTouch;
        private Transform _actorHolder;
        private bool _isAuto;
        private bool _isInitialized;
        private bool _isGameStart;

        private void Start()
        {
            Initialized();
        }

        private void FixedUpdate()
        {
            if (_isInitialized is false)
                return;
            
            _inGameTouch.UpdateTouch();
        }

        private void Initialized()
        {
            _actorHolder = Util.CreateObject(ActorHolder, transform).transform;

            LoadInput();
            LoadPlayer();
            
            _isInitialized = true;
        }

        private void LoadInput()
        {
            var caster = FindObjectOfType<GraphicRaycaster>();
            var eventSystem = FindObjectOfType<EventSystem>();
            
            _inGameTouch = new InGameTouch(caster, eventSystem);
        }
        
        private async void LoadPlayer()
        {
            var player = await ActorCreator.CreatePlayer("Player_Normal_Magic", _actorHolder);
            
            player.Initialized();

            _inGameTouch.OnTouched += player.Move;
        }
    }
}