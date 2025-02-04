using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    private int _toTheRight = 1;
    private int _stop = 0;
    private int _toTheLeft = -1;

    public event Action<int> HorizontalWasPressed;
    public event Action JumpWasPressed;

    private void Update()
    {
        if (Input.GetAxis(Horizontal) > 0)
        {
            HorizontalWasPressed?.Invoke(_toTheRight);
        }
        else if (Input.GetAxis(Horizontal) < 0)
        {
            HorizontalWasPressed?.Invoke(_toTheLeft);
        }
        else if(Input.GetAxis(Horizontal) == 0)
        {
            HorizontalWasPressed?.Invoke(_stop);
        }
        
        if (Input.GetAxis(Jump) > 0)
        {
            JumpWasPressed?.Invoke();
        }
    }
}
