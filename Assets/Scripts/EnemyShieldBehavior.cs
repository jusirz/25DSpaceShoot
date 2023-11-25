using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            _enemy.GetComponent<Enemy>().EnemyShieldChange();
            Destroy(this.gameObject, .3f);
        }
        else if (collision.CompareTag("Player"))
        {
            _enemy.GetComponent<Enemy>().EnemyShieldChange();
            Destroy(this.gameObject, .3f);
        }
    }
}
