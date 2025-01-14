using Cinemachine;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Player;
using NoneProject.Common;
using NoneProject.GameSystem.Input;
using NoneProject.Manager;
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
        public bool IsAutoMove => isAutoMove;
        
        [SerializeField] private bool isAutoMove;

        private ActorManager _actorManager;
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
        }

        private void FixedUpdate()
        {
            if (_isInitialized is false)
                return;

            if (_actorManager.Player is null)
                return;
            
            if (_actorManager.Player.IsLoaded is false)
                return;

            if (isAutoMove)
            {
                _actorManager.Player.Move(Vector2.zero);
                return;
            }
            
            _inGameTouch.UpdateTouch();
        }

        private void Initialized()
        {
            // GameManager가 초기화 완료까지 대기.
            UniTask.WaitUntil(() => GameManager.Instance.isInitialized).Forget();
            
            // Actor Manager 캐싱.
            _actorManager = ActorManager.Instance;
            
            // InGame을 등록. 
            GameManager.Instance.SetInGame(this);
            
            LoadHolder();
            LoadInput();
            LoadPlayer();
            
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

        private void LoadPlayer()
        {
            ActorManager.Instance.LoadPlayer(_actorHolder, OnComplete);
            return;

            void OnComplete(PlayerController player)
            {
                _cam.Follow = player.transform;
                _inGameTouch.OnTouched += player.Move;
            }
        }
    }
}