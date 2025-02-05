using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));public static readonly int IsJump = Animator.StringToHash(nameof(IsJump));
    }
}