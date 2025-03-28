using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GamePlaying,
    GamePaused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int cubeCount = 0;
    [SerializeField] private GameObject gameOverPanel;

    GameState gameState;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Instance = this;
        gameState = GameState.GamePlaying;
        gameOverPanel.SetActive(false);
    }

    public void IncremenetCubeCount()
    {
        cubeCount++;
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }
}
