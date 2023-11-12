using System;
using UnityEngine;

namespace Platformer.Player
{
    public abstract class PlayerView : MonoBehaviour
    {
        public abstract void Tick(Vector2 velocity, bool isOnGround);
        public abstract void PlayHitAnimation(Action onAnimationFinished);
        public abstract void PlayDieAnimation(Action onAnimationFinished);
        public abstract void PlayAttackAnimation(Action onAnimationFinished);
    }
}