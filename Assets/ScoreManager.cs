using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private int totalScore = 0;
    private Shooter shooter;

    private void Start()
    {
        shooter = FindObjectOfType<Shooter>();
        shooter.OnHitTargets += CalculateScore;
    }

    private void CalculateScore(List<Target> targetsHit)
    {
        var value = 0;
        var multiplier = 0;

        foreach (var target in targetsHit)
        {
            value += target.ScoreValue;
            multiplier += target.ScoreMultiplier;
        }

        var score = value * multiplier;
        totalScore += score;
        RefreshScoreText();
    }

    private void RefreshScoreText()
    {
        scoreText.text = totalScore.ToString("000,000,000");//, new CultureInfo("es-ES"));
    }
    
    private void OnDestroy()
    {
        shooter.OnHitTargets -= CalculateScore;
    }
}