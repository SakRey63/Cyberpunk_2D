using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool IsGround { get; private set; }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Platform>(out _))
        {
            IsGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Platform>(out _))
        {
            IsGround = false;
        }
    }
}
