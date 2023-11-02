using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private readonly float _enemyLaserSpeed = 7;
    [SerializeField]
    private GameObject _player;
    private Player _playerPlayer;

    void Update()
    {
        EnemyLaserMove();
        _player = GameObject.Find("Player");
        _playerPlayer = _player.GetComponent<Player>();
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
            _playerPlayer.Damage();
            Destroy(this.gameObject);
            Debug.Log("Enemy Laser Collided with player");
        }
        else if (other.CompareTag("shield"))
        {
            _playerPlayer.Damage();
            Debug.Log("Shield was hit");
        }
    }
}
