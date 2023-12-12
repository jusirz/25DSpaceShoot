using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadnoughtBoss : MonoBehaviour
{
    private GameObject _player;

    [SerializeField]
    private int _bossHealth = 100;
    [SerializeField]
    private float _dreadSpeed = 3f;


    private bool _dreadnaughtDead = false;
    private bool _openingSequence = true;

    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        transform.position = new Vector3(20.61f, 4.88f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        DreadnaughtMovement();
        _uiManager.DreadHealthUpdate(_bossHealth);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < 3; i++)
            {
                _player.GetComponent<Player>().Damage();
            }

        }
        if (other.CompareTag("Laser"))
        {
            DreadnaughtDamage(3);
            Destroy(other.gameObject);
        }
    }

    private void DreadnaughtMovement()
    {
        if (transform.position.x > .88f)
        {
            transform.Translate(Vector3.left * _dreadSpeed * Time.deltaTime);
        }
    }

    private void DreadnaughtDamage(int taken)
    {
        _bossHealth -= taken;
        Debug.Log("boss health is " + _bossHealth);
        if (_bossHealth <= 0)
        {
            _dreadnaughtDead = true;
        }
    }

    private void BossDeath()
    {
        if (_dreadnaughtDead == true)
        {
            Destroy(this.gameObject);
        }
    }
}
