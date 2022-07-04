using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarBehaviour : MonoBehaviour
{
    [Header("Police Car Lights")]
    public Light redLight;
    public Light blueLight;
    public float lightDelay;
    [Header("Police Car Shoot")]
    public GameObject bullet;
    public float shootingSeriesDelay;
    public float singleShotDelay;
    public int bulletsInSeries;

    [Header("Police Car Movment")]
    //[HideInInspector]
    public bool isLeft;
    public float policeCarVerticalSpeed;
    public GameObject explosion;
    public AudioClip shootSound;
    

    [HideInInspector]
    public int pointsPerCar;
    private float lightShowDelay;
    private float shootDelay;
    private Vector3 policaCarPos;
    private GameObject bulletObject;
    private AudioSource audioS;

    private void Start()
    {
        shootDelay = shootingSeriesDelay;
        lightShowDelay = 2*lightDelay;
        audioS = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        lightShowDelay -= Time.deltaTime;
        if(lightShowDelay > lightDelay)
        {
            blueLight.enabled = false;
            redLight.enabled = true;
        }
        else if (lightShowDelay <= lightDelay && lightShowDelay > 0)
        {
            redLight.enabled =false;
            blueLight.enabled = true;
        }
        else if (lightShowDelay <= 0)
        {
            lightShowDelay = 2 * lightDelay;
        }

        if(gameObject.transform.position.y < -6.9)
        {
            gameObject.transform.Translate(new Vector3(0, 1, 0) * policeCarVerticalSpeed * Time.deltaTime);
        }
        else
        {
            shootDelay -= Time.deltaTime;
            if(shootDelay <= 0 && GameObject.FindWithTag("Untouchable") == false)
            {
                StartCoroutine("Shoot");
                shootDelay = shootingSeriesDelay;
            }
        }
    }

    IEnumerator Shoot()
    {
        for (int i = bulletsInSeries; i>0; i--)
        {
            audioS.PlayOneShot(shootSound, 3f);
            bulletObject = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            if (isLeft == true)
            {
                bulletObject.GetComponent<Bullet>().Direction = 1;
            }
            else if (isLeft == false)
            {
                bulletObject.GetComponent<Bullet>().Direction = -1;
            }
            yield return new WaitForSeconds(singleShotDelay);
        }

    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "Barrier")
        {
            if (isLeft)
            {
                WaveManager.isLeft = false;
            }
            else if (!isLeft)
            {
                WaveManager.isRight = false;
            }
            PointsManager.points += pointsPerCar;
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
