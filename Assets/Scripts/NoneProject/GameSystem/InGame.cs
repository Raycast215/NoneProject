using Cinemachine;
using Cysharp.Threading.Tasks;
using NoneProject.Input;
using NoneProject.Manager;
using NoneProject.Stage;
using NoneProject.Tile;
using NoneProject.UI.EnemyCount;
using NoneProject.UI.StageTimer;
using NoneProject.Utility;
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
        public Timer Timer { get; private set; }
       
        private CinemachineVirtualCamera _cam;
        private JoyStickController _joyStick;
        private TileCreator _tileCreator;
        private StageController _stageController;
        
        private bool _isAutoMove;
        // To Do : Data Load하기.
        private int _stageIndex = 1;
        
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

            if (_joyStick is null)
                return;

            if (_stageController.CurrentStage is null)
                return;
            
            _joyStick.UpdateController();
            Timer?.StartTimer(_stageController.CurrentStage.second);
        }
        
        private async void Initialize()
        {
            // GameManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => GameManager.Instance.isInitialized);
            // Player 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => Manager.PlayerManager.Instance.isInitialized);
            // Enemy 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => EnemyManager.Instance.isInitialized);
            // Projectile 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => ProjectileManager.Instance.isInitialized);
            // StageManager 초기화 완료까지 대기.
            await UniTask.WaitUntil(() => StageManager.Instance.isInitialized);
            
            // InGame을 등록. 
            GameManager.Instance.SetInGame(this);

            // 카메라가 Player를 따라가도록 설정.
            _cam.Follow = Manager.PlayerManager.Instance.Player.transform;

            _tileCreator = new TileCreator();
            
            // Tile 생성까지 대기.
            await UniTask.WaitUntil(() => _tileCreator.IsInitialized);
            
            Timer = new Timer(1.0f);
            _stageController = new StageController();
            _stageController.ChangeStageIndex(_stageIndex);
            
            Subscribe();
            
            IsInitialized = true;
        }

        private void Subscribe()
        {
            _joyStick.OnMoveVectorUpdated += Manager.PlayerManager.Instance.Player.Move;
            
            _stageController.OnEnemySpawned += enemyID => EnemyManager.Instance.Get(enemyID, new Vector2(), true);
            
            Timer.OnTimerStarted += () => _stageController.StartStage().Forget();
            Timer.OnTimerFinished += _stageController.Dispose;
            Timer.OnTimerFinished += Timer.Dispose;
            Timer.OnTimerFinished += () => _stageIndex++;
            Timer.OnTimerFinished += () => _stageController.ChangeStageIndex(_stageIndex);

            UIManager.Instance.Get<StageTimerViewer>(nameof(StageTimerViewer)).Subscribe();
            UIManager.Instance.Get<EnemyCountViewer>(nameof(EnemyCountViewer)).Subscribe();
        }
    }
}