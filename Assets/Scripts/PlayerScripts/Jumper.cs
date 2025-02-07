using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Jump(float force)
    {
        _rigidbody2D.velocity = (Vector2.up * force);
    }
}