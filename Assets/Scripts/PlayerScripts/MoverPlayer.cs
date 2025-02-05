using UnityEngine;

public class MoverPlayer : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Move(float moveDirection, float speed)
    {
        _rigidbody2D.velocity = new Vector2(moveDirection * speed, _rigidbody2D.velocity.y);
    }
}
