using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadnoughtBoss : MonoBehaviour
{
    private GameObject _player;
    private GameObject _gameManager;
    

    [SerializeField]
    private int _bossHealth = 100;
    [SerializeField]
    private float _dreadSpeed = 3f;
    [SerializeField]
    private GameObject _swarm;
    [SerializeField]
    private bool _centered;
    [SerializeField]
    private float _attackSpeed = 3f;

    //Damage
    [SerializeField]
    private GameObject damage1;
    [SerializeField]
    private GameObject damage2;
    [SerializeField]
    private GameObject damage3;




    private UIManager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager");
        transform.position = new Vector3(20.61f, 5.58f, 0f);
        StartCoroutine(DreadnaughtAttack());
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

    private IEnumerator TurnOnHealthBar()
    {
        yield return new WaitForSeconds(3f);
        _uiManager.EnableDreadHealthBar();
    }

    private void DreadnaughtMovement()
    {
        if (transform.position.x > .88f)
        {
            transform.Translate(_dreadSpeed * Time.deltaTime * Vector3.left);
            StartCoroutine(TurnOnHealthBar());
        }
    }

    private void DreadnaughtDamage(int taken)
    {
        _bossHealth -= taken;
        if (_bossHealth <= 0)
        {
            BossDeath();
            _gameManager.GetComponent<GameManager>().GameOver();
        }
        if (_bossHealth <= 75 && damage1.activeInHierarchy != true)
        {
            damage1.SetActive(true);
            _attackSpeed = 2.5f;
        }
        if (_bossHealth <= 50 && damage2.activeInHierarchy != true)
        {
            damage2.SetActive(true);
            _attackSpeed = 2f;
        }
        if (_bossHealth <= 25 && damage3.activeInHierarchy != true)
        {
            damage3.SetActive(true);
            _attackSpeed = 1.5f;
        }
    }

    private void BossDeath()
    {
        _uiManager.DisableDreadHealthBar();
        Destroy(this.gameObject);
        _uiManager.ChangeGameWon(true);
        
    }


    private IEnumerator DreadnaughtAttack()
    {
        while (_bossHealth > 0)
        {
            yield return new WaitForSeconds(_attackSpeed);
            for (int i = 0; i < 5; i++)
            {
                Vector3 swarmPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                _ = Instantiate(_swarm, swarmPos, Quaternion.identity);  
            }

        }

    }
}
