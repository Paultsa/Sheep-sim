using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : IEnemyState
{
    private StatePatternEnemy sheep;

    private bool sleeping;
    private bool haveTarget;
    private float sleepTimer;


    public SleepState(StatePatternEnemy statePatternEnemy)
    {
        sheep = statePatternEnemy;
    }

    public void UpdateState()
    {
        Sleep();
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void ToHungryState()
    {
        sheep.energy += sleepTimer;
        sleepTimer = 0;
        sheep.currentState = sheep.hungryState;
        
    }

    public void ToNormalState()
    {
        sheep.energy += sleepTimer;
        sleepTimer = 0;
        sheep.currentState = sheep.normalState;
    }

    public void ToRunAwayState()
    {
        sheep.energy += sleepTimer;
        sleepTimer = 0;
        sheep.currentState = sheep.runAwayState;
    }

    public void ToSleepState()
    {
    }

    private void Sleep()
    {
        sheep.navMeshAgent.speed = 1;
        sheep.indicator.material.color = Color.cyan;

        if (Vector3.Distance(sheep.shelter.transform.position, sheep.transform.position) > 20 && !haveTarget)
        {
            sheep.navMeshAgent.isStopped = true;
            sleeping = true;
        }
        else
        {
            if (!sheep.inShelter)
            {
                sheep.navMeshAgent.destination = sheep.shelter.transform.position;
                sheep.navMeshAgent.isStopped = false;
                haveTarget = true;
            }
        }

        if (sheep.navMeshAgent.remainingDistance <= sheep.navMeshAgent.stoppingDistance && !sheep.navMeshAgent.pathPending)
        {
            sheep.navMeshAgent.isStopped = true;
            haveTarget = false;
            sleeping = true;
        }
        if (sleeping && sheep.inShelter)
        {
            sleepTimer += Time.deltaTime * 5;
        }
        else if (sleeping && !sheep.inShelter)
        {
            sleepTimer += Time.deltaTime * 2;
        }
        if (sleepTimer > sheep.maxSleepTime)
        {
            ToNormalState();
        }
    }
}
