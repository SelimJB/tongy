using UnityEngine;
using UnityEngine.UI;

namespace TongueShooter.Scoring
{
	public class ScoreView : MonoBehaviour
	{
		[SerializeField] private Text scoreText;
		[SerializeField] private Text gameOverMenuScoreText;
		[SerializeField] private ScoreManager scoreManager;

		public void Start()
		{
			scoreManager.OnScoreIncr += RefreshScoreText;
		}

		public void RefreshScoreText(int score)
		{
			scoreText.text = score.ToString("000,000,000"); //, new CultureInfo("es-ES"));
			if (gameOverMenuScoreText != null)
				gameOverMenuScoreText.text = score.ToString("000,000,000"); //, new CultureInfo("es-ES"));
		}

		private void OnDestroy()
		{
			scoreManager.OnScoreIncr -= RefreshScoreText;
		}
	}
}