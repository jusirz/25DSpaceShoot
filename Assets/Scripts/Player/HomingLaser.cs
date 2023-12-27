using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
{
    private Vector3 _enemyLocation;
    private bool _enableHoming = false;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

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

    public void ActivateFailedToFind(int option)
    {
        if (option == 1)
        {
            FailedToFind(1);
        }
        if (option == 2)
        {
            FailedToFind(2);
        }
    }
    private void FailedToFind(int option)
    {
        if (option == 1)
        {
            _player.GetComponent<Player>().AmmoIncrease(4);
            Destroy(this.gameObject);
        }
        if (option == 2)
        {
            Destroy(this.gameObject);
        }

    }
}
