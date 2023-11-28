using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private readonly float _enemyLaserSpeed = 7;
    [SerializeField]
    private GameObject _player;


    void Update()
    {
        _player = GameObject.Find("Player");
      
        EnemyLaserMove();
    }

    private void EnemyLaserMove()
    {
        transform.Translate(_enemyLaserSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y < (-6.4f))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            _player.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
            Debug.Log("Enemy Laser Collided with player");
        }
        else if (other.CompareTag("shield"))
        {
            _player.GetComponent<Player>().Damage();
            Debug.Log("Shield was hit");
        }
    }
}
