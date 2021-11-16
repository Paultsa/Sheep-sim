using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Curves : MonoBehaviour
{
    public AnimationCurve normal = AnimationCurve.Linear(0, 0, 100, 1);
    public AnimationCurve hungry = AnimationCurve.Linear(0, 0, 100, 1);
    public AnimationCurve sleep = AnimationCurve.Linear(0, 0, 100, 1);
    public AnimationCurve runAway = AnimationCurve.Linear(0, 0, 100, 1);
}
