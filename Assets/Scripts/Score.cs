using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score1Text;
    public TextMeshProUGUI score2Text;
    
    public PaddleRace race;

    public void printScore()
    {
        int score1 = Mathf.CeilToInt(race.paddleStats1.currentSpeed);
        score1Text.text = score1.ToString();
        
        int score2 = Mathf.CeilToInt(race.paddleStats2.currentSpeed);
        score2Text.text = score2.ToString();
    }
}
