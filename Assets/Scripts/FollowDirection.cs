using System;
using UnityEngine;
using UnityEngine.AI;

public class FollowDirection : MonoBehaviour
{
    
    private NavMeshAgent _agent;
    private Vector3	_direction;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetDirection(Vector3 destination)
    {
        if(_agent.isOnNavMesh)
        {
            _agent.SetDestination(destination);
        }
    }
    
    
}
