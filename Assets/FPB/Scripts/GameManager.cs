using UnityEngine;
using UnityEngine.UI;

namespace FPB.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("UI Handle")]
        [SerializeField] private GameViewHandle _gameViewHandle;

        [Header("Others")]
        [SerializeField] private Button _playButton;

        private const string BEST_SCORE_KEY = "BestScore";
        public GameState GameState { get; protected set; } = GameState.Home;
        private int _currentScore;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                _gameViewHandle.ScoreText.text = $"Score: {_currentScore}";
            }
        }

        public int BestScore
        {
            get => PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

            set => PlayerPrefs.SetInt(BEST_SCORE_KEY, value);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(PlayButtonClick);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(PlayButtonClick);
        }

        private void Start()
        {
            SetGameState(GameState.Home);

            _gameViewHandle.SetViewOnStart();
        }

        private void PlayButtonClick()
        {
            SetGameState(GameState.GetReady);
            CurrentScore = 0;

            _gameViewHandle.SetViewOnPlay();
        }

        public void GamePlay()
        {
            SetGameState(GameState.Playing);

            _gameViewHandle.ToggleGameReady(false);
        }

        public void GameOver()
        {
            SetGameState(GameState.GameOver);

            _gameViewHandle.SetViewOnGameOver();
            FinalScoreHandle();
        }

        private void FinalScoreHandle()
        {
            _gameViewHandle.GameOverScoreText.text = CurrentScore.ToString();

            if (CurrentScore > BestScore)
            {
                BestScore = CurrentScore;
            }

            _gameViewHandle.GameOverBestScoreText.text = BestScore.ToString();
        }

        public void AddScore()
        {
            if (GameState != GameState.Playing) return;

            CurrentScore++;
        }

        private void SetGameState(GameState state) => GameState = state;
    }
}