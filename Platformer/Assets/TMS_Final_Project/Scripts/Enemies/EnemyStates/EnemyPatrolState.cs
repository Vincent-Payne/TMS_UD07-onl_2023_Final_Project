using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPatrolState : BaseEnemyState
{
    [SerializeField]
    protected Transform[] _waypoints;

    [SerializeField]
    protected float _moveSpeed;


    private int _nextWaypointIndex;
    private Transform _nextWaypoint;
    //private Vector3 _enemyPosition;
    private bool _isMoving = false;

    private void Start()
    {
        if (_waypoints.Length > 0)
        {
            _nextWaypointIndex = 0;
            _nextWaypoint = _waypoints[_nextWaypointIndex];
        }
    }

    public override void Enter()
    {
        Debug.Log("Entered Patrol State");
        _isMoving = true;
    }

    public override void Exit()
    {
        Debug.Log("Left Patrol State");
        _isMoving = false;
    }

    private void Update()
    {
        if (_isMoving == true && _nextWaypoint != null && _waypoints.Length > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextWaypoint.position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _nextWaypoint.position) < 0.2f)
            {
                _nextWaypointIndex += 1;
                if (_nextWaypointIndex >= _waypoints.Length)
                {
                    _nextWaypointIndex = 0;
                }

                _nextWaypoint = _waypoints[_nextWaypointIndex];
                Debug.Log("Left Patrol State");
                _isMoving = false;
                _EnemyStateManager.SwitchTo(typeof(EnemyIdleState));
            }
        }
    }
}
