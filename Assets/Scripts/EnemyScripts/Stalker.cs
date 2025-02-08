using UnityEngine;

public class Stalker : MonoBehaviour
{
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
            
        _enemyMover.Move(target);
        
        _flipper.MovementDirection(target);
    }
}