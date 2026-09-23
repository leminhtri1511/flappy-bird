using FPB.Scripts.Manager;
using UnityEngine;

namespace FPB.Scripts.View
{
    public class GroundScroller : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 2f;
        [SerializeField] private Material _material;

        private Vector2 _offset;

        private void Update()
        {
            if (GameManager.Instance.GameState == GameState.GameOver) return;
            
            _offset.x += _scrollSpeed * Time.deltaTime;
            
            _material.mainTextureOffset = _offset;
        }
    }
}