using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _forceJump = 9f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _detector;
    [SerializeField] private Wallet _wallet;
    
    private JumperPlayer _jumperPlayer;
    private FlipperPlayer _flipperPlayer;
    private MoverPlayer _moverPlayer;
    private PlayerAnimations _playerAnimations;

    private void Awake()
    {
        _playerAnimations = GetComponent<PlayerAnimations>();
        _flipperPlayer = GetComponent<FlipperPlayer>();
        _jumperPlayer = GetComponent<JumperPlayer>();
        _moverPlayer = GetComponent<MoverPlayer>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            Move(_inputReader.Direction);
        }

        if (_inputReader.GetIsJump() && _detector.IsGround)
        { 
            Jump();
        }
        
        _playerAnimations.MoveAnimation(_inputReader.Direction != 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Money>(out _))
        {
            _wallet.AddMoney();
        }
    }

    private void Move(float direction)
    {
        _playerAnimations.MoveAnimation(direction != 0);
        _moverPlayer.Move(direction, _speed);
        _flipperPlayer.LockAtTarget(direction);
    }

    private void Jump()
    {
        _jumperPlayer.Jump(_forceJump);
    }
}
