using Platformer.Common;
using Platformer.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    public Action<Collider2D> OnEnemyAttacked;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy") OnEnemyAttacked?.Invoke(collider);
    }
}
