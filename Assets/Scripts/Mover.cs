using UnityEngine;

public class Mover : MonoBehaviour
{
    private bool _facingRight = true;
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    public void Move(int moveDirection, float speed)
    {
        _rigidbody2D.velocity = new Vector2(moveDirection * speed, _rigidbody2D.velocity.y);

        if (_facingRight == false && moveDirection > 0 || _facingRight && moveDirection < 0)
        {
            _facingRight = !_facingRight;

            transform.rotation *= _lockAtTarget;
        }

        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, moveDirection > 0 || moveDirection < 0);
    }
}
