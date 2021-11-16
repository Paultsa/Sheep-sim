using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowFood : MonoBehaviour
{

    public GameObject food;
    public float counter;
    public float maxCounter;


    void Start()
    {
        
    }

    void Update()
    {
        if(counter < maxCounter)
        {
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0;
            Instantiate(food, new Vector3(Random.Range(-15, 15), 0.5f, Random.Range(-15, 15)), Quaternion.identity);
        }
    }
}
