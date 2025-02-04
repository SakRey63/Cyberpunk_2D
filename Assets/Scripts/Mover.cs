using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private bool _facingRight = true;
    private float _epsilon = 0.1f;

    public event Action EnemyWasReachingTarget;
    
    public void Move (Vector3 target, float speed)
    {
        if (!_facingRight && transform.position.x < target.x)
        {
            _facingRight = !_facingRight;
            
            transform.Rotate(0, 180f, 0);
        }
        else if(_facingRight && transform.position.x > target.x )
        {
            _facingRight = !_facingRight;
            
            transform.Rotate(0, 180f, 0);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if ((transform.position - target).sqrMagnitude < _epsilon)
        {
            EnemyWasReachingTarget?.Invoke();
        }
    }
}
