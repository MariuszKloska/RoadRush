using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilCarBehaviour : MonoBehaviour
{
    public float crashDamage = 20f;
    public GameObject explosion;

    public float civilCarSpeed = 5f;
    public int direction = -1;
    [HideInInspector]
    public int pointsPerCar;

    private Vector3 civilCarPosition;

    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0, direction, 0) * civilCarSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision obj)
    {
        if(obj.gameObject.tag == "Player")
        {
            obj.gameObject.GetComponent<PlayerCarMovment>().durability -= crashDamage / 5;
        }
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            PointsManager.points -= pointsPerCar;
            obj.gameObject.GetComponent<PlayerCarMovment>().durability -= crashDamage;
            //Debug.Log("You hit civil car !");
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "Shield")
        {
            PointsManager.points += 2*pointsPerCar;
            //Debug.Log("You crash civil car with shield!");
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "EndOfRoad")
        {
            PointsManager.points += pointsPerCar;
            Destroy(this.gameObject);
        }
    }
}
