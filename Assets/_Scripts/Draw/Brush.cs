using UnityEngine;

namespace _Scripts.Draw
{
    public class Brush : MonoBehaviour
    {
        [SerializeField]
        private float _height;
        [SerializeField]
        private TrailRenderer _trailRenderer;

        private void Start()
        {
            _trailRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            Draw();
            Clear();
        }

        private void Clear()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _trailRenderer.gameObject.SetActive(false);
                _trailRenderer.Clear();
            }
        }

        private void Draw()
        {
            if (Input.GetMouseButton(0))
            {
                _trailRenderer.gameObject.SetActive(true);
                var cameraZDistance = Camera.main.WorldToScreenPoint(_trailRenderer.transform.position).z;
                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                newPosition.y = _height;
                
                _trailRenderer.transform.position = newPosition;
            }
        }
        
    }
}