using UnityEngine;

namespace Main
{
    public static class Animations
    {
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        public static readonly int HorizontalValue = Animator.StringToHash("DirectionVector");
        public static readonly int Death = Animator.StringToHash("Death");
        public static readonly int GameOver = Animator.StringToHash("GameOver");
        public static readonly int StartGame = Animator.StringToHash("StartGame");
        public static readonly int ThrowGranade = Animator.StringToHash("ThrowGranade");
    }
}

