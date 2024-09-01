using NoneProject.Actor.Player;
using NoneProject.GameSystem.Input;
using NoneProject.GameSystem.Stage;
using Template.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AutoPlay = NoneProject.UI.AutoPlay.AutoPlay;

namespace NoneProject.GameSystem
{
    // Scripted by Raycast
    // 2024.09.01
    // InGame에서 필요한 시스템을 관리하고 실행하는 클래스입니다.
    
    public class InGame : MonoBehaviour
    {
        private const string ActorHolder = "Actor Holder";

        private PlayerController _player;
        private AutoPlay _autoPlayUI;
        private InGameTouch _inGameTouch;
        private Transform _actorHolder;
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
            
            if (_player is null)
                return;
            
            _inGameTouch.UpdateTouch();
            _player.UpdateAutoMove(_autoPlayUI.IsAutoPlay);
        }

        private void Initialized()
        {
            LoadHolder();
            LoadInput();
            LoadPlayer();
            LoadAutoPlayUI();
            
            _isInitialized = true;
        }

        private void LoadHolder()
        {
            _actorHolder = Util.CreateObject(ActorHolder, transform).transform;
        }
        
        private void LoadInput()
        {
            var caster = FindObjectOfType<GraphicRaycaster>();
            var eventSystem = FindObjectOfType<EventSystem>();
            
            _inGameTouch = new InGameTouch(caster, eventSystem);
        }

        private async void LoadPlayer()
        {
            _player = await ActorCreator.CreatePlayer("Player_Normal_Magic", _actorHolder);
            _player.Initialized();
            _inGameTouch.OnTouched += _player.UpdateInputMove;
        }

        private void LoadAutoPlayUI()
        {
            _autoPlayUI = FindObjectOfType<AutoPlay>();
            _autoPlayUI.OnAutoPlayUpdated += SetInputMoveEvent;
        }

        private void SetInputMoveEvent(bool isAutoInput)
        {
            if (isAutoInput)
                _inGameTouch.OnTouched -= _player.UpdateInputMove;
            else
                _inGameTouch.OnTouched += _player.UpdateInputMove;
        }
    }
}