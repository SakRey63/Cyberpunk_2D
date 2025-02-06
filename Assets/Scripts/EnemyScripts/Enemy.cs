using UnityEngine;

[RequireComponent(typeof(EnemyMover)), RequireComponent(typeof(AttackEnemy)),RequireComponent(typeof(Harassment)), RequireComponent(typeof(FlipperEnemy)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(FlipperEnemy))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private int _health = 100;
    [SerializeField] private int _demage = 5;
    
    private Patroller _patroller;
    private Harassment _harassment;
    private AttackEnemy _attackEnemy;
    private int _dead = 0;

    public int Health => _health;
    
    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _harassment = GetComponent<Harassment>();
        _attackEnemy = GetComponent<AttackEnemy>();
    }

    private void Update()
    {
        LookingAround();
    }

    private void LookingAround()
    {
        if (_scanner.CountEnemy > 0 )
        {
            _harassment.PursueTarget(_scanner.Target);

            if (_harassment.IsCaughtTarget && _attackEnemy.IsAttack)
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
        _attackEnemy.Attack(_demage);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
    
    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        Debug.Log(name + " - Мое здоровье: " + _health);

        if (_health <= _dead)
        {
            Dead();
        }
    }
}
