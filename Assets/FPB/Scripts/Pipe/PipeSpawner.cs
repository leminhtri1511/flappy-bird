using System.Collections.Generic;
using FPB.Scripts.Manager;
using UnityEngine;

namespace FPB.Scripts.Pipe
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _pipePrefab;
        [SerializeField] private float _spawnRate = 2f;
        [SerializeField] private float _heightOffset = 0.8f;
        [SerializeField] private int _poolSize = 10;

        private float _timer;
        private Camera _mainCamera;
        private readonly Queue<PipeController> _pipePool = new();
        private readonly List<PipeController> _allPipes = new();

        private void Awake()
        {
            _mainCamera = Camera.main;
            SetSpawnPosition();
            CreatePool();
        }

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.Playing) return;

            _timer += Time.deltaTime;

            while (_timer >= _spawnRate)
            {
                SpawnPipe();
                _timer -= _spawnRate;
            }
        }

        private void SetSpawnPosition()
        {
            float screenRight = _mainCamera.orthographicSize * _mainCamera.aspect;
            float spawnX = screenRight + 0.5f;

            Vector3 pos = transform.position;
            pos.x = spawnX;

            transform.position = pos;
        }

        private void CreatePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                var pipeObj = Instantiate(_pipePrefab);
                pipeObj.SetActive(false);

                var pipe = pipeObj.GetComponent<PipeController>();
                pipe.Initialize(this);

                _pipePool.Enqueue(pipe);
                _allPipes.Add(pipe);
            }
        }

        private void SpawnPipe()
        {
            if (_pipePool.Count == 0)
            {
                Debug.Log("No pipes in list");
                return;
            }

            var pipe = _pipePool.Dequeue();
            var randomY = Random.Range(-_heightOffset, _heightOffset);

            pipe.transform.position = transform.position + new Vector3(0, randomY, 0);
            pipe.gameObject.SetActive(true);
        }

        public void ReturnToPool(PipeController pipe)
        {
            pipe.gameObject.SetActive(false);
            _pipePool.Enqueue(pipe);
        }

        public void ResetSpawner()
        {
            _timer = 0f;
            _pipePool.Clear();

            foreach (var pipe in _allPipes)
            {
                pipe.gameObject.SetActive(false);
                _pipePool.Enqueue(pipe);
            }
        }
    }
}