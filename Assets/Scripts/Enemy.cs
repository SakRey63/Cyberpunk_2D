using UnityEngine;

[RequireComponent(typeof(EnemyMover)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(Flipper))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    
    private Patroller _patroller;
    private Flipper _flipper;
    
    private void Awake()
    {
        _patroller = GetComponent<Patroller>();
        _flipper = GetComponent<Flipper>();
    }

    private void Update()
    {
        LookingAround();
    }

    private void LookingAround()
    {
        if (_scanner.CountEnemy > 0 )
        {
            _flipper.PursueTarget(_scanner.Target);
        }
        else
        {
            _patroller.ContinuePatrolling();
        }
    }
}
