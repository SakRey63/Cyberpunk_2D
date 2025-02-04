using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    
    private Mover _mover;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
    }

    public void PursueTarget(Vector2 target)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, true);
        
        _mover.Move(target, _speed);
    }
}
