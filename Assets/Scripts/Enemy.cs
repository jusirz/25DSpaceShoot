using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemyYMove = 9;
    [SerializeField]
    private float _enemyXMove = 4;
    private float _posXRand;
    public Player _player;
    public Animator _explodeEnemy;
    public BoxCollider2D _enemyCollider;
    private AudioSource _explosionSourceEnemy;
    [SerializeField]
    private GameObject _enemyShotPreFab;
    private bool _enemyAlive = true;
    [SerializeField]
    private int _enemyType;






    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explodeEnemy = GetComponent<Animator>();
        _enemyCollider = GetComponent<BoxCollider2D>();
        _explosionSourceEnemy = GetComponent<AudioSource>();

         StartCoroutine(EnemyLaserSpawn());
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
            Destroy(this.gameObject, 2.5f);
            _player.AddScore(1);
        }


    }

    public IEnumerator EnemyLaserSpawn()
    {
        while (_enemyAlive == true)
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


    private void EnemyMovement()
    {
        if (_enemyType == 1)
        {
            transform.Translate(_enemyYMove * Time.deltaTime * Vector3.down);
            if (transform.position.y < -5.2f)
            {
                _posXRand = Random.Range(-9.45f, 9.67f);
                transform.position = new Vector3(_posXRand, 7.18f, transform.position.z);
            }
        }
        else if (_enemyType == 2)
        {
            transform.Translate(_enemyXMove * Time.deltaTime * Vector3.left);
            if (transform.position.x > 11.7f)
            {
                float posyrand = Random.Range(2.49f, 6.92f);
                transform.position = new Vector3(-11.7f, posyrand, transform.position.z);
            }
        }
    }





}
