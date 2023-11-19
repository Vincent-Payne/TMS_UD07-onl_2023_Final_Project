using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Common;
using Platformer.Player;

namespace Platformer.Enemies
{
    public class EnemyBat : Enemy
    {
        public override void PlayEnemyDeathSound()
        {
            SoundManager.Sound_Manager.PlayEnemyBatDeathSound();
        }
        public override void PlayEnemyHitSound()
        {
            SoundManager.Sound_Manager.PlayEnemyBatHitSound();
        }
    }
}
