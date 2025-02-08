using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float _radiusDetector = 0.6f;
    
    private bool _isGround;

    public bool IsGround => _isGround;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Platform>(out _))
        {
            _isGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Platform>(out _))
        {
            _isGround = false;
        }
    }
}
