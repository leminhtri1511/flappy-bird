using UnityEngine;

namespace FPB.Scripts.Pipe
{
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _pipePrefab;
        [SerializeField] private float _spawnRate = 2f;
        [SerializeField] private float _heightOffset = 0.8f;
        [SerializeField] private int _poolSize = 10;
    }
}