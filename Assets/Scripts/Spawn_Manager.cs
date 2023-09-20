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
    private float _spawnLocationX;
    private bool _alive = true;
    void Start()
    {
        StartSpawnEngine();
        _spawnLocationX = Random.Range(-9.45f, 9.67f);
    }

    private IEnumerator SpawnEnemy()
    {
        while (_alive == true)
        {
            Vector3 enemySpawnPos = new Vector3(_spawnLocationX, 5.58f, 0);
            GameObject newEnemy = Instantiate(_enemytoSpawn, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
            Debug.Log("new enemy spawned");
        }
    }

    private IEnumerator TripleShotSpawn()
    {
        while (_alive == true)
        {
            Vector3 _tripleSpawnPos = new Vector3(_spawnLocationX, 5.58f, 0);
            GameObject TripleSpawn = Instantiate(_TriplePowerUp, _tripleSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
            Debug.Log("new triple shot power up spawned");
        }
    }
    private IEnumerator SpeedBoostSpawn()
    {
        while (_alive == true)
        {
            Vector3 _speedSpawnPos = new Vector3(_spawnLocationX, 5.58f, 0);
            GameObject SpeedSpawn = Instantiate(_SpeedPowerUp, _speedSpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(25f);
            Debug.Log("new speed power up spawned");
        }
    }
    private IEnumerator ShieldSpawn()
    {
        while (_alive == true)
        {
            Vector3 _shieldSpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject ShieldSpawn = Instantiate(_ShieldPowerUp, _shieldSpawnPos, Quaternion.identity);
            float a = Random.Range(3.0f, 28f);
            yield return new WaitForSeconds(a);
            Debug.Log("shield has spawned");
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
