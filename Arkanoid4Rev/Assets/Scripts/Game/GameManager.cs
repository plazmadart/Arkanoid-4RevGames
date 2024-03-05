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
    [SerializeField] private TMP_Text _winText;

    [Header("Game parameters")]
    [SerializeField] private int _lives = 3;

    [Header("Level prefabs")]
    [SerializeField] List<GameObject> _levelPrefabs = new List<GameObject>();

    [Header("Panels")]
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _winPanel;

    private GameObject _ball;
    private GameObject _platform;
    private BallMovement _ballMovement;
    private Movement _movement;
    private int currentLevel = 0;
    private int levelBlocksCount; 
    private int destroyedBlocksCount;
    private int score;
    private int remainingLives;
    private int levelScore;
    private bool gamePaused = false;

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
        levelScore += points;
        _scoreText.text = score.ToString("D5");

        IncreaseDestroyedBlocksCount();
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

    private void WinGame()
    {
        MoveToStartPosition();
        StopGame();
        _winPanel.SetActive(true);
        _winText.text = $"Ого!\r\nВам удалось набрать:\r\n <color=green>{score}</color> очков!\r\nХотите начать заново?";
    }

    private void FinishLevel()
    {
        destroyedBlocksCount = 0;
        levelScore = 0;
        MoveToStartPosition();
        currentLevel++;

        if(currentLevel >= _levelPrefabs.Count)
        {
            WinGame();
        }
        else
        {
            DestroyLevel();
            LoadLevel();
            _levelCountText.text = $"LEVEL: {currentLevel + 1}";
        }
    }

    public void RestartGame()
    {
        _losePanel.SetActive(false);
        _livesCountText.text = $"LIVES: {_lives}";
        _levelCountText.text = $"LEVEL: {1}";
        score = 0;
        _scoreText.text = score.ToString("D5");
        currentLevel = 0;
        remainingLives = _lives;
        destroyedBlocksCount = 0;

        DestroyLevel();
        LoadLevel();
        ContinueGame();
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

    public void ReloadLevel()
    {
        score -= levelScore;
        levelScore = 0;
        _scoreText.text = score.ToString("D5");
        destroyedBlocksCount = 0;

        DecreaseLives();
        DestroyLevel();
        LoadLevel();


        if (gamePaused)
        {
            gamePaused = false;
            ContinueGame();
        }

        MoveToStartPosition();
    }

    public void Pause()
    {
        gamePaused = true;
        _ballMovement.StopMoving();
        _ballMovement.enabled = false;
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
        _ballMovement.enabled = true;
        _movement.enabled = true;

        if (gamePaused)
        {
            _ballMovement.ContinueMoving();
            gamePaused = false;
        }
    }
}
