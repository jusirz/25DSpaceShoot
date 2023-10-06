using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private float _enemyLaserSpeed = 7;
    [SerializeField]
    private GameObject _player;
    private Collider2D _playerCollider;
    private Player _playerPlayer;

    void Update()
    {
        EnemyLaserMove();
        _player = GameObject.Find("Player");
        _playerCollider = _player.GetComponent<Collider2D>();
        _playerPlayer = _player.GetComponent<Player>();
    }

    private void EnemyLaserMove()
    {
        transform.Translate(_enemyLaserSpeed * Vector3.down * Time.deltaTime);
        if (transform.position.y < (-6.4f))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _playerPlayer.Damage();
            Destroy(this.gameObject);
            Debug.Log("Enemy Laser Collided with player");
        }
        else
        {
            Debug.Log("Something else was hit");
        }
    }
}
