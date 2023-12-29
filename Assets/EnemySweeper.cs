using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySweeper : MonoBehaviour
{
    public LayerMask _enemyLayer;
    private GameObject _hitEnemy;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        DestroyAllEnemies();
    }

    private void DestroyAllEnemies()
    {
        Vector2 origin = transform.position;
        RaycastHit2D[] enemies = Physics2D.CircleCastAll(origin, 40f, Vector2.zero, Mathf.Infinity, _enemyLayer, minDepth: 0f, maxDepth: 0f);
        List<RaycastHit2D> enemyList = new List<RaycastHit2D>(enemies);
        foreach (var enemy in enemies)
        {
            Destroy(enemy.collider.gameObject);
            Destroy(this.gameObject);
        }
    }
}
