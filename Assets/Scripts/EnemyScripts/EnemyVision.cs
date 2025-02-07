using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private bool _isDetected = false;
    private Vector2 _target;

    public Vector2 Target => _target;
    public bool IsDetected => _isDetected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _isDetected = true;

            _target = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _isDetected = false;
        }
    }
}
