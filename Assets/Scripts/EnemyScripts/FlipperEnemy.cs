using UnityEngine;

public class FlipperEnemy : MonoBehaviour
{
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    private bool _facingRight = true;
    
    public void TurnInOppositeDirection(Vector2 target)
    {
        if (_facingRight == false && transform.position.x < target.x || _facingRight && transform.position.x > target.x )
        {
            _facingRight = !_facingRight;
        
            transform.rotation *= _lockAtTarget;
        }
    }
}
