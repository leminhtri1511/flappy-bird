using FPB.Scripts.Manager;
using UnityEngine;

namespace FPB.Scripts.Pipe
{
    public class PipeController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _hideXPosition = -10f;

        private PipeSpawner _pipeSpawner;

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.Playing) return;
            
            transform.position += Vector3.left * (_moveSpeed * Time.deltaTime);

            if (transform.position.x < _hideXPosition)
            {
                _pipeSpawner.ReturnToPool(this);
            }
        }

        public void Initialize(PipeSpawner pipeSpawner)
        {
            _pipeSpawner = pipeSpawner;
        }
    }
}