using Platformer.Common;
using UnityEngine;

namespace Platformer.Enemies
{
    public class Bat : MonoBehaviour, IDamageSource
    {
        [SerializeField]
        private int _damage;

        public int Damage => _damage;
    }
}