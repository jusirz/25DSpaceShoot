using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int _laserSpeed = 10;

    void Update()
    {
        LaserMove();
    }
    private void LaserMove()
    {
        transform.Translate(_laserSpeed * Vector3.up * Time.deltaTime);
        if (transform.position.y > 7)
        {
            Destroy(this.gameObject);
        }
    }
}
