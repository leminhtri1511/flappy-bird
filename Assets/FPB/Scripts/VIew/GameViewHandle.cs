using TMPro;
using UnityEngine;

namespace FPB.Scripts.View
{
    public class GameViewHandle : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject _logo;
        [SerializeField] private GameObject _playButton;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _gameReadyPanel;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _gameOverScoreText;
        [SerializeField] private TextMeshProUGUI _gameOverBestScoreText;
        
        public TextMeshProUGUI ScoreText => _scoreText;
        public TextMeshProUGUI GameOverScoreText => _gameOverScoreText;
        public TextMeshProUGUI GameOverBestScoreText => _gameOverBestScoreText;

        public void SetViewOnStart()
        {
            ToggleGameLogo(true);
            TogglePlayButton(true);
            ToggleGameReady(false);
            ToggleGameOver(false);
            ToggleScoreText(false);
        }

        public void SetViewOnPlay()
        {
            ToggleGameLogo(false);
            TogglePlayButton(false);
            ToggleGameReady(true);
            ToggleGameOver(false);
            ToggleScoreText(true);
        }

        public void SetViewOnGameOver()
        {
            ToggleGameOver(true);
            ToggleScoreText(false);
            TogglePlayButton(true);
        }

        public void ToggleGameLogo(bool isVisible) => _logo.SetActive(isVisible);
        public void TogglePlayButton(bool isVisible) => _playButton.SetActive(isVisible);
        public void ToggleGameReady(bool isVisible) => _gameReadyPanel.SetActive(isVisible);
        public void ToggleScoreText(bool isVisible) => _scoreText.gameObject.SetActive(isVisible);
        public void ToggleGameOver(bool isVisible) => _gameOverPanel.SetActive(isVisible);
    }
}