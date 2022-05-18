using System;
using UnityEngine;

namespace _Scripts.CameraMove
{
    public class CameraMovement : MonoBehaviour
    {
        private const int CameraTargetLayer = 6;
        private Vector3 _hitPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;
        private Vector3 _cameraPosition = Vector3.zero;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetHitPoint();
            }

            if (Input.GetMouseButton(0))
            {
                if (_hitPosition != Vector3.zero)
                {
                    _currentPosition = Input.mousePosition;
                    LeftMouseDrag();
                }
                
            }

            if (Input.GetMouseButtonUp(0))
            {
                _hitPosition = Vector3.zero;
            }
        }

        private void LeftMouseDrag()
        {
            _currentPosition.z = _hitPosition.z = _cameraPosition.y;
            Vector3 direction = Camera.main.ScreenToWorldPoint(_currentPosition) -
                                Camera.main.ScreenToWorldPoint(_hitPosition);
            
            direction = direction * -1;
            Vector3 position = _cameraPosition + direction;
            position.y = _cameraPosition.y;
            transform.position = position;
        }

        private void SetHitPoint()
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
                
            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.gameObject.layer == CameraTargetLayer)
                {
                    _hitPosition = Input.mousePosition;
                    _cameraPosition = transform.position;
                }
            }
        }
        
    }
}