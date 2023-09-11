using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private float fireRate = .25f;
    private float canFire = -.1f;
    public GameObject laser;
    private int _lives = 3;
    [SerializeField]
    private Spawn_Manager spawnManager;



    void Start()
    {
        StrtPos();
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

        if (spawnManager = null)
        {
            Debug.LogError("Spawn Manager is Null");
        }
    }

    void Update()
    {
        CalcMove();
        LaserCooldown();
    }

    private void StrtPos()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    private void CalcMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(_speed * horizontalInput * Time.deltaTime * Vector3.right);
        transform.Translate(_speed * Time.deltaTime * verticalInput * Vector3.up);
        if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }
        else if (transform.position.y < -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }
        else if (transform.position.x >= 11.14)
        {
            transform.position = new Vector3(-11.24f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.24f)
        {
            transform.position = new Vector3(11.14f, transform.position.y, 0);
        }
    }
    private void LaserCooldown()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
        {
            canFire = Time.time + fireRate;
            Instantiate(laser, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
        }
    }
    public void Damage()
    {
        _lives--;
        if (_lives < 1)
        {
            spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
            Destroy(this.gameObject);
            spawnManager.StopSpawn();
        }
    }












}