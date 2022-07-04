using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCarBehaviour : MonoBehaviour
{
    [Header("Settings at -> Game manager")]
    public GameObject bomb;
    [HideInInspector]
    public int bombsAmount;
    [HideInInspector]
    public int banditCarVerticalSpeed;
    [HideInInspector]
    public float banditCarHorizontalSpeed;
    [HideInInspector]
    public float bombsDelay;
    [HideInInspector]
    public int pointsPerCar;

    private float Delay;
    private GameObject playersCar;
    private Vector3 banditCarPos;

    private void Start()
    {
        playersCar = GameObject.FindWithTag("Player");
        Delay = bombsDelay;
    }
    private void FixedUpdate()
    {
        if (playersCar == null)
        {
            playersCar = GameObject.FindWithTag("Player");
        }
        else
        {
            banditCarPos = Vector3.Lerp(transform.position, playersCar.transform.position, Time.deltaTime * banditCarHorizontalSpeed);
            Mathf.Clamp(banditCarPos.x, -6.5f, 6.5f);
            transform.position = new Vector3(banditCarPos.x, transform.position.y, 0);
        }
    }
    private void Update()
    {
        if(playersCar == null)
        {
            playersCar = GameObject.FindWithTag("Player");
        }
        else
        {
            if(gameObject.transform.position.y > 10 && bombsAmount >0)
            {
                this.gameObject.transform.Translate(new Vector3(0, -1, 0) * banditCarVerticalSpeed * Time.deltaTime);
            }
            else if (bombsAmount <= 0)
            {
                this.gameObject.transform.Translate(new Vector3(0, 1,0) * banditCarVerticalSpeed * Time.deltaTime);
                if (gameObject.transform.position.y > 18f)

                {
                    PointsManager.points += pointsPerCar;
                    Destroy(this.gameObject);
                }
            }
            else
            {
               

                Delay -= Time.deltaTime;
                if (Delay <= 0 && bombsAmount > 5)
                {
                    Delay = bombsDelay;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
                else if(Delay <= 0 && bombsAmount <= 5 && bombsAmount > 0)
                {
                    Delay = bombsDelay / 2;
                    bombsAmount--;
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
