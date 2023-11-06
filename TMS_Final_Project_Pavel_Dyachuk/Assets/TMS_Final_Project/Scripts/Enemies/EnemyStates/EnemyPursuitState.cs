using Platformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuitState : BaseEnemyState
{
    [SerializeField]
    private float pursuiteSpeed = 1f;

    private Transform _playerPosition;
    private bool _isPursuing = false;

    public override void Enter()
    {
        Debug.Log("Entered Pursuit State");
        _isPursuing = true;
        _playerPosition = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    public override void Exit()
    {
        Debug.Log("Left Pursuit State");
        _isPursuing = false;

    }

    private void Update() 
    { 
        if(_isPursuing) 
        { 
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition.transform.position, pursuiteSpeed * Time.deltaTime); 
        }

    }


}
