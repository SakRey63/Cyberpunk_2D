using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    
    private float _epsilon = 0.1f;
    private bool _isFinished = false;

    public bool Finished => _isFinished;
    
    public void Move (Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        
        if ((transform.position - target).sqrMagnitude < _epsilon)
        {
            _isFinished = true;
        }
    }

    public void ContinueMove()
    {
        _isFinished = false;
    }
}
