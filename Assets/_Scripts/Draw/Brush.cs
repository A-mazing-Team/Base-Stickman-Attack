using System;
using UnityEngine;

namespace _Scripts.Draw
{
    public class Brush : MonoBehaviour
    {
        private const int SpawnLayer = 7;
        public event Action<Vector3> OnDeltaPassed;
        
        [SerializeField]
        private float _height;
        [SerializeField]
        private TrailRenderer _trailRenderer;
        
        public float spawnOffset;

        private Vector3 _previousPosition = Vector3.zero;

        private bool _canDraw = false;

        private void Start()
        {
            _trailRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_canDraw)
            {
                return;
            }
            
            Draw();
            Clear();
        }

        private void Clear()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _trailRenderer.Clear();
                _trailRenderer.gameObject.SetActive(false);
                _previousPosition = Vector3.zero;
            }
        }

        private void Draw()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(Ray, out hit))
                {
                    if (hit.collider.gameObject.layer == SpawnLayer)
                    {
                        _previousPosition = hit.point + Vector3.up * 0.1f;
                        _trailRenderer.transform.position = _previousPosition;
                        _trailRenderer.gameObject.SetActive(true);
                    }
                    
                }
            }
            
            
            if (Input.GetMouseButton(0) && _previousPosition != Vector3.zero)
            {
                var newPosition = GetCameraPosition();
                _trailRenderer.transform.position = newPosition;

                if (Vector3.Distance(_previousPosition, newPosition) >= spawnOffset)
                {
                    _previousPosition = newPosition;
                    OnDeltaPassed?.Invoke(newPosition);
                }
            }
        }

        private Vector3 GetCameraPosition()
        {
            var cameraZDistance = Camera.main.WorldToScreenPoint(_trailRenderer.transform.position).z;
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            newPosition.y = _height;
            return newPosition;
        }

        public void CanDraw(bool state)
        {
            _canDraw = state;
        }
        
    }
}