using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int laserSpeed = 10;

    void Update()
    {
        laserMove();
    }
    private void laserMove()
    {
        transform.Translate(laserSpeed * Vector3.up * Time.deltaTime);
        if (transform.position.y > 7)
        {
            Destroy(this.gameObject);
        }
    }
}
