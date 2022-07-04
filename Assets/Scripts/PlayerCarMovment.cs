using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerCarMovment : MonoBehaviour
{
    public float carHorizontalSpeed= 8;
    private Vector3 carPosition;
    public float maxDurability = 100f;
    [HideInInspector]
    public float durability;

    [Header("A.I. settings")]
    public bool autoPlay = true;
    [Range(0f, 3f)]
    public float fowardRideTime;
    //[HideInInspector]
    static public int mapStep = 1;
    static int[] carMoveMap = new int[300];
    static public int itteration = 1;
    static public int decisionsBest = 0;
    [Range(1f,4f)]
    public float GameSpeed = 1f;
    [Range(0f, 1f)]
    public float LerpValue = 0.5f;
    private GameObject LerpStart;
    private GameObject LerpEnd;

    private float delayTime =0;
    private float[] possibleAIPositions = new float[4] {0f, 0.33f, 0.66f, 1f }; //{ -5.4f, -2.1f, 1.9f, 5.6f }; //pozycje x na pasach;




    private void Start()
    {
        LerpStart = GameObject.FindWithTag("Start");
        LerpEnd = GameObject.FindWithTag("End");
        mapStep = 1;
        carPosition = this.gameObject.transform.position;
        if (autoPlay)
        {
            Time.timeScale = GameSpeed;
            maxDurability = 999;
        }
        if (!autoPlay)
        {
            if (Time.timeScale > 1)
            {
                Time.timeScale = 1;
            }
            maxDurability = 100;
        }

        durability = maxDurability;

        // zape³nienie tabeli wartoœciami 9 domyœlnymi do zmiany 
        if (itteration == 1 && File.Exists(Application.persistentDataPath + "/aidata.sav") == false)
        {
            resetTable();
        }
        else if (File.Exists(Application.persistentDataPath + "/aidata.sav"))
        {
            LoadProgress();
        }




    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveProgress();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            LoadProgress();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowArrayofMoves();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (AudioListener.volume != 0)
            AudioListener.volume =0;
            else if (AudioListener.volume == 0)
                AudioListener.volume = 1;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetTable();
        }
            if (!autoPlay)
        {
            carPosition.x += Input.GetAxis("Horizontal") * carHorizontalSpeed * Time.deltaTime;
            carPosition.x = Mathf.Clamp(carPosition.x, -6.7f, 6.7f);
            this.gameObject.transform.position = carPosition;
        }
        if (autoPlay)
        {
            if (durability < 990)
            {
                Debug.Log("Zakoñczono iteracje: " + itteration + " S.I. podje³a " + mapStep + " decyzji");
                if (mapStep != 0)
                {
                    carMoveMap[mapStep] = 9;
                    carMoveMap[mapStep-1] = 9;
                }
                if (mapStep > decisionsBest)
                {
                    decisionsBest = mapStep;
                }
                itteration++;
                mapStep = 1;
                SaveProgress();
                SceneManager.LoadScene(2);
                //zrobiæ zapisz do pliku !!!!
            }
            //autoporusznanie wed³ug tabeli ruchów generowanej losowo  
            if (carMoveMap.Length <= mapStep) // zabezpieczenie tabeli pêtl¹ 
            {
                mapStep = 1;
            }
            if (carMoveMap[mapStep] == 9)
            {
                carMoveMap[mapStep] = UnityEngine.Random.Range(0, 4);

            }
            delayTime -= Time.deltaTime;


            if (delayTime < 0)
            {


                transform.position = Vector3.Lerp(LerpStart.transform.position, LerpEnd.transform.position, LerpValue);
                Mathf.Clamp(carPosition.x, -6.5f, 6.5f);

                if (carMoveMap[mapStep] == carMoveMap[mapStep - 1])
                {
                    mapStep++;
                    delayTime = fowardRideTime;
                }
                else if (possibleAIPositions[carMoveMap[mapStep]] > possibleAIPositions[carMoveMap[mapStep - 1]])
                {
                    LerpValue += (Time.deltaTime / carHorizontalSpeed);
                    if (LerpValue >= possibleAIPositions[carMoveMap[mapStep]])
                    {
                        mapStep++;
                        delayTime = 0.01f;
                    }
                }
                else if (possibleAIPositions[carMoveMap[mapStep]] < possibleAIPositions[carMoveMap[mapStep - 1]])
                {
                    LerpValue -= (Time.deltaTime / carHorizontalSpeed);
                    if (LerpValue <= possibleAIPositions[carMoveMap[mapStep]])
                    {
                        mapStep++;
                        delayTime = 0.01f;

                    }
                }

            }

            /* if (carPosition.x > possibleAIPositions[carMoveMap[mapStep]]) // automatycznie w prawo 
             {
                 carPosition.x += -1 * carHorizontalSpeed * Time.deltaTime;
                 carPosition.x = Mathf.Clamp(carPosition.x, -6.7f, 6.7f);
                 this.gameObject.transform.position = carPosition;

                 if (carPosition.x >= -5.42f && carPosition.x <= -5.38f ||
                     carPosition.x >= -2.12f && carPosition.x <= -2.08f ||
                     carPosition.x >= 1.88f && carPosition.x <= 1.92f ||
                     carPosition.x >= 5.58f && carPosition.x <= 5.62f)
                 {
                     delayTime -= Time.deltaTime;
                     if (delayTime < 0)
                     {
                         mapStep++;
                         if (carMoveMap[mapStep] == 9)
                         {
                             carMoveMap[mapStep] = UnityEngine.Random.Range(0, 3);
                         }
                         if (carMoveMap[mapStep] == carMoveMap[mapStep - 1])
                         {
                             delayTime = fowardRideTime;
                         }
                         else
                         {
                             delayTime = decisionDelay;
                         }
                     }

                 }
             }

             else if (carPosition.x < possibleAIPositions[carMoveMap[mapStep]]) // automatycznie w prawo 
             {
                 carPosition.x += 1 * carHorizontalSpeed * Time.deltaTime;
                 carPosition.x = Mathf.Clamp(carPosition.x, -6.7f, 6.7f);
                 this.gameObject.transform.position = carPosition;

                 if (carPosition.x >= -5.42f && carPosition.x <= -5.38f ||
                     carPosition.x >= -2.12f && carPosition.x <= -2.08f ||
                     carPosition.x >= 1.88f && carPosition.x <= 1.92f ||
                     carPosition.x >= 5.58f && carPosition.x <= 5.62f)
                 {
                     delayTime -= Time.deltaTime;
                     if (delayTime < 0)
                     {
                         mapStep++;
                         if (carMoveMap[mapStep] == 9)
                         {
                             carMoveMap[mapStep] = UnityEngine.Random.Range(0, 3);
                         }
                         if (carMoveMap[mapStep] == carMoveMap[mapStep - 1])
                         {
                             delayTime = fowardRideTime;
                         }
                         else
                         {
                             delayTime = decisionDelay;
                         }
                     }

                 }

             }*/

        }

        }
    public void resetTable()
    {
        for (int i = 0; i < carMoveMap.Length; i++)
        {
            carMoveMap[i] = 9;
        }
        carMoveMap[0] = 0;
        itteration = 1;
        decisionsBest = 0;
        Debug.Log("Proces uczenia S.I. usuniêty");


    }

    public void SaveProgress()
    {
        Debug.Log("Learn save sucessed");
        FileStream savefile = File.Create(Application.persistentDataPath + "/aidata.sav");

        SaveData data = new SaveData();
        data.arrayOfMoves = carMoveMap;
        data.savebestValueDecisions = decisionsBest;
        data.saveIterationCount = itteration;

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(savefile, data);
        savefile.Close(); 
    }

    public void LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/aidata.sav"))
        {
            Debug.Log("Learn progress loaded");
            FileStream savefile = File.Open(Application.persistentDataPath + "/aidata.sav", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            SaveData data = (SaveData)bf.Deserialize(savefile);
            carMoveMap = data.arrayOfMoves;
            decisionsBest = data.savebestValueDecisions;
            itteration = data.saveIterationCount;
        }
        else
        {
            Debug.Log("Save file not found");
        }
    }
    public void ShowArrayofMoves()
    {
        for(int i = 0; i < 20; i++)
        {
            Debug.Log(i + ": " + carMoveMap[i]);
        }
    }


    }
[Serializable]
class SaveData
{
    public int[] arrayOfMoves = new int[300];
    public int savebestValueDecisions;
    public int saveIterationCount;
}

