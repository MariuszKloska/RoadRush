using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarDurabilityManager : MonoBehaviour
{
    public GameObject playerCarPrefab;
    public GameObject spawPoint;
    public TextMeshPro durabilityText;
    public GameObject[] hearts;
    public int lives;
    public GameObject EndGameScreen;
    [HideInInspector]
    public int maxLives;
    private GameObject playerCar;
    public GameObject panelAI;

    private void Start()
    {
        durabilityText.GetComponent<MeshRenderer>().sortingLayerName = "Durability";
        maxLives = lives;
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawPoint.transform.position, Quaternion.identity);


    }
    private void Update()
    {
        if(playerCar.GetComponent<PlayerCarMovment>().durability <= 0)
        {
            Destroy(playerCar);
            lives --;
            Destroy(hearts[lives]);
            if(lives > 0)
            {
                StartCoroutine("SpawnCar");

            }
            else if (lives <= 0)
            {
                Time.timeScale = 0;
                EndGameScreen.SetActive(true);
            }
        } else if (playerCar.GetComponent<PlayerCarMovment>().durability > playerCar.GetComponent<PlayerCarMovment>().maxDurability)
        {
            playerCar.GetComponent<PlayerCarMovment>().durability = playerCar.GetComponent<PlayerCarMovment>().maxDurability;
        }

        durabilityText.text = "Durability: " + playerCar.GetComponent<PlayerCarMovment>().durability +"/" + playerCar.GetComponent<PlayerCarMovment>().maxDurability;
    }
    IEnumerator SpawnCar()
    {
        playerCar = (GameObject)Instantiate(playerCarPrefab, spawPoint.transform.position, Quaternion.identity);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        playerCar.GetComponent<BoxCollider>().isTrigger = true;
        playerCar.tag = "Untouchable";
        yield return new WaitForSeconds(3);
        playerCar.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        playerCar.GetComponent<BoxCollider>().isTrigger = false;
        playerCar.tag = "Player";



    }
}
