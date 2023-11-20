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
    private bool _enemyAlive = true;
    [SerializeField]
    private int _enemyType;
    private bool _thirdEnemy;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explodeEnemy = GetComponent<Animator>();
        _enemyCollider = GetComponent<BoxCollider2D>();
        _explosionSourceEnemy = GetComponent<AudioSource>();
        StartCoroutine(EnemyLaserSpawn());
        StartCoroutine(Enemy3MovementRoll());
    }

    void Update()
    {
        EnemyMovement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            _enemyAlive = false;
            if (_player != null)
            {
                _player.Damage();
                Debug.Log("Damage to player.");
            }
            _explodeEnemy.SetTrigger("EnemyExplosion");
            _explosionSourceEnemy.Play();
            Destroy(this.gameObject, 2.5f);
        }

        if (other.CompareTag("Laser"))
        {
            _enemyAlive = false;
            _explodeEnemy.SetTrigger("EnemyExplosion");
            Destroy(_enemyCollider);
            _enemyYMove = 1;
            Destroy(other.gameObject);
            _explosionSourceEnemy.Play();
            if (_player != null)
            {
                _player.AddScore(1);
                Debug.Log("Score was added");
            }
            Destroy(this.gameObject, 2.5f);
        if (other.CompareTag("shield"))
            {
                _enemyAlive = false;
                _explodeEnemy.SetTrigger("EnemyExplosion");
                Destroy(_enemyCollider);
                _enemyYMove = 1;
                _explosionSourceEnemy.Play();
                _player.Damage();
                Destroy(this.gameObject, 2.5f);
            }

        }


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
                    Vector3 _enemyLaserPos = transform.position;
                    GameObject _newRocket = Instantiate(_rocketShotPrefab, _enemyLaserPos, Quaternion.identity);
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
                    GameObject _newEnemyLaser = Instantiate(_enemyShotPreFab, _enemyLaserPos, Quaternion.identity);
                }
                float b = Random.Range(1f, 3f);
                yield return new WaitForSeconds(b);
            }

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
                Enemy3Movement();
                if (transform.position.x > 11.7f || transform.position.y < -5.2f)
                {
                    transform.position = new Vector3(-9.4f, 5.93f, transform.position.z);
                }
                break;

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
        else if(_thirdEnemy == false)
        {
            transform.Translate(_enemyXMove * Time.deltaTime * Vector3.left);
        }
    }
}
