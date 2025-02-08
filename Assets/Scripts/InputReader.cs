using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private float _direction;

    public event Action IsJump;
    public event Action IsAttack;

    public float Direction => _direction;

    private void Update()
    {
        _direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
        {
            IsJump?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            IsAttack?.Invoke();
        }
    }
}
