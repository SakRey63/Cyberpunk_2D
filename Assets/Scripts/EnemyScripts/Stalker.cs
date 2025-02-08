using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    
    private EnemyMover _enemyMover;
    private EnemyAnimator _animator;
    private Flipper _flipper;
    private bool _isCaughtTarget;
    
    public bool IsCaughtTarget => _enemyMover.Finished;
        
    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
        _enemyMover = GetComponent<EnemyMover>();
        _flipper = GetComponent<Flipper>();
    }

    public void PursueTarget(Vector2 target)
    {
        _animator.MoveAnimation(true);
            
        _enemyMover.Move(target, _speed);
        
        _flipper.LockAtTarget(MovementDirection(target));
    }
    
    private float MovementDirection(Vector2 point)
    {
        float right = 1;
        float left = -1;
        
        if (point.x < transform.position.x)
        {
            return left;
        }
        else
        {
            return right;
        }
    }
}