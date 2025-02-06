using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private float _direction;
    private bool _isJump;
    private bool _isAttack;
    
    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    public bool GetIsAttack() => GetBoolAsTrigger(ref _isAttack);

    public float Direction => _direction;

    private void Update()
    {
        _direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isAttack = true;
        }
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
