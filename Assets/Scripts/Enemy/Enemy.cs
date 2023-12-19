using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemyYMove = 9;
    [SerializeField]
    private float _enemyXMove = 4;
    public Player _player;
    public Animator _explodeEnemy;

    public BoxCollider2D _enemyCollider;
    private AudioSource _explosionSourceEnemy;
    [SerializeField]
    private GameObject _enemyShotPreFab;
    [SerializeField]
    private GameObject _rocketShotPrefab;
    [SerializeField]
    private GameObject _rocket2ShotPrefab;
    private bool _enemyAlive = true;

    private bool _enemyShield = true;
    [SerializeField]
    private GameObject _enemyShieldObject;


    [SerializeField]
    private int _enemyType;
    private bool _thirdEnemy;

    private bool _closeToPlayer = false;
    [SerializeField]
    private bool _behindPlayer = false;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explodeEnemy = GetComponent<Animator>();
        _explosionSourceEnemy = GetComponent<AudioSource>();
        StartCoroutine(EnemyLaserSpawn());
        StartCoroutine(Enemy3MovementRoll());
    }

    void Update()
    {
        BehindCheck();
        if (_enemyType == 3)
        {
            PlayerDistanceCheck();
        }
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_player != null || _enemyShield == false)
        {
            if (other.CompareTag("Player"))
            {
                DeathActions();
                _player.Damage();
                Destroy(this.gameObject, 2.5f);
            }
            if (other.CompareTag("Laser"))
            {
                DeathActions();
                Destroy(other.gameObject);
                _player.AddScore(1);
                Destroy(this.gameObject, 2.5f);
            }
            if (other.CompareTag("shield"))
            {
                DeathActions();
                _player.Damage();
                Destroy(this.gameObject, 2.5f);
            }
        }
    }

    private void DeathActions()
    {
        _enemyAlive = false;
        Destroy(_enemyCollider);
        _explodeEnemy.SetTrigger("EnemyExplosion");
        _enemyYMove = 1;
        _explosionSourceEnemy.Play();
    }

    public void LaserMovement()
    {
            StartCoroutine(LaserMovementSwitch());
            StopCoroutine(LaserMovementSwitch());
    }

    private IEnumerator LaserMovementSwitch()
    {
        _enemyXMove = -6;
        yield return new WaitForSeconds(1f);
        _enemyXMove = -4;
    }


    private void PlayerDistanceCheck()
    {
        float dist = 4f;
        if (Vector3.Distance(transform.position, _player.transform.position) < dist)
        {
            if (_behindPlayer == false)
            {
                _closeToPlayer = true;
            }
            else
            {
                _closeToPlayer = false;
            }

        }
        else if (Vector3.Distance(transform.position, _player.transform.position) > dist)
        {
            _closeToPlayer = false;
        }
    }
    
    private void BehindCheck()
    {
        if (transform.position.y < _player.transform.position.y)
        {
            _behindPlayer = true;
        }
        else
        {
            _behindPlayer = false;
        }
    }

    public void EnemyShieldChange()
    {
        _enemyShield = false;
    }


    public IEnumerator EnemyLaserSpawn()
    {
        while (_enemyAlive == true)
        {
            if (_enemyType == 3)
            {
                float a = Random.Range(2f, 6f);
                yield return new WaitForSeconds(a);
                if (_enemyAlive == true)
                {
                    if (_behindPlayer == false)
                    {
                        Vector3 _enemyLaserPos = transform.position;
                        _ = Instantiate(_rocketShotPrefab, _enemyLaserPos, Quaternion.identity);
                    }
                    if (_behindPlayer == true)
                    {
                        Vector3 _enemyLaserPos2 = transform.position;
                        _ = Instantiate(_rocket2ShotPrefab, _enemyLaserPos2, Quaternion.identity);
                    }
                }
                float b = Random.Range(1f, 3f);
                yield return new WaitForSeconds(b);
            }
            else
            {
                float a = Random.Range(2f, 6f);
                yield return new WaitForSeconds(a);
                if (_enemyAlive == true)
                {
                    Vector3 _enemyLaserPos = transform.position;
                    _ = Instantiate(_enemyShotPreFab, _enemyLaserPos, Quaternion.identity);
                }
                float b = Random.Range(1f, 3f);
                yield return new WaitForSeconds(b);
            }

        }

    }

    public void EnemyPickupTrigger()
    {
        EnemyPickUp();
    }

    private void EnemyPickUp()
    {
        if (_enemyAlive == true)
        {
            Vector3 _enemyLaserPos = transform.position;
            _ = Instantiate(_enemyShotPreFab, _enemyLaserPos, Quaternion.identity);
        }
    }

    private void EnemyMovement()
    {
        switch (_enemyType)
        {
            case 1:
                transform.Translate(_enemyYMove * Time.deltaTime * Vector3.down);
                if (transform.position.y < -5.2f)
                {
                    float _posXRand = Random.Range(-9.45f, 9.67f);
                    transform.position = new Vector3(_posXRand, 7.18f, transform.position.z);
                }
                break;
            case 2:
                transform.Translate(_enemyXMove * Time.deltaTime * Vector3.left);
                if (transform.position.x > 11.7f)
                {
                    float posyrand = Random.Range(2.49f, 6.92f);
                    transform.position = new Vector3(-11.7f, posyrand, transform.position.z);
                }
            break;
            case 3:
                if (_closeToPlayer == false)
                {
                    Enemy3Movement();
                    if (transform.position.x > 11.7f || transform.position.y < -5.2f)
                    {
                        transform.position = new Vector3(-9.4f, 5.93f, transform.position.z);
                    }
                }

                else if (_closeToPlayer == true)
                {
                    if (_enemyAlive == true)
                    {
                        MoveTowardsPlayer();
                    }
                    
                }
                break;

        }
    }

    private void MoveTowardsPlayer()
    {
        if (_behindPlayer == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 3f * Time.deltaTime);
        }
    }
    private IEnumerator Enemy3MovementRoll()
    {
        while (true)
        {
            _thirdEnemy = false;
            yield return new WaitForSeconds(2f);
            _thirdEnemy = true;
            yield return new WaitForSeconds(2f);
        }
    }

    private void Enemy3Movement()
    {
        if (_thirdEnemy == true)
        {
            transform.Translate(_enemyYMove * Time.deltaTime * Vector3.down);
        }
        else if (_thirdEnemy == false)
        {
            transform.Translate(_enemyXMove * Time.deltaTime * Vector3.left);
        }
    }
}
