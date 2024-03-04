using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("TMP components")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _livesCountText;
    [SerializeField] private TMP_Text _levelCountText;
    [SerializeField] private TMP_Text _loseText;

    [Header("Game parameters")]
    [SerializeField] private int _lives = 3;

    [Header("Level prefabs")]
    [SerializeField] List<GameObject> _levelPrefabs = new List<GameObject>();

    [Header("Panels")]
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _pausePanel;

    private GameObject _ball;
    private GameObject _platform;
    private BallMovement _ballMovement;
    private Movement _movement;
    private int currentLevel = 0;
    private int levelBlocksCount; 
    private int destroyedBlocksCount;
    private int score;
    private int remainingLives;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _ballMovement = _ball.GetComponent<BallMovement>();
        _platform = GameObject.FindGameObjectWithTag("Player");
        _movement = _platform.GetComponent<Movement>();
        remainingLives = _lives;
        _livesCountText.text = $"LIVES: {remainingLives}";
        LoadLevel();
    }

    public void SetLevelBlockCount(int count)
    {
        levelBlocksCount = count;
    }

    public void IncreaseDestroyedBlocksCount()
    {
        destroyedBlocksCount++;

        if (destroyedBlocksCount >= levelBlocksCount)
        {
            FinishLevel();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        _scoreText.text = score.ToString("D5");
    }

    public void DecreaseLives()
    {
        remainingLives--;
        _livesCountText.text = $"LIVES: {remainingLives}";

        if(remainingLives <= 0)
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        MoveToStartPosition();
        StopGame();
        _losePanel.SetActive(true);
        _loseText.text = $"У вас закончились попытки.\r\nВам удалось набрать:\r\n <color=green>{score}</color> очков!\r\nХотите начать заново?";
    }

    private void FinishLevel()
    {
        destroyedBlocksCount = 0;
        MoveToStartPosition();
        currentLevel++;

        if(currentLevel >= _levelPrefabs.Count)
        {
            LoseGame();
        }
        else
        {
            LoadLevel();
            _levelCountText.text = $"LEVEL: {currentLevel + 1}";
        }
    }

    public void RestartGame()
    {
        _losePanel.SetActive(false);
        _movement.enabled = true;
        _ballMovement.enabled = true;
        _livesCountText.text = $"LIVES: {_lives}";
        score = 0;
        _scoreText.text = score.ToString("D5");
        currentLevel = 0;
        remainingLives = _lives;

        DestroyLevel();
        LoadLevel();
        MoveToStartPosition();
    }

    private void MoveToStartPosition()
    {
        _ballMovement.MoveToStartPosition();
        _movement.MoveToStartPosition();
    }

    private void LoadLevel()
    {
        Instantiate(_levelPrefabs[currentLevel], Vector3.zero, Quaternion.identity);
    }

    private void DestroyLevel()
    {
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        Destroy(level);
    }

    public void Pause()
    {
        _ballMovement.StopMoving();
        _movement.enabled = false;
        _pausePanel.SetActive(true);
    }

    private void StopGame()
    {
        _ballMovement.enabled = false;
        _movement.enabled = false;
    }

    public void ContinueGame()
    {
        _ballMovement.ContinueMoving();
        _movement.enabled = true;
    }
}
