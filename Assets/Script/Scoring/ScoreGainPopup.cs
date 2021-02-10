using UnityEngine;

namespace Pyoro.Scoring
{
    public class ScoreGainPopup : MonoBehaviour
    {
        [SerializeField] private TextMesh text;

        private float duration = 1f;

        private void Start()
        {
            Destroy(gameObject, duration);
        }

        public void Initialize(int score, Vector2 position)
        {
            text.text = score.ToString();
            transform.position = position;
        }
    }
}