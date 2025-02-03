using UnityEngine;

public class Player : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump;
    [SerializeField] private Wallet _wallet;

    private Vector2 _moveVector;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private Animator _animator;

    private bool _isFlipX;
    private bool _isPlatform;
    private float _direction;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Money _))
        {
            _wallet.AddMoney();
        }
    }

    private void Move()
    {
        _renderer.flipX = _isFlipX;
        
        _direction = Input.GetAxis(Horizontal);
        
        transform.Translate(_direction * _speed * Time.deltaTime, 0, 0);

        if (_direction != 0)
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
            
            if (_direction < 0)
            {
                _isFlipX = true;
            }
            else if(_direction > 0)
            {
                _isFlipX = false;
            }
        }
        else
        {
            _animator.SetBool(PlayerAnimatorData.Params.IsWalk, false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isPlatform)
            {
                _rigidbody2D.AddForce(Vector2.up * _forceJump, ForceMode2D.Impulse);
            }
        }
    }
}
