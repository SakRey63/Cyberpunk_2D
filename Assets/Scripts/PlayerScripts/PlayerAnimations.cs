using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void MoveAnimation(bool isPlaying)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsWalk, isPlaying);
    }

    public void AttackAnimation(bool isPlaying)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsAttack, isPlaying);
    }
}