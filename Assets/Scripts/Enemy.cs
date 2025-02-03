using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private float _delay = 2.0f;

    private int _numberPoint = 0;
    private float _epsilon = 0.1f;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private IEnumerator LingerOnGoal()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
        
        float defaultSpeed = _speed;
        
        float zeroSpeed = 0;
        
        _speed = zeroSpeed;
        
        NextTargetPosition();
        
        yield return delay;

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
        
        _speed = defaultSpeed;
    }
    
    private void Move()
    {
        Vector3 target = _points[_numberPoint].position;

        if (transform.position.x < target.x)
        {
            _renderer.flipX = false;
        }
        else
        {
            _renderer.flipX = true;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        
        if ((transform.position - target).sqrMagnitude < _epsilon)
        {
            StartCoroutine(LingerOnGoal());
        }
    }

    private void NextTargetPosition()
    {
        _numberPoint ++;

        if (_numberPoint == _points.Length)
        {
            _numberPoint = 0;
        }
    }
}
