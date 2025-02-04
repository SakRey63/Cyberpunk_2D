using UnityEngine;

[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private InputReader _inputReader;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private bool _facingRight = true;
    private bool _isPlatform;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalWasPressed += Move;
        _inputReader.JumpWasPressed += JumpLogic;
    }

    private void OnDisable()
    {
        _inputReader.HorizontalWasPressed -= Move;
        _inputReader.JumpWasPressed -= JumpLogic;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Platform _))
        {
            _isPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Platform _))
        {
            _isPlatform = false;
        }
    }

    private void Move(int moveDirection)
    {
        _rigidbody2D.velocity = new Vector2(moveDirection * _speed, _rigidbody2D.velocity.y);

        if (!_facingRight && moveDirection > 0 )
        {
             _facingRight = !_facingRight;
            
             transform.Rotate(0, 180f, 0);
        }
        else if (_facingRight && moveDirection < 0)
        {
            _facingRight = !_facingRight;
            
            transform.Rotate(0, 180f, 0);
        }

        if (moveDirection > 0 || moveDirection < 0)
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
        }
        else
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
        }
    }

    private void JumpLogic()
    {
        if (_isPlatform)
        {
            _rigidbody2D.velocity = (Vector2.up * _forceJump);
        }
    }
}
