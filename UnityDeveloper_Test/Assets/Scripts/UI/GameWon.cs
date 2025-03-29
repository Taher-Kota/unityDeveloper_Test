using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    [SerializeField] private Button restartButton, quitButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}
