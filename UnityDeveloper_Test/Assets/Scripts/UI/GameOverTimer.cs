using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameOverTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    float countDownTimer;

    private void Awake()
    {
        countDownTimer = 10;
        timer.text = countDownTimer.ToString();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            countDownTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(countDownTimer / 60);
            int seconds = Mathf.FloorToInt(countDownTimer % 60);

            timer.text = minutes.ToString() + ":" + seconds.ToString();
            if (countDownTimer <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                timer.text = "0:00";
                GameManager.Instance.GameOver();
            }
        }
    }
}
