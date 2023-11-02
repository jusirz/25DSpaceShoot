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
    [SerializeField]
    private GameObject _asteroidStart;
    [SerializeField]
    private GameObject _shieldCount1;
    [SerializeField]
    private GameObject _shieldCount2;
    [SerializeField]
    private GameObject _shieldCount3;
    private Player _player;
    [SerializeField]
    private Text _ammoNumber;
    [SerializeField]
    private GameObject _ammoOut;
    [SerializeField]
    private Slider _thrusterSlider;
    private float _speedSliderValue;

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoNumber.text = "" + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }
    public void UpdateAmmo(int playerAmmo)
    {
        _ammoNumber.text = "" + playerAmmo;
    }

    public void SpeedUpdate(float speedValue)
    {
        
        _speedSliderValue = speedValue;
        if (_speedSliderValue == 8f)
        {
            _thrusterSlider.value = 1;
        }
        else if (_speedSliderValue == 11f)
        {
            _thrusterSlider.value = 2;
        }
        else if (_speedSliderValue == 14f)
        {
            _thrusterSlider.value = 3;
        }
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

    public void OutAmmoSwitch(bool ammoswitch)
    {
        if (ammoswitch == true)
        {
            _ammoOut.SetActive(true);
        }
        if (ammoswitch == false)
        {
            _ammoOut.SetActive(false);
        }
    }

    public void AsteroidStartMessageFlip(bool active)
    {
        if (active == true)
        {
            _asteroidStart.SetActive(true);
        }
        else if (active == false)
        {
            _asteroidStart.SetActive(false);
        }
    }

    public void ShieldUIActivate()
    {
        _shieldCount1.SetActive(true);
        _shieldCount2.SetActive(true);
        _shieldCount3.SetActive(true);
    }

    public void ShieldUIChange()
    {
        switch (_player._shieldDamage)
        {
            case 1:
                _shieldCount3.SetActive(false);
                break;
            case 2:
                _shieldCount2.SetActive(false);
                break;
            case 3:
                _shieldCount1.SetActive(false);
                break;
        }
    }

}