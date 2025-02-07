using System;
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
    [SerializeField] private WeaponPlayer _weapon;
    
    private Jumper _jumper;
    private Flipper _flipper;
    private MoverPlayer _moverPlayer;
    private PlayerAnimations _playerAnimations;
    private int _dead = 0;
    private int _heal = 25;
    private int _maxHealth = 100;
    private bool _isJump = false;
    private bool _isAttack = false;

    public int Health => _health;

    public event Action<int> WasHeal;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= _dead)
        {
            Dead();
        }
    }
    
    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        _flipper = GetComponent<Flipper>();
        _jumper = GetComponent<Jumper>();
        _moverPlayer = GetComponent<MoverPlayer>();
    }

    private void OnEnable()
    {
        _inputReader.IsAttack += WasInputAttack;
        _inputReader.IsJump += WasInputJump;
    }

    private void OnDisable()
    {
        _inputReader.IsAttack -= WasInputAttack;
        _inputReader.IsJump -= WasInputJump;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            Move(_inputReader.Direction);
        }
 
        if (_isJump && _detector.ScanSoil() == false)
        {
            _isJump = false;
        }
        
        if (_isJump && _detector.ScanSoil())
        {
            Jump();
            
            _isJump = false;
        }

        if (_isAttack)
        {
            Attack();

            _isAttack = false;
        }
        
        _playerAnimations.MoveAnimation(_inputReader.Direction != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Money>(out _))
        {
            _wallet.AddMoney();
            
            if(other.TryGetComponent(out Item item))
            {
                item.ApplyTreatment();
            }
        }
        else if(other.TryGetComponent(out Item item))
        {
            Healing(item);
        }
    }

    private void WasInputJump(bool jump)
    {
        _isJump = jump;
    }
    
    private void WasInputAttack(bool attack)
    {
        _isAttack = attack;
    }

    private void Healing(Item medicine)
    {
        if (_health < _maxHealth)
        {
            _health += _heal;

            medicine.ApplyTreatment();

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            
            WasHeal?.Invoke(_health);
        }
    }

    private void Attack()
    {
        _weapon.gameObject.SetActive(true);
        _weapon.LaunchAttack();
    }

    private void Move(float direction)
    {
        _playerAnimations.MoveAnimation(direction != 0);
        _moverPlayer.Move(direction, _speed);
        _flipper.LockAtTarget(direction);
    }

    private void Jump()
    {
        _jumper.Jump(_forceJump);
    }
    
    private void Dead()
    {
        Destroy(gameObject);
    }
}
