using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CircleDetector : MonoBehaviour
{
    private GameObject _homingLaser;
    public LayerMask _enemyLayer;
    private GameObject _hitObject;
    private bool _messageSent = false;



    void Start()
    {
        _homingLaser = transform.parent.gameObject;
        DetectEnemy();
    }

    private void FixedUpdate()
    {
        if (_messageSent == true)
        {
            StartCoroutine(SendLocationInfo());
        }
        
    }

    public IEnumerator SendLocationInfo()
    {
        while (true)
        {
            if (_hitObject == null)
            {
                _messageSent = false;
                _homingLaser.GetComponent<HomingLaser>().ActivateFailedToFind(2);
                Destroy(this.gameObject);
            }
            if (_hitObject != null && _homingLaser != null)
            {
                _homingLaser.GetComponent<HomingLaser>().GetEnemyLocation(_hitObject.transform.position);
            }
            yield return new WaitForEndOfFrame();

        }
    }

    private void DetectEnemy()
    {
        Vector2 origin = transform.position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, 40f, Vector2.zero, Mathf.Infinity, _enemyLayer, minDepth: 0f, maxDepth: 0f);
        List<RaycastHit2D> hitList = new List<RaycastHit2D>(hits);
        List<float> distances = new List<float>();
        if (hits.Length != 0) 
        {
            foreach (var hit in hitList)
            {

              distances.Add(hit.distance);
            }
            float smallestDistance = distances.Min();
            foreach (var hit in hitList)
            {

                if (hit.distance == smallestDistance)
                {
                    if (_messageSent == false)
                    {
                        _hitObject = hit.collider.gameObject;
                        _messageSent = true;
                    }
                }
            }
        }
        else if (hits.Length == 0)
        {
            _homingLaser.GetComponent<HomingLaser>().ActivateFailedToFind(1);
            Debug.Log("hits was null and void was called");
        }

        }

}
