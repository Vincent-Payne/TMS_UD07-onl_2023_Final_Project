using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState : MonoBehaviour
{
    protected EnemyStateManager _EnemyStateManager;

    public virtual void Init(EnemyStateManager enemyStateManager)
    {
        _EnemyStateManager = enemyStateManager;
    }

    public virtual void Enter()
    {
        //gameObject.SetActive(true);
    }

    public virtual void Exit()
    {
        //gameObject.SetActive(false);
    }
}
