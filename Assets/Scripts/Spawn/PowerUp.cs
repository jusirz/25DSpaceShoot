using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 3.0f;
    [SerializeField]
    private int _powerupID; //0 = tripleshot 1 = speed 2 = shield
    [SerializeField]
    private AudioClip _powerUpSoundSource;
    private GameObject _player;

    private bool _closeToPlayer = false;


    private void Start()
    {
        _player = GameObject.Find("Player");
    }
    void Update()
    {
        PowerUpMovement();
        DistanceCheck();
    }

    private void PowerUpMovement()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_closeToPlayer == true)
            {
                MoveTowardsPlayer();
            }
        } 
        transform.Translate(_powerUpSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y <= -5.85f)
        {
            Destroy(this.gameObject);
        }
    }

    private void DistanceCheck()
    {
        float dist = 4f;
        if (_player != null)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < dist)
            {
                _closeToPlayer = true;
            }
            else if (Vector3.Distance(transform.position, _player.transform.position) > dist)
            {
                _closeToPlayer = false;
            }
        }

    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 300f * Time.deltaTime);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.CompareTag("Player") && player != null)
        {
            AudioSource.PlayClipAtPoint(_powerUpSoundSource, transform.position);
            switch (_powerupID)
            {
                case 0:
                    player.ActivateLaserChange(1);
                    break;
                case 1:
                    player.SpeedPower();
                    break;
                case 2:
                    if (player._activeShield == true)
                    {
                        return;
                    }
                    else
                    {
                        player.ShieldPower();
                    }
                    break;
                case 3:
                    player.AmmoIncrease(15);
                    break;
                case 4:
                    player.HealthIncrease();
                    break;
                case 5:
                    player.ActivateLaserChange(2);
                    break;
                case 6:
                    player.StartSlowDown();
                    break;
                case 7:
                    player.ActivateLaserChange(3);
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
        if (other.CompareTag("EnemyLaser"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }

    }


}



