using System;
using System.Collections.Generic;
using Pyoro.Scoring;
using UnityEngine;

namespace Pyoro.Background
{
	public class BackgroundManager : MonoBehaviour
	{
		[SerializeField] private List<BackgroundEventContainer> backgroundEvents; // TODO : scriptable object

		[SerializeField] private ScoreManager scoreManager;

		private void Start()
		{
			scoreManager.OnScoreIncr += TriggerEventIfNeeded;
			// TODO : SORT backgroundEvents list
			// backgroundEvents.Sort(x => x.scoreThreshold);
		}

		private void TriggerEventIfNeeded(int score)
		{
			foreach (var backgroundEvent in backgroundEvents)
				if (backgroundEvent.scoreThreshold <= score && !backgroundEvent.isLaunched)
				{
					StartCoroutine(backgroundEvent.backgroundEvent.Trigger());
					backgroundEvent.isLaunched = true; // TODO REMOVE
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
		public bool isLaunched; // TODO : Better system, should not iterate on all the list on each score incr
	}
}