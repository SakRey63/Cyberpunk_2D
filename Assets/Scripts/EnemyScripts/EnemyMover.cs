using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    private float _epsilon = 0.1f;
    private bool _isFinished = false;

    public bool Finished => _isFinished;
    
    public void Move (Vector3 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
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
