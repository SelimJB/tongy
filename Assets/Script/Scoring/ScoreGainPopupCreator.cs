using UnityEngine;

namespace Pyoro.Scoring
{
	// FIXME : pooling
	public class ScoreGainPopupCreator : MonoBehaviour
	{
		[SerializeField] ScoreGainPopup scoreGainPopup;
		[SerializeField] private ScoreManager scoreManager;

		public void Start()
		{
			scoreManager.OnShootScoreIncr += Create;
		}

		public void Create(int score, Vector3 impactPoint)
		{
			var a = Instantiate(scoreGainPopup);
			a.Initialize(score, impactPoint);
		}

		private void OnDestroy()
		{
			scoreManager.OnShootScoreIncr -= Create;
		}
	}
}