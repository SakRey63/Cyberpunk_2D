using UnityEngine;

public class Stalker : MonoBehaviour
{
    [SerializeField] private Animations _animator;
    [SerializeField] private Flipper _flipper;
    
    private EnemyMover _enemyMover;
    
    private bool _isCaughtTarget;
    
    public bool IsCaughtTarget => _enemyMover.Finished;
        
    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
    }

    public void PursueTarget(Vector2 target)
    {
        _animator.MoveAnimation(true);
            
        _enemyMover.Move(target);
        
        _flipper.ChooseDirection(target);
    }
}