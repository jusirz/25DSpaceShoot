using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    private bool _isDead = false;
    [SerializeField]
    private float _swarmSpeed = 2f;
    [SerializeField]
    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == false)
        {
            SwarmMove();
        }

    }

    private void SwarmMove()
    {
        transform.Translate(Vector3.down * _swarmSpeed * Time.deltaTime);
        if (transform.position.y <= -9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = _player.GetComponent<Player>();
        if (other.CompareTag("Player"))
        {
            player.Damage();
            Destroy(this.gameObject);

        }
        else if (other.CompareTag("Laser"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
