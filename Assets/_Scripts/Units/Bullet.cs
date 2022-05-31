using QFSW.MOP2;
using UnityEngine;

public class Bullet: PoolableMonoBehaviour
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

            if (Vector3.Distance(transform.position ,targetPosition) < 0.2f || !_target.gameObject.activeSelf)
            {
                Release();
                Reset();
                _target = null;
            }
        }
            
    }
        
    private void Rotate(Vector3 to)
    {
        Vector3 dir = (to - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.forward);
            
        transform.rotation = rotation;
    }
    
    protected virtual void Reset(){}
    
}