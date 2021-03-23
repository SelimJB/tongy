using System;
using System.Collections;
using System.Collections.Generic;
using Pyoro.Trajectories;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Shooter))]
public class Tongue : MonoBehaviour
{
	private Shooter shooter;

	private float tongueTipCircleRadius = 0.5f;

	private void Start()
	{
		shooter = GetComponent<Shooter>();
		shooter.OnHitTargets += Swallow;
	}

	private void OnDestroy()
	{
		shooter.OnHitTargets -= Swallow;
	}

	private void Swallow(List<Target> targets, HitInfo hitInfo)
	{
		foreach (var target in targets)
		{
			StartCoroutine(Swallow(target, hitInfo));
		}
	}

	private IEnumerator Swallow(Target target, HitInfo hitInfo)
	{
		yield return new WaitForEndOfFrame(); // OnHit must be triggered before the position modification

		var trajectory = target.GetComponent<IMoving>();
		if (trajectory != null) trajectory.IsActive = false;

		target.gameObject.transform.position = hitInfo.AimedPoint + (Vector3)Random.insideUnitCircle * tongueTipCircleRadius;
		var dir = hitInfo.Origin - target.transform.position;

		var duration = 0.2f; // TODO change
		var t = 0f;

		while (t < duration)
		{
			target.gameObject.transform.position += dir * Time.deltaTime / duration;
			t += Time.deltaTime;
			yield return null;
		}

		Destroy(target.gameObject);
	}
}