using FPB.Scripts.Manager;
using UnityEngine;

namespace FPB.Scripts.Score
{
    public class ScoreZone : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        private bool _hasScore;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_hasScore) return;

            if (collision.CompareTag(PLAYER_TAG))
            {
                _hasScore = true;
                GameManager.Instance.AddScore();
            }
        }

        public void ResetScoreZone()
        {
            _hasScore = false;
        }
    }
}