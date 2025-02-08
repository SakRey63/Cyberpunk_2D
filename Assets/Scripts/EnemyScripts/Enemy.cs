using UnityEngine;

[RequireComponent(typeof(Health)),RequireComponent(typeof(Stalker)), RequireComponent(typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private EnemyWeapon _weapon;
    
    private Health _healthEnemy;
    private Patroller _patroller;
    private Stalker _stalker;
    
    private void Awake()
    {
        _healthEnemy = GetComponent<Health>();
        _patroller = GetComponent<Patroller>();
        _stalker = GetComponent<Stalker>();
    }

    private void Update()
    {
        LookingAround();
    }

    public void TakeDamage(int damage)
    {
        _healthEnemy.TakeDamage(damage);
    }
    
    private void LookingAround()
    {
        if (_enemyVision.IsDetected)
        {
            _stalker.PursueTarget(_enemyVision.Target);
            
            if (_stalker.IsCaughtTarget)
            {
                Attack();
            }
        }
        else
        {
            _patroller.ContinuePatroller();
        }

        if (_healthEnemy.IsDead)
        {
            Dead();
        }
    }

    private void Attack()
    {
        _weapon.gameObject.SetActive(true);
        _weapon.LaunchAttack();
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
