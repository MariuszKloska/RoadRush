using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosion;
    public int bombDamage;
    public float bombSpeed;
    [HideInInspector]
    public int pointsPerBomb;

    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(0,-1,0)*bombSpeed* Time.deltaTime);
/*        if (gameObject.transform.position.y < -17)
        {
            Destroy(this.gameObject);
        }*/
    }
    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            PointsManager.points -= pointsPerBomb;
            obj.gameObject.GetComponent<PlayerCarMovment>().durability -= bombDamage;
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);   
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "Shield")
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "EndOfRoad")
        {
            PointsManager.points += pointsPerBomb;
            Destroy(this.gameObject);
        }

    }
}
