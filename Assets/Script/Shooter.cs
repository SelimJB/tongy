using System;
using System.Collections;
using System.Collections.Generic;
using Pyoro.Targets;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private float maxLength = 6f;
	[SerializeField] private TongueAnimator tongueAnimator;

	public event Action<List<Target>, HitInfo> OnHitTargets;

	private Vector2 sweepStartPoint;
	private Vector2 sweepEndPoint;
	private Vector2 sweepVector;
	private Color lineRendererColor;
	private Vector2 origin;

	public Vector2 Origin => origin;
	public float MaxLength => maxLength;

	private void Start()
	{
		origin = transform.position;
		lineRenderer.SetPosition(0, origin);
		lineRendererColor = lineRenderer.startColor;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
			OnShootInit();
		if (Input.GetMouseButton(0))
			OnShootPrep();
		if (Input.GetMouseButtonUp(0))
			OnShootRelease();
	}

	private void OnShootInit()
	{
		sweepStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnShootPrep()
	{
		sweepEndPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		sweepVector = Vector2.ClampMagnitude(sweepEndPoint - sweepStartPoint, maxLength);
		lineRenderer.SetPosition(1, origin + sweepVector);
		lineRenderer.endWidth = 1f / (sweepVector.magnitude + 1);
	}

	private void OnShootRelease()
	{
		var aimPoint = origin - sweepVector;
		var targetsHit = Shoot(aimPoint);
		tongueAnimator.PlayShootAnimation(aimPoint);
		StartCoroutine(ShootReleaseAnimation(aimPoint));
		if (targetsHit.Count > 0)
			HitTargets(targetsHit, aimPoint);
	}

	IEnumerator ShootReleaseAnimation(Vector3 aimPoint)
	{
		yield return new WaitForSeconds(0.02f);
		lineRenderer.endColor = lineRenderer.startColor = lineRendererColor;
		lineRenderer.startWidth = 1f;
		lineRenderer.widthCurve.keys[0].value = 1f;
		lineRenderer.SetPosition(1, origin - (Vector2)aimPoint.normalized * 0.7f);
	}

	private List<Target> Shoot(Vector2 aimPoint)
	{
		var direction = aimPoint - origin;
		var hits = Physics2D.RaycastAll(origin, direction.normalized, direction.magnitude);
		var targetsHit = new List<Target>();

		if (hits.Length != 0)
		{
			foreach (var hit in hits)
			{
				if (hit.collider != null)
				{
					var target = hit.collider.gameObject.GetComponent<Target>();
					if (target != null)
					{
						targetsHit.Add(target);
					}
				}
			}
		}

		return targetsHit;
	}

	private void HitTargets(List<Target> targets, Vector2 aimPoint)
	{
		var power = (aimPoint - origin).magnitude / maxLength;
		var hitInfo = new HitInfo(origin, aimPoint, power);
		OnHitTargets?.Invoke(targets, hitInfo);

		foreach (var target in targets)
		{
			target.OnHit(hitInfo);
		}
	}
}

public class HitInfo
{
	public Vector3 Origin { get; }
	public Vector3 AimedPoint { get; }
	public float Power { get; }

	public HitInfo(Vector3 origin, Vector3 aimedPoint, float power)
	{
		Origin = origin;
		AimedPoint = aimedPoint;
		Power = power;
	}
}