using UnityEngine;

[RequireComponent(typeof(EnemyMover)),RequireComponent(typeof(Harassment)), RequireComponent(typeof(FlipperEnemy)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(FlipperEnemy))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    
    private Patroller _patroller;
    private Harassment _harassment;
    
    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _harassment = GetComponent<Harassment>();
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
        }
        else
        {
            _patroller.ContinuePatrolling();
        }
    }
}
