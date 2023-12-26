using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    private Vector3 _enemyLocation;
    private bool _enableHoming = false;

    private void FixedUpdate()
    {
        if (_enableHoming == true)
        {
            HomingMovement();
        }
    }
    public void GetEnemyLocation(Vector3 _enemy)
    {
        _enemyLocation = _enemy;
        _enableHoming = true;
    }
    private void HomingMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, _enemyLocation, 10f * Time.deltaTime);
    }

    public void ActivateFailedToFind()
    {
        FailedToFind();
    }
    private void FailedToFind()
    {
        Destroy(this.gameObject); 
    }
}
