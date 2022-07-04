using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuses : MonoBehaviour
{
    [Header("Type of bonus")]
    public bool isDurability;
    public bool isShield;
    public bool isSpeed;

    [Header("Bonuses settings")]
    public float bonusSpeed = 10f;

    [Header("Durablility Settings")]
    public float reapairPoints;

    [Header("Shield Settings")]
    public GameObject shield;
    private GameObject playerCar;
    private Vector3 playerCarPos;

    [Header("Speed Settings")]
    public float speedBoost;
    public float duration;
    private bool isActive = false;

    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, -1, 0) * bonusSpeed * Time.deltaTime);
/*        if (gameObject.transform.position.y < -17)
        {
            Destroy(this.gameObject);
        }*/
    }

    private void OnTriggerEnter(Collider obj)
    {
        if(obj.gameObject.tag == "Player" || obj.gameObject.tag == "Shield")
        {
            if (isDurability)
            {
                obj.gameObject.GetComponent<PlayerCarMovment>().durability += reapairPoints;
                Destroy(this.gameObject);
            } 
            else if (isShield)
            {
                playerCar = GameObject.FindWithTag("Player");
                obj.gameObject.tag = "Shield";
                playerCarPos = playerCar.transform.position;
                playerCarPos.y = -0.1f;
                GameObject shieldObj = (GameObject)Instantiate(shield, playerCarPos, Quaternion.identity);
                shieldObj.transform.parent = playerCar.transform;
                Destroy(this.gameObject);
            } 
            else if (isSpeed)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                isActive = true;
                StartCoroutine("SpeedBoostActivated");
            }
        } else if(obj.gameObject.tag == "EndOfRoad" && isActive == false)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator SpeedBoostActivated()
    {
        while(duration > 0)
        {
            duration -= Time.deltaTime/ speedBoost;
            Time.timeScale = speedBoost;
            yield return null;
        }
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }
}
