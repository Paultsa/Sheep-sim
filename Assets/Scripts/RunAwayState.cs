using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayState : IEnemyState
{
    private StatePatternEnemy sheep;

    public RunAwayState(StatePatternEnemy statePatternEnemy)
    {
        sheep = statePatternEnemy;
    }

    public void UpdateState()
    {
        RunAway();
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void ToHungryState()
    {
        sheep.currentState = sheep.hungryState;
    }

    public void ToNormalState()
    {
        sheep.currentState = sheep.normalState;
    }

    public void ToRunAwayState()
    {
    }

    public void ToSleepState()
    {
        sheep.currentState = sheep.sleepState;
    }

    private void RunAway()
    {
        sheep.indicator.material.color = Color.red;
        sheep.energy -= Time.deltaTime * 3;
        Vector3 dogToSheep = sheep.dog.transform.position - sheep.transform.position;
        sheep.navMeshAgent.destination = sheep.transform.position - dogToSheep;
        float speed = 20 - Vector3.Distance(sheep.dog.transform.position, sheep.transform.position);
        if(speed < 2)
        {
            speed = 2;
        }
        sheep.navMeshAgent.speed = speed;
        sheep.navMeshAgent.isStopped = false;
    }


}
