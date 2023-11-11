using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _TriplePowerUp;
    [SerializeField]
    private GameObject _SpeedPowerUp;
    [SerializeField]
    private GameObject _ShieldPowerUp;
    [SerializeField]
    private GameObject _AmmunitionUp;
    [SerializeField]
    private GameObject _AsteroidObject;
    private bool _alive = true;
    [SerializeField]
    private GameObject _HealthObject;
    [SerializeField]
    private GameObject _waveShot;

    [Header("Spawn Times")]
    [SerializeField]
    private float _enemySpawnTimer;
    [SerializeField]
    private float _shieldSpawnTimer;
    [SerializeField]
    private float _tripleShotSpawnTimer;
    [SerializeField]
    private float _speedBoostSpawnTimer;
    [SerializeField]
    private float _ammoSpwawnTimer = 60f;
    [SerializeField]
    private float _healthSpawnTimer = 40f;
    [SerializeField]
    private float _waveShotSpawnTimer = 100f;

    [Header("Enemy Info")]
    [SerializeField]
    private GameObject _enemytoSpawn1;
    [SerializeField]
    private GameObject _enemytoSpawn2;

    private bool _enemiesSpawned;
    public UIManager _UIManager;
    public GameManager _GameManager;
    private int _stageSelector;


    public void Start()
    {
        SpawnAsteroid();
        SetTimers();
        _GameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();


    }

    private void SetTimers()
    {
        _tripleShotSpawnTimer = Random.Range(18f, 25);
        _shieldSpawnTimer = Random.Range(30f, 40f);
        _ammoSpwawnTimer = 25f;
        _enemySpawnTimer = 8f;
        _speedBoostSpawnTimer = 15f;
    }
    private IEnumerator SpawnEnemy()
    {
        while (_enemiesSpawned == false)
        {
            Vector3 enemyspawnpos1 = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject newEnemy = Instantiate(_enemytoSpawn1, enemyspawnpos1, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);
        }

        while (_enemiesSpawned == false)
        {
            Vector3 enemyspawnpos2 = new Vector3(-11.7f, Random.Range(2.49f, 6.92f), 0);
            GameObject newEnemy = Instantiate(_enemytoSpawn2, enemyspawnpos2, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }


    private IEnumerator TripleShotSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_tripleShotSpawnTimer);
            Vector3 _tripleSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject TripleSpawn = Instantiate(_TriplePowerUp, _tripleSpawnPos, Quaternion.identity);
        }
    }
    private IEnumerator SpeedBoostSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_speedBoostSpawnTimer);
            Vector3 _speedSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject SpeedSpawn = Instantiate(_SpeedPowerUp, _speedSpawnPos, Quaternion.identity);
        }
    }
    private IEnumerator ShieldSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_shieldSpawnTimer);
            Vector3 _shieldSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject ShieldSpawn = Instantiate(_ShieldPowerUp, _shieldSpawnPos, Quaternion.identity);
        }
    }
    private IEnumerator AmmoUpSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_ammoSpwawnTimer);
            Vector3 _ammoSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject AmmoUp = Instantiate(_AmmunitionUp, _ammoSpawnPos, Quaternion.identity);
        }

    }
    private IEnumerator HealthSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_healthSpawnTimer);
            Vector3 _healthSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject _healthUp = Instantiate(_HealthObject, _healthSpawnPos, Quaternion.identity);   
        }
    }

    private IEnumerator WaveShotSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_waveShotSpawnTimer);
            Vector3 _waveSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject _waveShotSpawn = Instantiate(_waveShot, _waveSpawnPos, Quaternion.identity);
        }
    }

    public void SpawnAsteroid()
    {
        if (true)
        {
            Vector3 _asteroidStartPos = new Vector3(0, 8.2f, 0);
            GameObject _AsteroidSpawn = Instantiate(_AsteroidObject, _asteroidStartPos, Quaternion.identity);
        }

    }

    private IEnumerator EnemySpawnCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            _enemiesSpawned = false;
            yield return new WaitForSeconds(5f);
            _enemiesSpawned = true;
        }
    }

    public void StageSpawn(int stageselect)
    {
        bool UIonoff = true;
        _stageSelector = stageselect;
        switch (_stageSelector)
        {
            case 1:
                _enemySpawnTimer = 7f;
                if (UIonoff == true)
                {
                    _UIManager.GetComponent<UIManager>().StartEnemyWave();
                }
                UIonoff = false;
                break;
            case 2:
                _enemySpawnTimer = 5.5f;
                _UIManager.GetComponent<UIManager>().StartEnemyWave();
                break;
            case 3:
                _enemySpawnTimer = 4f;
                _UIManager.GetComponent<UIManager>().StartEnemyWave();
                break;
            case 4:
                _enemySpawnTimer = 2f;
                _UIManager.GetComponent<UIManager>().StartEnemyWave();
                break;
            default:
                break;
        }

        
    }


    public void StopSpawn()
    {
        _alive = false;
        StopCoroutine(ShieldSpawn());
        StopCoroutine(SpawnEnemy());
        StopCoroutine(TripleShotSpawn());
        StopCoroutine(SpeedBoostSpawn());
        StopCoroutine(AmmoUpSpawn());
        StopCoroutine(HealthSpawn());
        StopCoroutine(WaveShotSpawn());
        StopCoroutine(EnemySpawnCounter());

        Destroy(gameObject);
    }
    public void StartSpawnEngine()
    {
        StartCoroutine(EnemySpawnCounter());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(TripleShotSpawn());
        StartCoroutine(SpeedBoostSpawn());
        StartCoroutine(ShieldSpawn());
        StartCoroutine(AmmoUpSpawn());
        StartCoroutine(HealthSpawn());
        StartCoroutine(WaveShotSpawn());
   
    }

}
