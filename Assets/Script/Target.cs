using System;
using UnityEngine;

public class Target : MonoBehaviour
{
	[SerializeField] int score = 100;
	[SerializeField] float multiplier = 1;

	public event Action<HitInfo> onHit;
	public int ScoreValue => score;
	public float ScoreMultiplier => multiplier;

	public virtual void OnHit(HitInfo hitInfo)
	{
		onHit?.Invoke(hitInfo);
	}
}