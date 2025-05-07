using System;
using System.Collections.Generic;
using TongueShooter.Scoring;
using UnityEngine;

// WIP : The background will evolve/change depending on the score
namespace TongueShooter.Background
{
	public class BackgroundManager : MonoBehaviour
	{
		[SerializeField] private List<BackgroundEventContainer> backgroundEvents;
		[SerializeField] private ScoreManager scoreManager;

		private int currentEventIndex;

		private void Start()
		{
			scoreManager.OnScoreIncr += TriggerEventIfNeeded;
			backgroundEvents.Sort((a, b) => a.scoreThreshold.CompareTo(b.scoreThreshold));
		}

		private void TriggerEventIfNeeded(int score)
		{
			// We only check the events that haven't been triggered yet
			while (currentEventIndex < backgroundEvents.Count &&
			       backgroundEvents[currentEventIndex].scoreThreshold <= score)
			{
				var currentEvent = backgroundEvents[currentEventIndex];
				StartCoroutine(currentEvent.backgroundEvent.Trigger());
				currentEventIndex++;
			}
		}

		private void OnDestroy()
		{
			scoreManager.OnScoreIncr -= TriggerEventIfNeeded;
		}
	}

	[Serializable]
	public class BackgroundEventContainer
	{
		[SerializeField] public BackgroundEvent backgroundEvent;
		[SerializeField] public int scoreThreshold;
	}
}