using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    [SerializeField]
    private float _asteroidSpeed = 1.0f;
    [SerializeField]
    private float _asteroidRotate;
    private bool _activateRotate = false;
    [SerializeField]
    private GameObject _explosion;
    public UIManager _uiManager;
    public Spawn_Manager _spawnManager;




    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager.AsteroidStartMessageFlip(true);


    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMove();
        ActivateRotation();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(_explodeTimer());
            Destroy(other.gameObject);
            StopCoroutine(_explodeTimer());
            _uiManager.GameOver();
        }
        else if (other.tag == "Laser")
        {
            StartCoroutine(_explodeTimer());
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            _uiManager.AsteroidStartMessageFlip(false);
            StopCoroutine(_explodeTimer());
            _spawnManager.StartSpawnEngine();
        }
    }

    public void AsteroidMove()
    {
        transform.Translate(_asteroidSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y < 1)
        {
            _asteroidSpeed = 0f;
            _activateRotate = true;
        }
    }

    public void ActivateRotation()
    {
        if (_activateRotate == true)
        {
            _asteroidRotate = 10.0f * Time.deltaTime;
            transform.Rotate(0, 0, _asteroidRotate);
        }
    }

    public IEnumerator _explodeTimer()
    {
        Vector3 _spawnExplosion = this.gameObject.transform.position;
        Instantiate(_explosion, _spawnExplosion, Quaternion.identity);
        yield return new WaitForSeconds(9f);
        Destroy(_explosion);
        

    }
}
