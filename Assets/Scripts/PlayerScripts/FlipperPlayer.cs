using UnityEngine;

public class FlipperPlayer : MonoBehaviour
{
    private bool _facingRight = true;
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);

    public void LockAtTarget(float moveDirection)
    {
        if (_facingRight == false && moveDirection > 0 || _facingRight && moveDirection < 0)
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;
        }
    }
}
