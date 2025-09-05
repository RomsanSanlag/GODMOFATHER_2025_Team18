using System;
using UnityEngine;
using System.Collections;

public class PaddleRace : MonoBehaviour
{
    public MainMenuFuncs MainMenuFuncs;
    
    public Transform player1;
    public Transform player2;

    public float player1Speed = 5f;
    public float player2Speed = 5f;

    //public float player1Distance = 5f;
    //public float player2Distance = 7f;
    
    public PaddleStats paddleStats1;
    public PaddleStats paddleStats2;
    
    private int finishedCount = 0;

    public void Start()
    {
        StartRace();
    }

    public void StartRace()
    {
        StartCoroutine(MovePlayer(player1, player1Speed, paddleStats1.currentSpeed));
        StartCoroutine(MovePlayer(player2, player2Speed, paddleStats2.currentSpeed));
    }

    private IEnumerator MovePlayer(Transform player, float speed, float distance)
    {
        Vector3 startPos = player.position;
        Vector3 targetPos = startPos + Vector3.right * distance;

        while (Vector3.Distance(player.position, targetPos) > 0.01f)
        {
            player.position = Vector3.MoveTowards(player.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        player.position = targetPos; // get final position
        
        finishedCount++;

        if (finishedCount == 2) // ici 2 joueurs
        {
            OnRaceFinished();
        }
    }

    private void OnRaceFinished()
    {
        MainMenuFuncs.ScoreScreen();
        Debug.Log("course finished");
    }
}
