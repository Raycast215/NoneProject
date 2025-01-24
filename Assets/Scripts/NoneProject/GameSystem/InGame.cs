using Cinemachine;
using Cysharp.Threading.Tasks;
using NoneProject.Input;
using NoneProject.Manager;
using Sirenix.OdinInspector;
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
                 Manager.PlayerManager.Instance.Player.ChangeMove(_isAutoMove);
             }
        }

        public bool IsInitialized { get; private set; }
        
        private CinemachineVirtualCamera _cam;
        private JoyStickController _joyStick;
        private bool _isAutoMove;

        private void Awake()
        {
            _cam = FindObjectOfType<CinemachineVirtualCamera>();
            _joyStick = FindObjectOfType<JoyStickController>();
        }

        private void Start()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            if (IsInitialized is false)
                return;

            _joyStick.UpdateController();
        }

        [Button("Change")]
        public void SetAutoMove()
        {
            IsAutoMove = !IsAutoMove;
        }
        
        private async void Initialize()
        {
            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized);
            // DataManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => StatDataManager.Instance.isInitialized);
            // Player 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => Manager.PlayerManager.Instance.isInitialized);
            // Enemy 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => EnemyManager.Instance.isInitialized);
            // Projectile 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => ProjectileManager.Instance.isInitialized);

            // InGame을 등록. 
            GameManager.Instance.SetInGame(this);

            // 카메라가 Player를 따라가도록 설정.
            _cam.Follow = Manager.PlayerManager.Instance.Player.transform;
            
            Subscribe();
            
            IsInitialized = true;
        }

        private void Subscribe()
        {
            _joyStick.OnMoveVectorUpdated += Manager.PlayerManager.Instance.Player.Move;
        }
    }
}