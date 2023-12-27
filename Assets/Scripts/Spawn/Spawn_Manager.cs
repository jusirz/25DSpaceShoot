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
    [SerializeField]
    private GameObject _slowPowerUp;
    [SerializeField]
    private GameObject _homingShot;

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
    [SerializeField]
    private float _slowDownSpawnTimer = 30f;
    [SerializeField]
    private float _homingShotSpawnTimer = 30f;

    [Header("Enemy Info")]
    [SerializeField]
    private GameObject _enemytoSpawn1;
    [SerializeField]
    private GameObject _enemytoSpawn15;
    [SerializeField]
    private GameObject _enemytoSpawn2;
    [SerializeField]
    private GameObject _enemytoSpawn3;
    [SerializeField]
    private GameObject _enemytoSpawn4;
    [SerializeField]
    private GameObject _enemySwarmSpawn;

    private int _enemiesSpawned;
    private GameObject _player;
    public GameManager _GameManager;
    private float _stageSelector;

    [SerializeField]
    private GameObject _dreadBoss;
    

    public void Start()
    {
        SpawnAsteroid();
        SetTimers();
        _GameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player");

    }

    public void BossSpawnTurnOn()
    {
        DreadSpawn();
    }



    private void SetTimers()
    {
        _tripleShotSpawnTimer = Random.Range(18f, 25);
        _shieldSpawnTimer = Random.Range(30f, 40f);
        _ammoSpwawnTimer = 25f;
        _enemySpawnTimer = 8f;
        _speedBoostSpawnTimer = 15f;
    }

    private void DreadSpawn()
    {
        Vector3 _dreadSpawnPos = new Vector3(20.61f, 4.88f, 0f);
        GameObject newBoss = Instantiate(_dreadBoss, _dreadSpawnPos, Quaternion.identity);
        _player.GetComponent<Player>().ActivateScaling();
        StopCoroutine(EnemySpawnCounter());
    }
    private void SpawnEnemy()
    {
        switch (_enemiesSpawned)
        {
            case 1:
                if (_stageSelector < 3)
                {
                    Vector3 enemyspawnpos1 = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                    GameObject newEnemy = Instantiate(_enemytoSpawn1, enemyspawnpos1, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                }
                if (_stageSelector > 2)
                {
                    Vector3 enemyspawnpos15 = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                    GameObject newEnemy = Instantiate(_enemytoSpawn15, enemyspawnpos15, Quaternion.identity);
                    newEnemy.transform.parent = _enemyContainer.transform;
                }
                    break;
            case 2:
                Vector3 enemyspawnpos2 = new Vector3(-11.7f, Random.Range(2.49f, 6.92f), 0);
                GameObject newEnemy2 = Instantiate(_enemytoSpawn2, enemyspawnpos2, Quaternion.identity);
                newEnemy2.transform.parent = _enemyContainer.transform;
                break;
            case 3:
                Vector3 enemyspawnpos3 = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                GameObject newEnemy3 = Instantiate(_enemytoSpawn3, enemyspawnpos3, Quaternion.identity);
                newEnemy3.transform.parent = _enemyContainer.transform;
                break;
            case 4:
                Vector3 enemyspawnpos4 = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                GameObject newEnemy4 = Instantiate(_enemytoSpawn3, enemyspawnpos4, Quaternion.identity);
                newEnemy4.transform.parent = _enemyContainer.transform;
                break;
            case 5:
                Vector3 swarmspawnpos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
                GameObject newSwarm = Instantiate(_enemySwarmSpawn, swarmspawnpos, Quaternion.identity);
                newSwarm.transform.parent = _enemyContainer.transform;
                break;
        }

    }

    private IEnumerator HomingShotSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_homingShotSpawnTimer);
            Vector3 _homingShotSpawn = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject HomingSpawn = Instantiate(_homingShot, _homingShotSpawn, Quaternion.identity);
        }
    }

    private IEnumerator SlowDownSpawn()
    {
        while (_alive == true)
        {
            yield return new WaitForSeconds(_slowDownSpawnTimer);
            Vector3 _slowDownSpawn = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject SlowSpawn = Instantiate(_slowPowerUp, _slowDownSpawn, Quaternion.identity);
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
        Vector3 _asteroidStartPos = new Vector3(0, 8.2f, 0);
        GameObject _AsteroidSpawn = Instantiate(_AsteroidObject, _asteroidStartPos, Quaternion.identity);
    }

    private IEnumerator EnemySpawnCounter()
    {
        while (true)
        {
            _enemiesSpawned = 1;
            SpawnEnemy();
            yield return new WaitForSeconds(_enemySpawnTimer);
            _enemiesSpawned = 2;
            SpawnEnemy();
            yield return new WaitForSeconds(_enemySpawnTimer);
            _enemiesSpawned = 3;
            SpawnEnemy();
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }

    public void StageSpawn(int stageselect)
    {
        _stageSelector = stageselect;
        switch (_stageSelector)
        {
            case 1:
                _enemySpawnTimer = 6f;
                break;
            case 2:
                _enemySpawnTimer = 4f;
                break;
            case 3:
                _enemySpawnTimer = 3f;
                break;
            case 4:
                _enemySpawnTimer = 2f;
                break;
            default:
                break;
        }     
    }


    public void StopSpawn()
    {
        _alive = false;
        StopCoroutine(ShieldSpawn());
        StopCoroutine(TripleShotSpawn());
        StopCoroutine(HomingShotSpawn());
        StopCoroutine(SpeedBoostSpawn());
        StopCoroutine(AmmoUpSpawn());
        StopCoroutine(HealthSpawn());
        StopCoroutine(WaveShotSpawn());
        StopCoroutine(EnemySpawnCounter());
        StopCoroutine(SlowDownSpawn());
        Destroy(gameObject);
    }
    public void StartSpawnEngine()
    {
        _player.GetComponent<Player>().AmmoIncrease(1);
        SpawnEnemy();
        StartCoroutine(EnemySpawnCounter());
        StartCoroutine(HomingShotSpawn());
        StartCoroutine(TripleShotSpawn());
        StartCoroutine(SpeedBoostSpawn());
        StartCoroutine(ShieldSpawn());
        StartCoroutine(AmmoUpSpawn());
        StartCoroutine(HealthSpawn());
        StartCoroutine(WaveShotSpawn());
        StartCoroutine(SlowDownSpawn());
    }

}
