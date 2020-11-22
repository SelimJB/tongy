using UnityEngine;

public class ScoreGainPopup : MonoBehaviour
{
    [SerializeField] private TextMesh text;

    private float duration = 2f;

    private void Start()
    {
        Destroy(this.gameObject, duration);
    }

    public void Initialize(int score, Vector2 position)
    {
        text.text = score.ToString();
        transform.position = position;
    }
}
