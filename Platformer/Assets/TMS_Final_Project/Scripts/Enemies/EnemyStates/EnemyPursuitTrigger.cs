using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuitTrigger : MonoBehaviour
{
    public event Action OnTriggeredPursuit;
    public event Action OnTriggeredPursuitStop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Detected");
        OnTriggeredPursuit?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Player Left");
        OnTriggeredPursuitStop?.Invoke();
    }
}
