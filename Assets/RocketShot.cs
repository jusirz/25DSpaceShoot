using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShot : MonoBehaviour
{
    private readonly float _rocketSpeed = 12f;
    [SerializeField]
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RocketLaserMove();
    }

    private void RocketLaserMove()
    {
        transform.Translate(_rocketSpeed * Time.deltaTime * Vector3.down);
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
        }
        else if (other.CompareTag("shield"))
        {
            _player.GetComponent<Player>().Damage();
        }
    }
}
