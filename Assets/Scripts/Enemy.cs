using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _enemyYMove = 4;
    private float _posXRand;
    [SerializeField]
    public Player _player;
    public Animator _explodeEnemy;
    public BoxCollider2D _enemyCollider;
    private AudioSource _explosionSourceEnemy;
  
    



    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _explodeEnemy = GetComponent<Animator>();
        _enemyCollider = GetComponent<BoxCollider2D>();
        _explosionSourceEnemy = GetComponent<AudioSource>();


    }

    void Update()
    {
        transform.Translate(_enemyYMove * Time.deltaTime * Vector3.down);
        if (transform.position.y < -5.2f)
        {
            _posXRand = Random.Range(-9.45f, 9.67f);
            transform.position = new Vector3(_posXRand, 7.18f, transform.position.z);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
                Debug.Log("Damage to player.");
            }
            _explodeEnemy.SetTrigger("EnemyExplosion");
            _explosionSourceEnemy.Play();
            Destroy(this.gameObject, 2.5f);
            
            

        }

        if (other.tag == "Laser")
        {
            _explodeEnemy.SetTrigger("EnemyExplosion");
            Destroy(_enemyCollider);
            _enemyYMove= 1;
            Destroy(other.gameObject);
            _explosionSourceEnemy.Play();
            Destroy(this.gameObject, 2.5f);
            _player.AddScore(1);
        }


    }




}
