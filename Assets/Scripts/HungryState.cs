using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryState : IEnemyState
{
    private StatePatternEnemy sheep;
    public bool haveTarget;
    public float eatTimer;
    public bool eating;
    



    public HungryState(StatePatternEnemy statePatternEnemy)
    {
        sheep = statePatternEnemy;
    }

    public void UpdateState()
    {

        Eat();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Food"))
        {
            GameObject.Destroy(other);
            GameObject[] sheeps = GameObject.FindGameObjectsWithTag("Sheep");
            foreach(GameObject singleSheep in sheeps)
            {
                if(singleSheep.GetComponent<StatePatternEnemy>().foodTarget == other.gameObject)
                {
                    singleSheep.GetComponent<StatePatternEnemy>().foodTarget = SearchNewFoodTarget();
                }
            }

        }
    }

    public void ToHungryState()
    {
    }

    public void ToNormalState()
    {
        sheep.currentState = sheep.normalState;
    }

    public void ToRunAwayState()
    {
        sheep.currentState = sheep.runAwayState;
    }

    public void ToSleepState()
    {
        sheep.currentState = sheep.sleepState;
    }

    private void Eat()
    {
        sheep.indicator.material.color = Color.yellow;

        if(haveTarget == false)
        {
            sheep.foodTarget = SearchNewFoodTarget();
            sheep.navMeshAgent.destination = sheep.foodTarget.transform.position;
            sheep.navMeshAgent.speed = 3;
            sheep.navMeshAgent.isStopped = false;
            haveTarget = true;
        }

        if(sheep.navMeshAgent.remainingDistance <= sheep.navMeshAgent.stoppingDistance && !sheep.navMeshAgent.pathPending && !eating)
        {
            sheep.navMeshAgent.isStopped = true;
            eatTimer = 0;
            eating = true;
        }
        if(eating)
        {
            if(eatTimer >= sheep.eatingDuration)
            {
                sheep.lastMeal = sheep.lastMeal / 4;
                sheep.energy += 20;
                eating = false;
                haveTarget = false;
            }
            sheep.transform.Rotate(0, sheep.eatingRotateSpeed / Time.deltaTime, 0);
            eatTimer += Time.deltaTime;
        }
    }

    private GameObject SearchNewFoodTarget()
    {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        if(food.Length > 0)
        {
            GameObject target = food[Random.Range(0, food.Length)];
            return target;
        }
        else
        {
            sheep.energy -= 5;
            return null;
        }
    }
}
