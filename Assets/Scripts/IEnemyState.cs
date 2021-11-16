using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToNormalState();

    void ToHungryState();

    void ToSleepState();

    void ToRunAwayState();
}
