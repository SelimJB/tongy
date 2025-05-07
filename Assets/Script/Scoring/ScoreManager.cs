using System;
using System.Collections.Generic;
using TongueShooter.Targets;
using UnityEngine;

namespace TongueShooter.Scoring
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

		private void ComputeScore(List<Target> targetsHit, HitInfo hitInfo)
		{
			var value = 0;
			var multiplier = 1f;

			foreach (var target in targetsHit)
			{
				value += target.ScoreValue;
				multiplier += target.ScoreMultiplier;
			}

			var score = (int)(value * multiplier);
			if (score > 0)
			{
				OnShootScoreIncr?.Invoke(score, hitInfo.AimedPoint);
				IncreaseScore(score);
			}
		}

		public void IncreaseScore(int score)
		{
			totalScore += score;
			OnScoreIncr?.Invoke(totalScore);
		}

		private void OnDestroy()
		{
			if (shooter != null)
				shooter.OnHitTargets -= ComputeScore;
		}
	}
}