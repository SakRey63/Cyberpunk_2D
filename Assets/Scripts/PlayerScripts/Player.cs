using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump = 9f;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 10;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private Wallet _wallet;
    
    private JumperPlayer _jumperPlayer;
    private FlipperPlayer _flipperPlayer;
    private MoverPlayer _moverPlayer;
    private PlayerAnimations _playerAnimations;
    private AttackPlayer _attackPlayer;
    private int _dead = 0;

    public int Health => _health;
    
    private void Awake()
    {
        _attackPlayer = GetComponent<AttackPlayer>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _flipperPlayer = GetComponent<FlipperPlayer>();
        _jumperPlayer = GetComponent<JumperPlayer>();
        _moverPlayer = GetComponent<MoverPlayer>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            Move(_inputReader.Direction);
        }

        if (_inputReader.GetIsJump() && _detector.IsGround)
        { 
            Jump();
        }

        if (_inputReader.GetIsAttack() && _attackPlayer.IsAttack)
        { 
            Attack(); 
        }
        
        _playerAnimations.MoveAnimation(_inputReader.Direction != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Money>(out _))
        {
            _wallet.AddMoney();
        }
    }

    private void Attack()
    {
        _attackPlayer.Attack(_damage);
    }

    private void Move(float direction)
    {
        _playerAnimations.MoveAnimation(direction != 0);
        _moverPlayer.Move(direction, _speed);
        _flipperPlayer.LockAtTarget(direction);
    }

    private void Jump()
    {
        _jumperPlayer.Jump(_forceJump);
    }
    
    private void Dead()
    {
        Destroy(gameObject);
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= _dead)
        {
            Dead();
        }
    }
}
