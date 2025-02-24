using UnityEngine;

[RequireComponent(typeof(Health)),RequireComponent(typeof(Stalker)), RequireComponent(typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private EnemyWeapon _weapon;
    
    private Health _health;
    private Patroller _patroller;
    private Stalker _stalker;
    
    private void Awake()
    {
        _health = GetComponent<Health>();
        _patroller = GetComponent<Patroller>();
        _stalker = GetComponent<Stalker>();
    }

    private void Update()
    {
        ChooseStatus();
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
    
    private void ChooseStatus()
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

        if (_health.IsDead)
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