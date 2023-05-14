using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMY_STATE { IDLE, PATROL, CHASE }

public class ChaseAI : MonoBehaviour
{
    public ENEMY_STATE currentState;
    public Transform[] waypoints;
    public Transform target;
    public float chaseSpeed = 5f;
    public float patrolSpeed = 2f;
    public float chaseDistance = 10f;
    public float idleTime = 3f;

    private int currentWaypointIndex;
    private float idleTimer;

    void Start()
    {
        currentState = ENEMY_STATE.IDLE;
        currentWaypointIndex = 0;
        idleTimer = idleTime;
    }

    void Update()
    {
        switch (currentState)
        {
            case ENEMY_STATE.IDLE:
                IdleState();
                break;
            case ENEMY_STATE.PATROL:
                PatrolState();
                break;
            case ENEMY_STATE.CHASE:
                ChaseState();
                break;
        }
    }

    void IdleState()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            currentState = ENEMY_STATE.PATROL;
            idleTimer = idleTime;
        }
    }

    void PatrolState()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = currentWaypoint.position - transform.position;
        transform.Translate(direction.normalized * patrolSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.2f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        if (CanSeeTarget())
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            currentState = ENEMY_STATE.CHASE;
        }
    }

    void ChaseState()
    {
        if (target == null)
        {
            currentState = ENEMY_STATE.PATROL;
            return;
        }

        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * chaseSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) > chaseDistance)
        {
            currentState = ENEMY_STATE.PATROL;
        }
    }

    bool CanSeeTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
        }

        return false;
    }
}
