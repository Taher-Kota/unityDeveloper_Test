using System.Collections;
using UnityEngine;

public enum GameState
{
    GamePlaying,
    GamePaused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int cubeCount = 0, totalCollectabelCube = 5;
    [SerializeField] private GameObject gameOverPanel, gameWonPanel;
    private readonly float GamePlayingTime = 120f;
    private float playerAirTime = 5f;
    [SerializeField] private Transform playerGroundCheckPoint;
    [SerializeField] private LayerMask groundLayer;
    private bool isJumping;
    GameState gameState;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Instance = this;
        gameState = GameState.GamePlaying;
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(GamePlaying());
    }

    private void Update()
    {
        print(isJumping);
        if (IsGamePlaying())
        {
            CheckPlayerGrounded();
            if (cubeCount == totalCollectabelCube)
            {
                StartCoroutine(GameWon());
            }
        }
    }

    public void IncremenetCubeCount()
    {
        cubeCount++;
    }


    private void GameOver()
    {
        StopAllCoroutines();
        Cursor.lockState = CursorLockMode.None;
        gameState = GameState.GameOver;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CheckPlayerGrounded()
    {
        bool isGrounded = Physics.CheckSphere(playerGroundCheckPoint.position, 0.3f, groundLayer);

        if (!isGrounded)
        {
            isJumping = true;
            playerAirTime -= Time.deltaTime;
            if (playerAirTime <= 0)
            {
                GameOver();
            }
        }
        else
        {
            playerAirTime = 5f;
            isJumping = false;
        }
    }

    public float GetGamePlayingTime()
    {
        return GamePlayingTime;
    }

    public bool IsGamePlaying()
    {
        return gameState == GameState.GamePlaying;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    IEnumerator GameWon()
    {
        yield return new WaitForSeconds(1f);
        gameWonPanel.SetActive(true);
        GameOver();
    }

    IEnumerator GamePlaying()
    {
        yield return new WaitForSeconds(GamePlayingTime);
        GameOver();
    }
}
