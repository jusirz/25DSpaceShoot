using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    private bool _isDead = false;
    [SerializeField]
    private float _swarmSpeed = 2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == false)
        {
            SwarmMove();
        }
        
    }

    private void SwarmMove()
    {
        transform.Translate(Vector3.down * _swarmSpeed * Time.deltaTime);
        if (transform.position.y >= 9f)
        {
            Destroy(this.gameObject);
        }
    }
}
