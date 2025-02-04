using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private Wallet _wallet;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _facingRight = true;
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalWasPressed += Move;
        _inputReader.JumpWasPressed += Jump;
    }

    private void OnDisable()
    {
        _inputReader.HorizontalWasPressed -= Move;
        _inputReader.JumpWasPressed -= Jump;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TryGetComponent(out Money _))
        {
            _wallet.AddMoney();
            
            Destroy(other.gameObject);
        }
    }

    private void Move(int moveDirection)
    {
        _rigidbody2D.velocity = new Vector2(moveDirection * _speed, _rigidbody2D.velocity.y);

        if (_facingRight == false && moveDirection > 0 || _facingRight && moveDirection < 0)
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;
        }

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, moveDirection > 0 || moveDirection < 0);
    }

    private void Jump()
    {
        if (_detector.Count > 0)
        {
            _rigidbody2D.velocity = (Vector2.up * _forceJump);
        }
    }
}
