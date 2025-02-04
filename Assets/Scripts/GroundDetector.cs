using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _countObjects;

    public int Count => _countObjects;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Platform _))
        {
            _countObjects++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Platform _))
        {
            _countObjects--;
        }
    }
}
