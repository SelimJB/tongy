using System;
using Pyoro.Enemies;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LifeReceptacle : MonoBehaviour
{
	[SerializeField] private LifeManager lifeManager;
	[SerializeField] private Color activeColor;
	[SerializeField] private Color inertColor;
	[SerializeField] private SpriteRenderer spriteRenderer;
	private Collider2D collider;
	private bool isInert;

	public Vector2 Position => transform.position;
	public bool IsInert => isInert;
	public LifeManager LifeManager => lifeManager;
	public event Action OnDie;

	private void Start()
	{
		collider = GetComponent<CircleCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var enemy = other.gameObject.GetComponent<Enemy>();
		if (enemy != null)
		{
			Die();
		}
	}

	private void Die()
	{
		SetInert(true);
		lifeManager.LoseHealth();
		OnDie?.Invoke();
	}

	private void SetInert(bool value)
	{
		isInert = value;
		spriteRenderer.color = value ? inertColor : activeColor;
		collider.enabled = !value;
	}
}