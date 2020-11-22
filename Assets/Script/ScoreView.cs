using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public void RefreshScoreText(int score)
    {
        scoreText.text = score.ToString("000,000,000");//, new CultureInfo("es-ES"));
    }
}