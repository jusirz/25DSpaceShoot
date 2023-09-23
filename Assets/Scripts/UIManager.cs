using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private GameObject _gameOver;
    [SerializeField]
    private GameObject _restartMessage;
    private bool _gameOverCheck = false;
    [SerializeField]
    private GameManager _gameManager;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOver()
    {
        _gameOverCheck = true;
        _restartMessage.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _gameManager.GameOver();
    }

    private IEnumerator GameOverFlicker()
    {
        while (_gameOverCheck == true)
        {
            _gameOver.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOver.SetActive(false);
            yield return new WaitForSeconds(.5f);

        }

    }

}