using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private float _direction;
    private bool _isJump;
    private bool _isAttack;

    public event Action<bool> IsJump;
    public event Action<bool> IsAttack;

    public float Direction => _direction;

    private void Update()
    {
        _direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
            
            IsJump?.Invoke(_isJump);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isAttack = true;
            
            IsAttack?.Invoke(_isAttack);
        }
    }
}
