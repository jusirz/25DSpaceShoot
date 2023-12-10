using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserDetection : MonoBehaviour
{
    private GameObject _enemy;

    void Start()
    {
        _enemy = transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            _enemy.GetComponent<Enemy>().LaserMovement();
            Debug.Log("Enemy Detected Laser");
        }
    }
}
