using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
    }
}