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
        _animator.SetBool(AnimatorData.Params.IsWalk, isPlaying);
    }
}