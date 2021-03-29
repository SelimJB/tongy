using System.Collections.Generic;
using Pyoro.Trajectories;
using Script;
using Script.Juce;
using UnityEngine;
using Random = UnityEngine.Random;

public class MosquitoWing : Target
{
	[SerializeField] private List<Sprite> wingSprites;
	[SerializeField] private Tween projectionTween;

	[SerializeField] private bool debug = true;
	[SerializeField] private float shootPowerProjectionTweaker = 1f;
	[SerializeField] private float proximityProjectionTweaker = 1f;

	private WingTrajectory wingTrajectory;

	protected override Trajectory Trajectory => wingTrajectory;

	public override void Initialise(Vector3 pos, HitInfo hitInfo)
	{
		if (wingTrajectory == null)
			wingTrajectory = GetComponent<WingTrajectory>();

		GetComponent<SpriteRenderer>().sprite = wingSprites[Random.Range(0, wingSprites.Count)];

		transform.position = pos;
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

		wingTrajectory.Initialize();
		var projectionVector = CalculateProjectionVector(hitInfo);
		StartCoroutine(projectionTween.Coroutine(x => wingTrajectory.Position += projectionVector * x));

		if (debug)
			Debug.DrawLine(transform.position, transform.position + projectionVector, Color.cyan, 2);
	}

	private Vector3 CalculateProjectionVector(HitInfo hitInfo)
	{
		var impactVector = transform.position - hitInfo.Origin;
		var aimVector = hitInfo.AimedPoint - hitInfo.Origin;
		var proximityIndex = impactVector.magnitude / aimVector.magnitude; // [0,1] - the closer the target, the closer this value is to 0 
		var projectionIntensityLimiter = 1 - proximityIndex / 3f;
		return impactVector.normalized * (hitInfo.Power * shootPowerProjectionTweaker + proximityProjectionTweaker / proximityIndex) * projectionIntensityLimiter;
	}

	private void Update()
	{
		if (transform.position.y < -30) // TODO : use real value
			TargetFactory.Release(this);
	}
}