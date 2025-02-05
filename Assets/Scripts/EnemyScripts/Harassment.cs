using UnityEngine;

public class Harassment : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    
    private EnemyMover _enemyMover;
    private Animator _animator;
    private FlipperEnemy _flipperEnemy;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _flipperEnemy = GetComponent<FlipperEnemy>();
    }

    public void PursueTarget(Vector2 target)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
            
        _enemyMover.Move(target, _speed);
        _flipperEnemy.TurnInOppositeDirection(target);
    }
}
