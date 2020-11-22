using UnityEngine;

// FIXME : pooling
public class ScoreGainPopupCreator : MonoBehaviour
{
    [SerializeField] ScoreGainPopup scoreGainPopup;

    public void Create(int score, Vector2 origin, Vector2 impactPoint)
    {
        var a = Instantiate(scoreGainPopup);
        a.Initialize(score, impactPoint);
    }
}