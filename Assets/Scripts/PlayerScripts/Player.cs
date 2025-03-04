using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private WeaponPlayer _weapon;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private PlayerScaner _scaner;
    [SerializeField] private Flipper _flipper;
    [SerializeField] private Animations _animations;
    
    private Health _health;
    private Jumper _jumper;
    private MoverPlayer _moverPlayer;
    private bool _isJump = false;
    private bool _isAttack = false;
    private bool _isVamp = false;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _jumper = GetComponent<Jumper>();
        _moverPlayer = GetComponent<MoverPlayer>();
    }

    private void OnEnable()
    {
        _scaner.FoundTreatment += Heal;
        _inputReader.IsAttack += OnInputAttack;
        _inputReader.IsJump += OnInputJump;
        _inputReader.IsSpell += OnInputSpell;
    }

    private void OnDisable()
    {
        _scaner.FoundTreatment -= Heal;
        _inputReader.IsAttack -= OnInputAttack;
        _inputReader.IsJump -= OnInputJump;
        _inputReader.IsSpell -= OnInputSpell;
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

        if (_isVamp)
        {
            CastingVampirism();

            _isVamp = false;
        }
        
        _animations.MoveAnimation(_inputReader.Direction != 0);
    }
    
    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnInputSpell()
    {
        _isVamp = true;
    }

    private void OnInputJump()
    {
        _isJump = true;
    }
    
    private void OnInputAttack()
    {
        _isAttack = true;
    }

    private void Heal(Item medicine)
    {
        _health.Heal(medicine.Heal);
        
        if (_health.IsHeal)
        {
            medicine.ApplyTreatment();

            _health.UseHealing();
        }
    }

    private void CastingVampirism()
    {
        _vampirism.UseVampirism();
    }
    
    private void Attack()
    {
        _weapon.gameObject.SetActive(true);
        _weapon.LaunchAttack();
    }

    private void Move(float direction)
    {
        _animations.MoveAnimation(direction != 0);
        _moverPlayer.Move(direction);
        _flipper.LockAtTarget(direction);
    }

    private void Jump()
    {
        _jumper.Jump();
    }
    
    private void Dead()
    {
        Destroy(gameObject);
    }
}