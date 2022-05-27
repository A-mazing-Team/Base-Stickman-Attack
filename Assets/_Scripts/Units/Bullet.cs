using UnityEngine;

public class Bullet: MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 15f;
    private Transform _target;
        
    public void Shoot(Transform to)
    {
        _target = to;
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 targetPosition = _target.position + Vector3.up;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _bulletSpeed * Time.deltaTime);
            Rotate(targetPosition);
                 
            if (!_target.gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
                 
            if (transform.position == _target.position)
            {
                _target = null;
                gameObject.SetActive(false);
            }
        }
            
    }
        
    private void Rotate(Vector3 to)
    {
        Vector3 dir = (to - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.forward);
            
        transform.rotation = rotation;
    }
}