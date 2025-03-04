using UnityEngine;

public class MoverPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Move(float moveDirection)
    {
        _rigidbody2D.velocity = new Vector2(moveDirection * _speed, _rigidbody2D.velocity.y);
    }
}