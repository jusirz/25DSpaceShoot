using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpColliderBehavior : MonoBehaviour
{
    private GameObject _enemy;

    private void Start()
    {
        _enemy = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            _enemy.GetComponent<Enemy>().EnemyPickupTrigger();
        }
    }
}
