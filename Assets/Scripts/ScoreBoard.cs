using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    int score = 0;

    void Start()
    {
        scoreText.text = $"Score: {score}";
    }

    public void IncreaseScore(int amount)
    {
        score += amount; // score = score + amount;
        scoreText.text = $"Score: {score}";
        Debug.Log($"score: {score}");
    }
}
