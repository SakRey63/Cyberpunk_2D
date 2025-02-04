using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private bool _facingRight = true;
    private float _epsilon = 0.1f;
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    public event Action EnemyWasReachingTarget;
    
    public void Move (Vector3 target, float speed)
    {
        if (_facingRight == false && transform.position.x < target.x || _facingRight && transform.position.x > target.x )
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if ((transform.position - target).sqrMagnitude < _epsilon)
        {
            EnemyWasReachingTarget?.Invoke();
        }
    }
}
