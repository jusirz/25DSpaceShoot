using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;
    private float _fireRate = .25f;
    private float _canFire = -.1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleLaser;
    public GameObject _ShieldVisual;
    public UIManager _uiManager;
    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private Spawn_Manager _spawnManager;

    private bool _activeTripleShot = false;
    private bool _activeShield = false;

    void Start()
    {
        StrtPos();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_activeTripleShot == true)
            {
                Instantiate(_tripleLaser, transform.position, Quaternion.identity);
                Debug.Log("new triple laser");
            }
            else
            {
                Instantiate(_laser, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
            }
        }
    }
    public void Damage()
    {
        if (_activeShield == true)
        {
            Debug.Log("Shield Blocking active");
            return;
        }
        else if (_activeShield == false)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);
        }

        if (_lives < 1)
        {
            _uiManager.GameOver();
            _spawnManager.StopSpawn();
            Destroy(this.gameObject);
        }
    }
    public void FlipTripleShot()
    {
        if (_activeTripleShot == false)
        {
            StartCoroutine(TripleShotTimer());
            StopCoroutine(TripleShotTimer());
        }

    }

    private IEnumerator TripleShotTimer()
    {
        _activeTripleShot = true;
        yield return new WaitForSeconds(5.0f);
        _activeTripleShot = false;

    }

    public void SpeedPower()
    {
        StartCoroutine(SpeedPowerTimer());
        StopCoroutine(SpeedPowerTimer());
    }

    private IEnumerator SpeedPowerTimer()
    {
        _speed += 5;
        yield return new WaitForSeconds(4.0f);
        _speed -= 5;
    }

    public void ShieldPower()
    {
        StartCoroutine(ShieldPowerTime());
        StopCoroutine(ShieldPowerTime());
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    private IEnumerator ShieldPowerTime()
    {
        _activeShield = true;
        _ShieldVisual.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        _ShieldVisual.SetActive(false);
        _activeShield = false; 

    }

}