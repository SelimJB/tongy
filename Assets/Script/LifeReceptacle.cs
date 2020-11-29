using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LifeReceptacle : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inertColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Collider2D collider;
    public event Action OnVitalityLoss;

    private void Start()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.gameObject.GetComponent<Target>();
        if (target != null && target.IsEnemy)
        {
            SetInert(true);
            OnVitalityLoss?.Invoke();
        }
    }

    private void SetInert(bool value)
    {
        spriteRenderer.color = value ? inertColor : activeColor;
        collider.enabled = !value;
    }
}
