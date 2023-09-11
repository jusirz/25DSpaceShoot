using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyYMove = 4;
    private float posXRand;
    

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(enemyYMove * Time.deltaTime * Vector3.down);
        if (transform.position.y < -5.2f)
        {
            posXRand = Random.Range(-9.45f, 9.67f);
            transform.position = new Vector3(posXRand, 7.18f, transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();   
            }
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {  
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }


    }
}
