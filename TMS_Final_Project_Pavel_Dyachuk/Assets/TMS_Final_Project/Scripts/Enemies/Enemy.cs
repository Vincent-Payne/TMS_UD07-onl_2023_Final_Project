using Platformer.Common;
using UnityEngine;

namespace Platformer.Enemies
{
    public class Enemy : MonoBehaviour, IDamageSource
    {
        [SerializeField]
        private int _damage;

        public int Damage => _damage;
    }
}