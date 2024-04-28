using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyIdleState : BaseEnemyState
{
    [SerializeField]
    private float idleTimer = 5f;
    private Coroutine idleCorutine = null;
    public override void Enter()
    {
        Debug.Log("Entered Idle State");
        idleCorutine = StartCoroutine(EnemyIdle());
    }

    private IEnumerator EnemyIdle()
    {
        yield return new WaitForSeconds(idleTimer);
        Debug.Log("Left Idle State Corutine");
        _EnemyStateManager.SwitchTo(typeof(EnemyPatrolState));
    }

    public override void Exit()
    {
        Debug.Log("Left Idle State");
        StopCoroutine(idleCorutine);
    }

}
