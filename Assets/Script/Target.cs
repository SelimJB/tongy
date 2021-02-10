using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color hitColor;
    [SerializeField] int score = 100;
    [SerializeField] float multiplier = 1;
    [SerializeField] bool isEnemy;

    public int ScoreValue => score;
    public float ScoreMultiplier => multiplier;
    public bool IsEnemy => isEnemy;

    public virtual void OnHit()
    {
        // sprite.color = hitColor;
        Destroy(gameObject);
    }
}