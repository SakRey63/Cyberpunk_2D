using System.Collections;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _delay = 2.0f;
    [SerializeField] private Animations _animator;
    [SerializeField] private Flipper _flipper;
    
    private int _numberPoint = 0;
    
    private EnemyMover _enemyMover;
    
    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
    }
    
    private IEnumerator TakeBreak()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);
        
        _numberPoint = ++_numberPoint % _points.Length;
       
        yield return delay;
        
        _enemyMover.ContinueMove();
    }
    
    public void ContinuePatroller()
    {
        SetNextTargetPosition();
    }
    
    private void SetNextTargetPosition()
    {
        if (_enemyMover.Finished)
        {
            _animator.MoveAnimation(false);
            
            StartCoroutine(TakeBreak());
        }
        else
        {
            _animator.MoveAnimation(true);
                        
            _enemyMover.Move(_points[_numberPoint].position);
            
            _flipper.ChooseDirection(_points[_numberPoint].position);
        }
    }
}