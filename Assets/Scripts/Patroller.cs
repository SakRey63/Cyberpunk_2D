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
    private Mover _mover;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        _mover.EnemyWasReachingTarget += NextTargetPosition;
    }

    private void OnDisable()
    {
        _mover.EnemyWasReachingTarget -= NextTargetPosition;
    }

    private IEnumerator LingerOnGoal()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);

        float tempSpeed = _speed;

        _speed = _zeroSpeed;
        
        yield return delay;

        _speed = tempSpeed;

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
    }
    
    private void NextTargetPosition()
    {
        _numberPoint = ++_numberPoint % _points.Length;

        StartCoroutine(LingerOnGoal());
    }

    public void ContinuePatrolling()
    {
        _mover.Move(_points[_numberPoint].position, _speed);
    }
}
