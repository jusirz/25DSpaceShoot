using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    [SerializeField]
    private float _gameSeconds;
    private int _stageChoice;
    private Spawn_Manager _spawnManager;
    private UIManager _uiManager;
    private bool _bossSpawned = false;


    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        _gameSeconds = Time.timeSinceLevelLoad;
        if (_bossSpawned != true)
        {
            StageManager();
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }

    public void StageManager()
    {

        if (_gameSeconds > 30f && _gameSeconds < 60f)
        {
            _stageChoice = 1;
        }
        else if (_gameSeconds > 60f && _gameSeconds < 180f)
        {
            _stageChoice = 2;
        }
        else if (_gameSeconds > 180f && _gameSeconds < 360f)
        {
            _stageChoice = 3;
        }
        else if (_gameSeconds > 360f && _gameSeconds < 500f)
        {
            _stageChoice = 4;
        }
        else if (_gameSeconds > 500f)
        {
            if (_bossSpawned == false)
            {
                _spawnManager.BossSpawnTurnOn();
                _bossSpawned = true;
            }
        }
        _spawnManager.StageSpawn(_stageChoice);
        EnemyWaveDisplay();
        
    }

    public void EnemyWaveDisplay()
    {
        _uiManager.GetComponent<UIManager>().StartEnemyWave(_stageChoice);
    }
}
