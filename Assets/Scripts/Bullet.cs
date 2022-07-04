using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosion;
    public int bulletDamage;
    [HideInInspector]
    public int Direction;
    public float bulletSpeed;

    private void Update()
    {
        this.gameObject.transform.Translate(new Vector3(Direction,0,0) * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            obj.gameObject.GetComponent<PlayerCarMovment>().durability -= bulletDamage;
            GameObject spawnedExplosion = (GameObject)Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            spawnedExplosion.GetComponent<AudioSource>().enabled = false;
            spawnedExplosion.transform.localScale = new Vector3(0.2f,0.2f,1);
            Destroy(this.gameObject);
        }
        else if (obj.gameObject.tag == "Shield" || obj.gameObject.tag == "Barrier" || obj.gameObject.tag == "PoliceCar")
        {
            Destroy(this.gameObject);
        }

    }
}
