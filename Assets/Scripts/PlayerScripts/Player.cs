using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private const string Money = "Money";
    private const string Medicine = "Medicine";
    
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump = 9f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WeaponPlayer _weapon;
    
    private Health _health;
    private Jumper _jumper;
    private Flipper _flipper;
    private MoverPlayer _moverPlayer;
    private PlayerAnimations _playerAnimations;
    private bool _isJump = false;
    private bool _isAttack = false;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _flipper = GetComponent<Flipper>();
        _jumper = GetComponent<Jumper>();
        _moverPlayer = GetComponent<MoverPlayer>();
    }

    private void OnEnable()
    {
        _inputReader.IsAttack += OnInputAttack;
        _inputReader.IsJump += OnInputJump;
    }

    private void OnDisable()
    {
        _inputReader.IsAttack -= OnInputAttack;
        _inputReader.IsJump -= OnInputJump;
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            Move(_inputReader.Direction);
        }
 
        if (_isJump && _detector.IsGround == false)
        {
            _isJump = false;
        }
        
        if (_isJump && _detector.IsGround)
        {
            Jump();
            
            _isJump = false;
        }

        if (_isAttack)
        {
            Attack();

            _isAttack = false;
        }

        if (_health.IsDead)
        {
            Dead();
        }
        
        _playerAnimations.MoveAnimation(_inputReader.Direction != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.Name == Money)
            {
                _wallet.AddMoney();
                
                item.ApplyTreatment();
            }
            else if (item.Name == Medicine)
            {
                Healing(item);
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnInputJump()
    {
        _isJump = true;
    }
    
    private void OnInputAttack()
    {
        _isAttack = true;
    }

    private void Healing(Item medicine)
    {
        _health.Healing(medicine.Heal);
        
        if (_health.IsHeal)
        {
            medicine.ApplyTreatment();

            _health.HealingIsOver();
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
