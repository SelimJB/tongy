using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LifeReceptacle : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color hitColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Collider2D collider;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.gameObject.GetComponent<Target>();
        if (target != null && target.IsEnemy)
        {
            Kill();
        }
    }

    private void Kill()
    {
        spriteRenderer.color = hitColor;
        collider.enabled = false;
    }

    public void Revive()
    {
        spriteRenderer.color = baseColor;
        collider.enabled = true;
    }
}
