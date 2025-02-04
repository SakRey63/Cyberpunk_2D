using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump = 9f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private Wallet _wallet;
    
    private Jumper _jumper;
    private Mover _mover;

    private void Awake()
    {
        _jumper = GetComponent<Jumper>();
        _mover = GetComponent<Mover>();
    }

    private void OnEnable()
    {
        _inputReader.HorizontalWasPressed += Move;
        _inputReader.JumpWasPressed += Jump;
    }

    private void OnDisable()
    {
        _inputReader.HorizontalWasPressed -= Move;
        _inputReader.JumpWasPressed -= Jump;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Money _))
        {
            _wallet.AddMoney();
            
            Destroy(other.gameObject);
        }
    }

    private void Move(int direction)
    {
        _mover.Move(direction, _speed);
    }

    private void Jump()
    {
        if (_detector.Count > 0)
        {
            _jumper.Jump(_forceJump);
        }
    }
}
