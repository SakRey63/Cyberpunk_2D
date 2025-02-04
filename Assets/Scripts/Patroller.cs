using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _delay = 2.0f;
    [SerializeField] private float _speed = 3.0f;
    
    private int _numberPoint = 0;
    private float _zeroSpeed = 0;
    private Animator _animator;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _enemyMover.EnemyWasReachingTarget += SelectTarget;
    }

    private void OnDisable()
    {
        _enemyMover.EnemyWasReachingTarget -= SelectTarget;
    }

    private void SelectTarget()
    {
        StartCoroutine(LingerOnGoal());
    }

    private IEnumerator LingerOnGoal()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
        
       float tempSpeed = _speed;

        _speed = _zeroSpeed;
        
        NextTargetPosition();
        
        yield return delay;

        _speed = tempSpeed;

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
    }
    
    private void NextTargetPosition()
    {
        _numberPoint = ++_numberPoint % _points.Length;
    }

    public void ContinuePatrolling()
    {
        _enemyMover.Move(_points[_numberPoint].position, _speed);
    }
}
