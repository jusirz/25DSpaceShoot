using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    public GameObject enemytoSpawn;
    public GameObject enemyContainer;
    private bool alive = true;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnEnemy()
    {


        while (alive == true)
        {
            Vector3 enemySpawnPos = new Vector3(Random.Range(-9.45f, 9.67f), 5.58f, 0);
            GameObject newEnemy = Instantiate(enemytoSpawn, enemySpawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
            Debug.Log("new enemy spawned");
        }



    }

    public void StopSpawn()
    {
        alive = false;
        StopCoroutine(SpawnEnemy());
    }
}
