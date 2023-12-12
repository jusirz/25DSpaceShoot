using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleDetector : MonoBehaviour
{
    private GameObject _homingLaser;
    public LayerMask _enemyLayer;


    void Start()
    {
        _homingLaser = transform.parent.gameObject;
    }

    private void FixedUpdate()
    {
        Vector2 origin = transform.position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 40f, Vector2.zero, Mathf.Infinity, _enemyLayer, minDepth: 0f, maxDepth: 0f);
        List<RaycastHit2D> hitList = new List<RaycastHit2D>(hits);
        List<float> distances = new List<float>();
        foreach (var hit in hitList)
        {
            distances.Add(hit.distance);
        }
        float smallestDistance = distances.Min();
        foreach (var hit in hitList)
        {
            if (hit.distance == smallestDistance)
            {
                _homingLaser.GetComponent<HomingLaser>().GetEnemyLocation(hit.transform.position);
            }
        }
    }

}
