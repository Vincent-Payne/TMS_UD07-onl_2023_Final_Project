using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using Platformer.Player;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField]
    private EnemyPursuitTrigger _enemyPursuitTrigger;
    [SerializeField]
    private EnemyPursuitTrigger _enemyPursuitStateStop;
    [SerializeField]
    private EnemyIdleState _enemyIdleState;
    [SerializeField]
    private EnemyPatrolState _enemyPatrolState;
    [SerializeField]
    private EnemyPursuitState _enemyPursuitState;
    [SerializeField]
    private float detectionDistance;


    private BaseEnemyState _currentState;
    private Dictionary<Type, BaseEnemyState> _states;

    private void Start()
    {
        _enemyPursuitTrigger.OnTriggeredPursuit += SwitchToPursuite;
        _enemyPursuitStateStop.OnTriggeredPursuitStop += SwitchToIdle;

        _enemyIdleState?.Init(this);
        _enemyPatrolState?.Init(this);
        _enemyPursuitState?.Init(this);

        _states = new Dictionary<Type, BaseEnemyState>()
            {
                { typeof(EnemyIdleState), _enemyIdleState },
                { typeof(EnemyPatrolState), _enemyPatrolState },
                { typeof(EnemyPursuitState), _enemyPursuitState },

            };

        SwitchTo(typeof(EnemyIdleState));
    }

    private void OnDestroy()
    {
        _enemyPursuitTrigger.OnTriggeredPursuit -= SwitchToPursuite;
        _enemyPursuitStateStop.OnTriggeredPursuitStop -= SwitchToIdle;
    }

    public void SwitchTo(Type newState)
    {
        _currentState?.Exit();
        _currentState = _states[newState];
        _currentState?.Enter();
    }

    public void SwitchToPursuite()
    {
        SwitchTo(typeof(EnemyPursuitState));
    }
    public void SwitchToIdle()
    {
        SwitchTo(typeof(EnemyIdleState));
    }

}
