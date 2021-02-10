using UnityEngine;
using UnityEngine.UI;

namespace Pyoro.Scoring
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private Text scoreText;
		[SerializeField] private Text gameOverMenuScoreText;

		public void RefreshScoreText(int score)
		{
			scoreText.text = score.ToString("000,000,000"); //, new CultureInfo("es-ES"));
			if (gameOverMenuScoreText != null)
				gameOverMenuScoreText.text = score.ToString("000,000,000"); //, new CultureInfo("es-ES"));
		}
	}
}