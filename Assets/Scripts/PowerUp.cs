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

    void Update()
    {
        PowerUpMovement();
    }

    private void PowerUpMovement()
    {
        transform.Translate(_powerUpSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y <= -5.85f)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed power up");
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(_powerUpSoundSource, transform.position);
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.ActivateLaserChange(1);
                        Debug.Log("Triple shot is active.");
                        break;
                    case 1:
                        player.SpeedPower();
                        Debug.Log("Speed Boost is active.");
                        break;
                    case 2:
                        if (player._activeShield == true)
                        {
                            Debug.Log("Player shield is already active");
                        }
                        else
                        {
                            player.ShieldPower();
                            Debug.Log("Shield is active.");
                        }
                        break;
                    case 3:
                        player.AmmoIncrease(15);
                        Debug.Log("Player collected ammo");
                        break;
                    case 4:
                        player.HealthIncrease();
                        Debug.Log("Player collected health");
                        break;
                    case 5:
                        player.ActivateLaserChange(2);
                        Debug.Log("Wave shot is active");
                        break;
                    case 6:
                        player.StartSlowDown();
                        break;
                    default:
                        Debug.Log("Powerup Undefined in switch.");
                        break;
                }
                Destroy(this.gameObject);
            }

        }
        
    }
}
