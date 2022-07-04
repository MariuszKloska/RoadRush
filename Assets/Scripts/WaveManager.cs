using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{
    [Header("Wave 1 (Civil Cars)")]
    public float CivilcarSpawnDelay = 2f;
    public GameObject civilCar;
    public int civilCarsAmount;

    [Header("Wave 2 (Bandit Cars)")]
    public GameObject banditCar;
    public int bombsAmount;
    public int banditCarVerticalSpeed;
    public float banditCarHorizontalSpeed;
    public float bombsDelay;
    private GameObject spawnedBanditCar;
    private bool isSpawned;
    private bool isSecondSpawned;

    [Header("Wave 3 (Police Cars)")]
    public GameObject policeCar;
    public int policeCarsAmount;
    [HideInInspector]
    static public bool isLeft;
    [HideInInspector]
    static public bool isRight;
    private GameObject spawnedPoliceCar;

    [Header("Points")]
    public int pointsPerCivilCar;
    public int pointsPerBanditCar;
    public int pointsPerBomb;
    public int pointsPerPolicaCar;

    public GameObject endGameScreen;




    private float[] lanesArray = {-5.75f,-2.13f,1.95f,5,74f};

    private float spawnDelay;
    private int civilCarCount = 0;
    private int[] spawnerArray = {0,1,2,3,3,2,1,0,2,2,3,3,0,0,0,1,0,1,1,3,4};

    private void Start()
    {

        spawnDelay = CivilcarSpawnDelay;

    }
    private void Update()
    {
        spawnDelay -= Time.deltaTime;

        if (spawnDelay <= 0 && civilCarsAmount > 0)
        {
            spawnCar();
            spawnDelay = CivilcarSpawnDelay;
           
        }
        else if (civilCarsAmount <= 0 && isSpawned == false)
        {
            spawnBanditCar();
        }
        else if (civilCarsAmount <= 0 && isSpawned == true && isSecondSpawned == false)
        {
            spawnBanditCar();
        }
        else if (civilCarsAmount <= 0 && policeCarsAmount > 0 && spawnedBanditCar == null)
        {
            SpawnedPoliceCar();
        }
        else if (policeCarsAmount <=0  && !isLeft && !isRight)
        {
            Time.timeScale = 0;
            endGameScreen.SetActive(true);
        }


    }

    private void SpawnedPoliceCar()
    {
        Transform playerCarPosition;
        if(GameObject.FindWithTag("Player"))
        {
            playerCarPosition = GameObject.FindWithTag("Player").transform;
        }
        else if (GameObject.FindWithTag("Shield"))
        {
            playerCarPosition = GameObject.FindWithTag("Shield").transform;
        }
        else if (GameObject.FindWithTag("Untouchable"))
        {
            playerCarPosition = GameObject.FindWithTag("Untouchable").transform;
        }
        else
        {
            playerCarPosition = null;
        }
        if (playerCarPosition.position.x <= 0 && isRight == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(5.75f, -14f, 0), Quaternion.identity);
            isRight = true;
            if (spawnedPoliceCar != null)
            {
                spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = false;
            }
            policeCarsAmount--;
        }
        else if (playerCarPosition.position.x > 0 && isLeft == false)
        {
            spawnedPoliceCar = (GameObject)Instantiate(policeCar, new Vector3(-5.75f, -14f, 0), Quaternion.identity);
            isLeft = true;
            if (spawnedPoliceCar != null)
            {
                spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().isLeft = true;
            }
            policeCarsAmount--;
        }
        spawnedPoliceCar.GetComponent<PoliceCarBehaviour>().pointsPerCar = pointsPerPolicaCar;
    }

    private void spawnBanditCar()
    {


        if (!isSpawned)
        {

            spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(-5.75f, 15, 0), Quaternion.identity);
            isSpawned = true;

            spawnDelay = CivilcarSpawnDelay;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount = bombsAmount;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarHorizontalSpeed = banditCarHorizontalSpeed;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().banditCarVerticalSpeed = banditCarVerticalSpeed;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsDelay = bombsDelay;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().pointsPerCar = pointsPerBanditCar;
            spawnedBanditCar.GetComponent<BanditCarBehaviour>().bomb.gameObject.GetComponent<Bomb>().pointsPerBomb = pointsPerBomb;

        }
        else if (isSpawned && !isSecondSpawned && spawnedBanditCar.GetComponent<BanditCarBehaviour>().bombsAmount< 2)
        {
            if (spawnedBanditCar.transform.position.x < 0)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(5.75f, 15, 0), Quaternion.identity);
                isSecondSpawned = true;
            }
            else if (spawnedBanditCar.transform.position.x >= 0)
            {
                spawnedBanditCar = (GameObject)Instantiate(banditCar, new Vector3(-5.75f, 15, 0), Quaternion.identity);
                isSecondSpawned = true;
            }
        }


    }

    private void spawnCar()
    {
            if (spawnerArray[civilCarCount] == 0 || spawnerArray[civilCarCount] == 1)
        {
            GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[spawnerArray[civilCarCount]], 15, 0), Quaternion.Euler(new Vector3(0,0,180)));
            car.GetComponent<CivilCarBehaviour>().direction = 1;
            car.GetComponent<CivilCarBehaviour>().civilCarSpeed = 20;
            car.GetComponent<CivilCarBehaviour>().pointsPerCar = pointsPerCivilCar;
        }

            if (spawnerArray[civilCarCount] == 2 || spawnerArray[civilCarCount] == 3)
        {
           GameObject car = (GameObject)Instantiate(civilCar, new Vector3(lanesArray[spawnerArray[civilCarCount]], 15, 0), Quaternion.identity);
            car.GetComponent<CivilCarBehaviour>().pointsPerCar = pointsPerCivilCar;

        }

        civilCarCount++;
        if (spawnerArray.Length <= civilCarCount)
        {
            civilCarCount = 0;
        }
        civilCarsAmount--;

    }
}
