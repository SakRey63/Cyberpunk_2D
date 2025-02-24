using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    private bool _facingRight = true;
    
    public void LockAtTarget(float direction)
    {
        if (_facingRight == false && direction > 0 || _facingRight && direction < 0)
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;

            _canvas.transform.rotation *= _lockAtTarget;
        }
    }
    
    public void MovementDirection(Vector2 point)
    {
        float right = 1;
        float left = -1;
        
        if (point.x < transform.position.x)
        {
            LockAtTarget(left);
        }
        else
        {
            LockAtTarget(right);
        }
    }
}
