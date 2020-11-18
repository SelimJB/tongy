using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color hitColor;
    [SerializeField] int score = 100;
    [SerializeField] int multiplier = 1;

    public int ScoreValue => score;
    public int ScoreMultiplier => multiplier;

    public virtual void OnHit()
    {
        sprite.color = hitColor;
    }
}