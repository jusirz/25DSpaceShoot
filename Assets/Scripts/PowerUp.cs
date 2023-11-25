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
                    default:
                        break;
                }
                Destroy(this.gameObject);
            }

        }
        
    }
}
