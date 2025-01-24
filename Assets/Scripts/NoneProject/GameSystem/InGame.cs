using Cinemachine;
using Cysharp.Threading.Tasks;
using NoneProject.Actor.Player;
using NoneProject.Input;
using NoneProject.Manager;
using Sirenix.OdinInspector;
using Template.Utility;
using UnityEngine;

namespace NoneProject.GameSystem
{
    // Scripted by Raycast
    // 2024.09.01
    // InGame에서 필요한 시스템을 관리하고 실행하는 클래스입니다.
    public class InGame : MonoBehaviour
    {
        public bool IsAutoMove
        {
            get => _isAutoMove;
            private set
             {
                 _isAutoMove = value;
                 ActorManager.Instance.Player.ChangeMove(_isAutoMove);
             }
        }

        [SerializeField] private JoyStickController joyStick;

        private bool _isAutoMove;
        private ActorManager _actorManager;
        private CinemachineVirtualCamera _cam;
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
            
            joyStick.UpdateController();
        }

        [Button("Change")]
        public void SetAutoMove()
        {
            IsAutoMove = !IsAutoMove;
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
            LoadPlayer();
            
            _isInitialized = true;
        }

        private void LoadHolder()
        {
            var constData = GameManager.Instance.Const;
            
            _playerHolder = Util.CreateObject(constData.PlayerHolder, transform).transform;
        }

        private void LoadPlayer()
        {
            ActorManager.Instance.LoadPlayer(_playerHolder, OnComplete);
            return;

            void OnComplete(PlayerController player)
            {
                _cam.Follow = player.transform;
                joyStick.OnMoveVectorUpdated += player.Move;
            }
        }
    }
}