using UnityEngine;

[RequireComponent(typeof(Mover)), RequireComponent(typeof(Patroller)), RequireComponent(typeof(Flipper))]
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
        LookAround();
    }

    private void LookAround()
    {
        if (_scanner.CountEnemy == 0 )
        {
            _patroller.ContinuePatrolling();
        }
        else
        {
            _flipper.PursueTarget(_scanner.Target);
        }
    }
}
