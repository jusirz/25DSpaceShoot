using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    private float _gameSeconds;
    private int _stageChoice;
    public Spawn_Manager _SpawnManager;
    public UIManager _UIManager;



    private void Start()
    {
        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

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
        _gameSeconds = Time.time;
        StageManager();
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
        else if (_gameSeconds > 360f)
        {
            _stageChoice = 4;
        }
        _SpawnManager.StageSpawn(_stageChoice);
        EnemyWaveDisplay();
        
    }

    public void EnemyWaveDisplay()
    {
        _UIManager.GetComponent<UIManager>().StartEnemyWave(_stageChoice);
    }
}
