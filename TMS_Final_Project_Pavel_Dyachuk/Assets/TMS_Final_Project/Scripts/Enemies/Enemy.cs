using Platformer.Common;
using Platformer.Player;
using UnityEngine;

namespace Platformer.Enemies
{
    public class Enemy : MonoBehaviour, IDamageSource
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private int _enemyHealth;

        public int Damage => _damage;

        public void EnemyTakeDamage(int damage)
        {
            _enemyHealth -= damage;
            //Debug.Log(_enemyHealth);
            if (_enemyHealth < 1)
            {
                PlayEnemyDeathSound();
                Destroy(this.gameObject);
            }
            else PlayEnemyHitSound();
        }
        public virtual void PlayEnemyDeathSound()
        {
            SoundManager.Sound_Manager.PlayEnemyDeathSound();
        }
        public virtual void PlayEnemyHitSound()
        {
            SoundManager.Sound_Manager.PlayEnemyHitSound();
        }
    }
}