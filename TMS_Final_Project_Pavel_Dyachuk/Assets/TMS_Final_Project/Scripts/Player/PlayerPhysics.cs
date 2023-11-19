using System;
using UnityEngine;

namespace Platformer.Player
{
    public abstract class PlayerPhysics : MonoBehaviour
    {
        public abstract event Action<GameObject> OnCollided;
        public abstract Vector2 Velocity { get; }
        public abstract bool IsOnGround { get; }
        public abstract void Enable();
        public abstract void Disable();
        public abstract void Tick(bool isRunningLeft, bool isRunningRight, bool isJumping);
    }
}