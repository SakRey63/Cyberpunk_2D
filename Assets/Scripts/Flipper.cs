using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    private bool _facingRight = true;
    
    public void LockAtTarget(float direction)
    {
        if (_facingRight == false && direction > 0 || _facingRight && direction < 0)
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;
        }
    }
}
