using UnityEngine;
using UnityEngine.InputSystem;

namespace FPB.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Floating")]
        [SerializeField] private float _floatAmplitude = 0.1f;
        [SerializeField] private float _floatSpeed = 4f;

        [Header("Movement")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _rotationSpeed = 50f;

        [Header("Player Frame")]
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector3 _startPosition;
        private const string OBSTACLE_TAG = "Obstacle";

        private void Start()
        {
            _startPosition = Vector3.zero;
            _rigidbody.simulated = false;
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == GameState.Home ||
                GameManager.Instance.GameState == GameState.GetReady)
            {
                FloatIdle();
            }
            else
            {
                RotatePlayer();
            }
        }

        private void FloatIdle()
        {
            var newY = _startPosition.y + Mathf.Sin(Time.time * _floatSpeed) * _floatAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        private void RotatePlayer()
        {
            var angle = Mathf.Clamp(_rigidbody.linearVelocityY * 5f, -90f, 30f);

            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(0, 0, angle),
                _rotationSpeed * Time.deltaTime
            );
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            switch (GameManager.Instance.GameState)
            {
                case GameState.Home:
                case GameState.GameOver:
                    return;
                case GameState.GetReady:
                    GameManager.Instance.GamePlay();
                    _rigidbody.simulated = true;
                    break;
            }

            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (GameManager.Instance.GameState == GameState.GameOver) return;

            if (!collision2D.collider.CompareTag(OBSTACLE_TAG)) return;

            GameManager.Instance.GameOver();
        }

        public void ResetPlayer()
        {
            _rigidbody.simulated = false;
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _startPosition = new Vector3(-0.5f, 0f, 0f);
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}