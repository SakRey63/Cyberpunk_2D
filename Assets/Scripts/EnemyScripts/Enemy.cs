using UnityEngine;

[RequireComponent(typeof(EnemyMover)),RequireComponent(typeof(Stalker)), RequireComponent(typeof(Flipper)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(Flipper))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyVision _enemyVision;
    [SerializeField] private EnemyWeapon _weapon;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _demage = 5;
    
    private Patroller _patroller;
    private Stalker _stalker;
    private int _dead = 0;
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        Debug.Log(name + " - Мое здоровье: " + _health);

        if (_health <= _dead)
        {
            Dead();
        }
    }
    
    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _stalker = GetComponent<Stalker>();
    }

    private void Update()
    {
        LookingAround();
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
            _patroller.ContinuePatrolling();
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
