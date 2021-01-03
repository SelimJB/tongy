using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private ScoreGainPopupCreator scoreGainPopupCreator;

    private int totalScore = 0;
    private Shooter shooter;

    private void Start()
    {
        shooter = FindObjectOfType<Shooter>();
        shooter.OnHitTargets += ComputeScore;
    }

    private void ComputeScore(List<Target> targetsHit)
    {
        var value = 0;
        var multiplier = 1f;

        foreach (var target in targetsHit)
        {
            value += target.ScoreValue;
            multiplier += target.ScoreMultiplier;
        }

        var score = (int)(value * multiplier);
        ProcessScore(score);
    }

    private void ProcessScore(int score)
    {
        scoreGainPopupCreator.Create(score, shooter.Origin, shooter.ImpactPoint);
        totalScore += score;
        scoreView.RefreshScoreText(totalScore);
    }

    private void OnDestroy()
    {
        shooter.OnHitTargets -= ComputeScore;
    }
}