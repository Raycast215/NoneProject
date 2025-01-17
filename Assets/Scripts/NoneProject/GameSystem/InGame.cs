using Cinemachine;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Player;
using NoneProject.GameSystem.Input;
using NoneProject.Manager;
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
        public bool IsAutoMove => isAutoMove;
        
        [SerializeField] private bool isAutoMove;

        private ActorManager _actorManager;
        private CinemachineVirtualCamera _cam;
        private InGameTouch _inGameTouch;
        private Transform _playerHolder;
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
            
            if (_actorManager.Player.IsInitialized is false)
                return;
            
            _inGameTouch.UpdateTouch();
        }

        private async void Initialized()
        {
            // GameManager가 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized);
            
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
            var constData = GameManager.Instance.Const;
            
            _playerHolder = Util.CreateObject(constData.PlayerHolder, transform).transform;
        }
        
        private void LoadInput()
        {
            var caster = FindObjectOfType<GraphicRaycaster>();
            var eventSystem = FindObjectOfType<EventSystem>();
            
            _inGameTouch = new InGameTouch(caster, eventSystem);
        }

        private void LoadPlayer()
        {
            ActorManager.Instance.LoadPlayer(_playerHolder, OnComplete);
            return;

            void OnComplete(PlayerController player)
            {
                _cam.Follow = player.transform;
                _inGameTouch.OnTouched += player.Move;
            }
        }
    }
}