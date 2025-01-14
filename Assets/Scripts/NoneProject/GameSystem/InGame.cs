using Cinemachine;
using NoneProject.Actor.Player;
using NoneProject.Common;
using NoneProject.GameSystem.Input;
using NoneProject.GameSystem.Stage;
using NoneProject.UI.AutoPlay;
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
        public PlayerController Player { get; private set; }
        public bool IsAutoMove => isAutoMove;

        [SerializeField] private Vector2 input;
        [SerializeField] private bool isAutoMove;
        
        private CinemachineVirtualCamera _cam;
        private AutoPlay _autoPlayUI;
        private InGameTouch _inGameTouch;
        private Transform _actorHolder;
        private bool _isInitialized;
        private bool _isGameStart;

        private void Awake()
        {
            _cam = FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            Initialized();
            GameManager.Instance.SetInGame(this);
        }

        private void FixedUpdate()
        {
            if (_isInitialized is false)
                return;

            if (Player is null)
                return;
            
            if (Player.IsLoaded is false)
                return;

            if (isAutoMove)
            {
                Player.Move(Vector2.zero);
                return;
            }
            
            _inGameTouch.UpdateTouch();
        }

        private void Initialized()
        {
            LoadHolder();
            LoadInput();
            Load();
            
            _isInitialized = true;
        }

        private void LoadHolder()
        {
            _actorHolder = Util.CreateObject(Define.PlayerHolder, transform).transform;
        }
        
        private void LoadInput()
        {
            var caster = FindObjectOfType<GraphicRaycaster>();
            var eventSystem = FindObjectOfType<EventSystem>();
            
            _inGameTouch = new InGameTouch(caster, eventSystem);
        }

        private async void Load()
        {
            Player = await ActorCreator.CreatePlayer(_actorHolder);
            Player.Initialized();
            
            _cam.Follow = Player.transform;
            
            _inGameTouch.OnTouched += Player.Move;
            _inGameTouch.OnTouched += vec => input = vec;
        }

        // private void LoadAutoPlayUI()
        // {
        //     AutoPlayPoint = FindObjectOfType<AutoPlayPoint>();
        //     
        //     _autoPlayUI = FindObjectOfType<AutoPlay>();
        //     _autoPlayUI.OnAutoPlayUpdated += SetInputMoveEvent;
        //     _autoPlayUI.Initialized();
        // }
      
        // private void SetInputMoveEvent(bool isAutoInput)
        // {
        //     if (isAutoInput)
        //     {
        //         _inGameTouch.OnTouched -= Player.UpdateInputMove;
        //     }
        //     else
        //     {
        //         _inGameTouch.OnTouched += Player.UpdateInputMove;
        //         Player.Initialized();
        //         GameManager.Instance.InGame.AutoPlayPoint.gameObject.SetActive(false);
        //     }
        // }
    }
}