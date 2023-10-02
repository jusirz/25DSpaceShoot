using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemytoSpawn;
    [SerializeField]
    private GameObject _TriplePowerUp;
    [SerializeField]
    private GameObject _SpeedPowerUp;
    [SerializeField]
    private GameObject _ShieldPowerUp;
    [SerializeField]
    private GameObject _AsteroidObject;
    private bool _alive = true;

    [Header("Spawn Times")]
    [SerializeField]
    private int _enemySpawnTimer;
    [SerializeField]
    private int _shieldSpawnTimer;
    [SerializeField]
    private int _tripleShotSpawnTimer;
    [SerializeField]
    private int _speedBoostSpawnTimer;

    public void Start()
    {
        SpawnAsteroid();
    }

    private IEnumerator SpawnEnemy()
    {
        while (_alive == true)
        {
            Vector3 enemySpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject newEnemy = Instantiate(_enemytoSpawn, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            float d = Random.Range(8f, 10f);
            yield return new WaitForSeconds(d);
            Debug.Log("new enemy spawned");
        }
    }

    private IEnumerator TripleShotSpawn()
    {
        while (_alive == true)
        {
            Vector3 _tripleSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject TripleSpawn = Instantiate(_TriplePowerUp, _tripleSpawnPos, Quaternion.identity);
            float b = Random.Range(20f, 40f);
            yield return new WaitForSeconds(b);
            Debug.Log("new triple shot power up spawned");
        }
    }
    private IEnumerator SpeedBoostSpawn()
    {
        while (_alive == true)
        {
            Vector3 _speedSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject SpeedSpawn = Instantiate(_SpeedPowerUp, _speedSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(10f);
            Debug.Log("new speed power up spawned");
        }
    }
    private IEnumerator ShieldSpawn()
    {
        while (_alive == true)
        {
            Vector3 _shieldSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject ShieldSpawn = Instantiate(_ShieldPowerUp, _shieldSpawnPos, Quaternion.identity);
            float a = Random.Range(20f, 30f);
            yield return new WaitForSeconds(a);
            Debug.Log("shield has spawned");
        }
    }

    public void SpawnAsteroid()
    {
        if (true)
        {
            Vector3 _asteroidStartPos = new Vector3(0, 8.2f, 0);
            GameObject _AsteroidSpawn = Instantiate(_AsteroidObject, _asteroidStartPos, Quaternion.identity);
            Debug.Log("Asteroid has spawned");
        }

    }

    public void StopSpawn()
    {
        _alive = false;
        StopCoroutine(ShieldSpawn());
        StopCoroutine(SpawnEnemy());
        StopCoroutine(TripleShotSpawn());
        StopCoroutine(SpeedBoostSpawn());
        Destroy(gameObject);
    }
    public void StartSpawnEngine()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(TripleShotSpawn());
        StartCoroutine(SpeedBoostSpawn());
        StartCoroutine(ShieldSpawn());
    }

}
