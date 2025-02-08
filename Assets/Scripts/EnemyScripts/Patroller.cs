using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _delay = 2.0f;

    private float _stopSpeed = 0;
    private int _numberPoint = 0;
    private EnemyAnimator _animator;
    private EnemyMover _enemyMover;
    private Flipper _flipper;

    private void Awake()
    {
        _animator = GetComponent<EnemyAnimator>();
        _enemyMover = GetComponent<EnemyMover>();
        _flipper = GetComponent<Flipper>();
    }
    
    private IEnumerator TakeBreak()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _numberPoint = ++_numberPoint % _points.Length;
       
        yield return delay;
        
        _enemyMover.ContinueMove();
    }
    
    public void ContinuePatroller()
    {
        SetNextTargetPosition();
    }
    
    private void SetNextTargetPosition()
    {
        if (_enemyMover.Finished)
        {
            _animator.MoveAnimation(false);
            
            StartCoroutine(TakeBreak());
        }
        else
        {
            _animator.MoveAnimation(true);
                        
            _enemyMover.Move(_points[_numberPoint].position);
            _flipper.MovementDirection(_points[_numberPoint].position);
        }
    }
}
