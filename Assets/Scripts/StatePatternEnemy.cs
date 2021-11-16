using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatternEnemy : MonoBehaviour
{

    public float sleepingDuration;
    public float eatingDuration;
    public float maxSleepTime;
    public float eatingRotateSpeed;

    public GameObject dog;

    public MeshRenderer indicator;

    public GameObject shelter;

    public float energy;
    public float maxEnergy;
    public float timeCounter;
    public int timeOfDay;
    public bool inShelter;
    public float lastMeal;
    public GameObject foodTarget;
    public float distanceToDog;

    public AI_Curves AI_Curves;
    public GUIStyle myStyle;

    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform walkTarget;
    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public NormalState normalState;
    [HideInInspector]
    public HungryState hungryState;
    [HideInInspector]
    public SleepState sleepState;
    [HideInInspector]
    public RunAwayState runAwayState;

    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        normalState = new NormalState(this);
        hungryState = new HungryState(this);
        sleepState = new SleepState(this);
        runAwayState = new RunAwayState(this);
    }

    void Start()
    {
        currentState = normalState;
        myStyle.normal.textColor = Color.red;
        myStyle.fontSize = 16;
        AI_Curves = GetComponent<AI_Curves>();
    }


    void Update()
    {

        CalculateUtility();
        currentState.UpdateState();
        timeCounter += Time.deltaTime;
        lastMeal += Time.deltaTime;
        energy -= Time.deltaTime * 1.1f;

        if(timeCounter > 40)
        {
            timeCounter = 0;
            timeOfDay = (timeOfDay + 1) % 4;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private float CalculateNormalUrge()
    {
        return energy;
    }

    private float CalculateHungryUrge()
    {
        //Kaava nälän laskemiseen
        float urge = (100 - energy) / 2 + lastMeal;
        return urge;
    }

    private float CalculateSleepUrge()
    {
        float urge = 100 - energy + timeOfDay * 15;
        return urge;
    }

    private float CalculateRunAwayUrge()
    {
        return Vector3.Distance(transform.position, dog.transform.position);
    }

    private void CalculateUtility()
    {
        float[] urges = {
            AI_Curves.normal.Evaluate(CalculateNormalUrge()),
            AI_Curves.hungry.Evaluate(CalculateHungryUrge()),
            AI_Curves.sleep.Evaluate(CalculateSleepUrge()),
            AI_Curves.runAway.Evaluate(CalculateRunAwayUrge())
        };

        int maxValue = 0;
        float max = urges[0];
        for(int i = 0; i <urges.Length; i++)
        {
            if(urges[i] > max)
            {
                max = urges[i];
                maxValue = i;
            }
        }
        if(maxValue == 0 && currentState != normalState) { currentState.ToNormalState(); }
        if (maxValue == 1 && currentState != hungryState) { currentState.ToHungryState(); }
        if (maxValue == 2 && currentState != sleepState) { currentState.ToSleepState(); }
        if (maxValue == 3 && currentState != runAwayState) { currentState.ToRunAwayState(); }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 400, 20), "Normal: Urge: " + CalculateNormalUrge() + " Utility: " + AI_Curves.normal.Evaluate(CalculateNormalUrge()), myStyle);
        GUI.Label(new Rect(10, 30, 400, 20), "Hungry: Urge: " + CalculateHungryUrge() + " Utility: " + AI_Curves.hungry.Evaluate(CalculateHungryUrge()), myStyle);
        GUI.Label(new Rect(10, 50, 400, 20), "Sleep: Urge: " + CalculateSleepUrge() + " Utility: " + AI_Curves.sleep.Evaluate(CalculateSleepUrge()), myStyle);
        GUI.Label(new Rect(10, 70, 400, 20), "RunAway: Urge: " + CalculateRunAwayUrge() + " Utility: " + AI_Curves.runAway.Evaluate(CalculateRunAwayUrge()), myStyle);
        GUI.Label(new Rect(10, 90, 400, 20), "TimeCounter: " + timeCounter + " Time of day: " + timeOfDay, myStyle);


    }
}
