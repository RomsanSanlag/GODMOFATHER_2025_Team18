using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float gameDuration = 30f; // duration in sec
    private float timeRemaining;
    private bool isRunning = false;

    public TextMeshProUGUI timerText;

    void Update()
    {
        if (isRunning)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isRunning = false;
                EndGame();
            }

            UpdateUI();
        }
    }

    public void StartTimer()
    {
        timeRemaining = gameDuration;
        isRunning = true;
    }

    void UpdateUI()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    void EndGame()
    {
        Debug.Log("OVER");
    }
}
