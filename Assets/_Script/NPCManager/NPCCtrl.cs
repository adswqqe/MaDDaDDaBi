using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCtrl : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    //
    Transform spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Initialization(Transform spawnPos, Transform target)
    {
        this.spawnPos = spawnPos;
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        agent.SetDestination(target.position);

        if(!agent.pathPending)
        {
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.0f)
                    if (target == spawnPos)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        target = spawnPos;
                    }
            }
        }
    }
}
