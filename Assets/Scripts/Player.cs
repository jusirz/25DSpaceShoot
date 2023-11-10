using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player stats
    [SerializeField]
    private float _speed = 8f;
    private readonly float _fireRate = .25f;
    private float _canFire = -.1f;
    private int _ammo = 15;
    //laser objects
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleLaser;
    [SerializeField]
    private GameObject _waveLaser;
    //player vfx
    public GameObject _ShieldVisual;
    [SerializeField]
    private GameObject _playerdamage1;
    [SerializeField]
    private GameObject _playerdamage2;
    //ui stuff
    public UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private Spawn_Manager _spawnManager;

    public bool _activeShield = false;
    public int _shieldDamage;
    private int _laserCommunicate = 0;

    //camera
    [SerializeField]
    private GameObject _cameraobject;

    private int _thrusterCool = 10;
    private bool _thrustActive;



    void Start()
    {
        StrtPos();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _laserSound;
        _cameraobject = GameObject.FindWithTag("MainCamera");
        _uiManager.UpdateAmmo(_ammo);
    }

    void Update()
    {
        CalcMove();
        LaserCooldown();
        AmmoCommunicate();
        ThrustCommunicate();

    }

    private void StrtPos()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    private void CalcMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _thrusterCool > 1)
        {
            _thrustActive = true;
            _speed += 3;
            StartCoroutine(ThrusterHeatUp());
            StopCoroutine(ThrusterHeatUp());

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _thrustActive = false;
            _speed -= 3;
            StartCoroutine(ThrusterCoolDown());
            StopCoroutine(ThrusterCoolDown());
        }

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

            if (_ammo <= 0)
            {
                _ammo = 0;
                Debug.Log("Out of ammo");
            }
            else
            {
                LaserShotSelection();
            }
        }
    }

    private IEnumerator ThrusterHeatUp()
    {
        bool turnoff = false;
        while (_thrustActive == true && turnoff == false)
        {
            _thrusterCool--;
            yield return new WaitForSeconds(.5f);
            if (_thrusterCool == 1)
            {
                turnoff = true;           
            }
        }
        
    }

    private IEnumerator ThrusterCoolDown()
    {
        bool turnoff = false;
        while (_thrustActive == false && turnoff == false)
        {
            _thrusterCool++;
            yield return new WaitForSeconds(.5f);
            if (_thrusterCool == 10)
            {
                turnoff = true;
            }
        }
    }
    public void Damage()
    {
        if (_activeShield == true)
        {
            _shieldDamage++;
            ShieldPowerTime();
            Debug.Log("Shield Blocking active. 1 damage to shield.");
        }
        else if (_activeShield == false)
        {
            _lives--;
            CameraShake();
            _uiManager.UpdateLives(_lives);
            VisualDamageChanger();
        }

    }

    //UI stuff here
    public void AmmoCommunicate()
    {
        if (_ammo <= 0)
        {
            _uiManager.OutAmmoSwitch(true);
        }
        else
        {
            _uiManager.OutAmmoSwitch(false);
        }
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    
    private void PlayerGameOverSequence()
    {
        _uiManager.GameOver();
        _spawnManager.StopSpawn();
        Destroy(this.gameObject);
    }
    
    public void ThrustCommunicate()
    {
        _uiManager.ThrusterUpdate(_thrusterCool);
    }

    //power up stuff here
    public void SpeedPower()
    {
        StartCoroutine(SpeedPowerTimer());
        StopCoroutine(SpeedPowerTimer());
    }
    private IEnumerator SpeedPowerTimer()
    {
        _speed += 6;
        yield return new WaitForSeconds(4.0f);
        _speed -= 6;
    }
    public void ShieldPower()
    {
        _activeShield = true;
        _ShieldVisual.SetActive(true);
        _uiManager.ShieldUIActivate();
    }
    private void ShieldPowerTime()
    {
        switch (_shieldDamage)
        {
            case 1:
                _uiManager.ShieldUIChange();
                Debug.Log("Shield took one damage");
                break;
            case 2:
                _uiManager.ShieldUIChange();
                Debug.Log("Shield took two damage");
                break;
            case 3:
                _uiManager.ShieldUIChange();
                Debug.Log("Shield took three damage and is destroyed");
                _activeShield = false;
                _ShieldVisual.SetActive(false);
                break;
        }
    }
    public void AmmoIncrease(int _increaseAmount)
    {
        _ammo += _increaseAmount;
        _uiManager.UpdateAmmo(_ammo);
    }

    public void AmmoDecrease(int _decreaseAmount)
    {
        _ammo -= _decreaseAmount;
        _uiManager.UpdateAmmo(_ammo);
    }

    public void HealthIncrease()
    {
        if (_lives == 3)
        {
            Debug.Log("Players health is full");
        }
        else
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            VisualDamageChanger();
        }
    }

    private IEnumerator LaserTypeChange(int _laserType)
    {
        switch (_laserType)
        {
            case 0:
                break;
            case 1:
                _laserCommunicate = 1;
                yield return new WaitForSeconds(5f);
                _laserCommunicate = 0;
                break;
            case 2:
                _laserCommunicate = 2;
                yield return new WaitForSeconds(5f);
                _laserCommunicate = 0;
                break;
        }
    }
    public void ActivateLaserChange(int _activateKey)
    {
        StartCoroutine(LaserTypeChange(_activateKey));
        StopCoroutine(LaserTypeChange(0));
    }

    private void LaserShotSelection()
    {
        switch (_laserCommunicate)
        {
            case 0:
                Instantiate(_laser, transform.position + new Vector3(0, 1.3f, 0), Quaternion.identity);
                _audioSource.Play(0);
                AmmoDecrease(1);
                break;
            case 1:
                Instantiate(_tripleLaser, transform.position, Quaternion.identity);
                _audioSource.Play(0);
                AmmoDecrease(3);
                break;
            case 2:
                Instantiate(_waveLaser, transform.position, Quaternion.identity);
                _audioSource.Play(0);
                AmmoDecrease(4);
                break;
        }
    }

    private void VisualDamageChanger()
    { 

        switch (_lives)
        {
            case 0:
                PlayerGameOverSequence();
                break;
            case 1:
                _playerdamage2.SetActive(true);
                _playerdamage1.SetActive(true);
                break;
            case 2:
                _playerdamage1.SetActive(true);
                _playerdamage2.SetActive(false);
                break;
            case 3:
                _playerdamage1.SetActive(false);
                break;
            default:
                Debug.Log("Player damage visualization is broken");
                break;
        }
    }
    public void CameraShake()
    {
        _cameraobject.GetComponent<CameraBehavior>().ActiveCameraShake();
        Debug.Log("CameraShake in player");
    }
}
