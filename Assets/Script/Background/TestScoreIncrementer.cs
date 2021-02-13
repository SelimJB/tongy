using Pyoro.Scoring;
using UnityEngine;

namespace Pyoro.Background
{
	public class TestScoreIncrementer : MonoBehaviour
	{
		private ScoreManager scoreManager;

		private void Start()
		{
			scoreManager = FindObjectOfType<ScoreManager>();
		}

		private void OnGUI()
		{
			if (GUI.Button(new Rect(200, 0, 200, 100), "Incr"))
			{
				scoreManager.IncreaseScore(500);
			}
		}
	}
}