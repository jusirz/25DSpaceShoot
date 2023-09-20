using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _tripleshotspeed = 3.0f;
    [SerializeField]
    private int _powerupID; //0 = tripleshot 1 = speed 2 = shield

    void Update()
    {
        PowerUpMovement();
    }

    private void PowerUpMovement()
    {
        transform.Translate(_tripleshotspeed * Vector3.down * Time.deltaTime);
        if (transform.position.y <= -5.85f)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed power up");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if (other.tag == "Player")
        {
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.FlipTripleShot();
                        Debug.Log("Triple shot is active.");
                        break;
                    case 1:
                        player.SpeedPower();
                        Debug.Log("Speed Boost is active.");
                        break;
                    case 2:
                        player.ShieldPower();
                        Debug.Log("Shield is active.");
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
