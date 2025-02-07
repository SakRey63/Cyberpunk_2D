using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _delay = 2.0f;

    private float _stopSpeed = 0;
    private int _numberPoint = 0;
    private Animator _animator;
    private EnemyMover _enemyMover;
    private Flipper _flipper;

    public void ContinuePatrolling()
    {
        NextTargetPosition();
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
        _flipper = GetComponent<Flipper>();
    }
    
    private IEnumerator TakeBreak()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _numberPoint = ++_numberPoint % _points.Length;
       
        yield return delay;
        
        _enemyMover.ContinueMoving();
    }
    
    private void NextTargetPosition()
    {
        if (_enemyMover.Finished)
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
                        
            _enemyMover.Move(_points[_numberPoint].position, _stopSpeed);
            
            StartCoroutine(TakeBreak());
        }
        else
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
                        
            _enemyMover.Move(_points[_numberPoint].position, _speed);
            _flipper.LockAtTarget(MovementDirection(_points[_numberPoint].position));
        }
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
