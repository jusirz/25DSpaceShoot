using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserDetection : MonoBehaviour
{
    private GameObject _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.parent.gameObject;
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
           _enemy.GetComponent<Enemy>().LaserMovement();
        }
    }
}
