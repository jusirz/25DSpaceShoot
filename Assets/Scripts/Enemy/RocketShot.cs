using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShot : MonoBehaviour
{
    private readonly float _rocketSpeed = 12f;
    [SerializeField]
    private int _rocketType;
    [SerializeField]
    private GameObject _player;


    void Start()
    {
        _player = GameObject.Find("Player");
        
    }
    void Update()
    {
        RocketLaserMove();
    }
    private void RocketLaserMove()
    {
        if (_rocketType == 1)
        {
            transform.Translate(_rocketSpeed * Time.deltaTime * Vector3.down);
            if (transform.position.y < (-6.4f))
            {
                Destroy(this.gameObject);
            }
        }
        else if (_rocketType == 2)
        {
            transform.Translate(_rocketSpeed * Time.deltaTime * Vector3.up);
            if (transform.position.y > (7.65f))
            {
                Destroy(this.gameObject);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("shield"))
        {
            _player.GetComponent<Player>().Damage();
        }
    }
}
