using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject destinationObject;
    private Transform _destination;

    private NavMeshAgent _navMeshAgent;
    void Start()
    {
        _destination = destinationObject.GetComponent<Transform>();
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            _navMeshAgent.destination = _destination.position;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log(_navMeshAgent.isStopped);
            Debug.Log(_navMeshAgent.destination.x + " " + _navMeshAgent.destination.y +
                      " " + _navMeshAgent.destination.z);
        }
    }
}
