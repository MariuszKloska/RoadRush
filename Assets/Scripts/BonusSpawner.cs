using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject[] bonuses;
    public float delay;

    private float delayTimer;
    private int arrayCounterType = 0;
    private int arrayCounter = 0;

    private int[] spawnerTypeArray = {0,2,1,0,0,2,2,1,2,0,0,2,1,1};
    private int[] spawnerArray = { 1, 2, 3, 3, 2, 1, 0, 2, 2, 3, 3, 0, 0, 0, 1, 0, 1, 1, 3, 4 };
    private float[] lanesArray = { -5.75f, -2.13f, 1.95f, 5, 74f };

    private void Start()
    {
        delayTimer = delay;
    }

    private void Update()
    {
        delayTimer -= Time.deltaTime;
        if (delayTimer <= 0)
        {
            delayTimer = delay;
            SpawnBonus();
        }
    }

    private void SpawnBonus()
    {
        Instantiate(bonuses[spawnerTypeArray[arrayCounterType]], new Vector3(lanesArray[spawnerArray[arrayCounter]], 15, 0), Quaternion.identity);

        arrayCounter++;
        arrayCounterType++;
        if (spawnerTypeArray.Length <= arrayCounterType)
        {
            arrayCounterType = 0;
        }
        if (spawnerArray.Length <= arrayCounter)
        {
            arrayCounter = 0;
        }
    }
}
