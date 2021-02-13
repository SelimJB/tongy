using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pyoro.Scoring
{
    public class ScoreManager : MonoBehaviour
    {
        public Action<int> OnScoreIncr;
        public Action<int, Vector3> OnShootScoreIncr;
        
        private int totalScore = 0;
        private Shooter shooter;

        private void Start()
        {
            shooter = FindObjectOfType<Shooter>();
            if (shooter != null)
                shooter.OnHitTargets += ComputeScore;
            else
                Debug.LogWarning("No shooter in the scene");
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
            OnShootScoreIncr?.Invoke(score, shooter.ImpactPoint);
            IncreaseScore(score);
        }

        public void IncreaseScore(int score)
        {
            totalScore += score;
            OnScoreIncr?.Invoke(totalScore);
        }

        private void OnDestroy()
        {
            shooter.OnHitTargets -= ComputeScore;
        }
    }
}