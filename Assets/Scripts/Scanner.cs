using UnityEngine;

public class Scanner : MonoBehaviour
{
    private int _countEnemy;
    private Vector2 _target;

    public Vector2 Target => _target;
    public int CountEnemy => _countEnemy;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _countEnemy++;

            _target = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _countEnemy--;
        }
    }
}
