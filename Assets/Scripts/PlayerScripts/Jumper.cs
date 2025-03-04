using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _forceJump = 9f;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody2D.velocity = (Vector2.up * _forceJump);
    }
}