using System.Collections.Generic;
using TongueShooter.Trajectories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TongueShooter.Targets
{
	public class MosquitoWing : Target
	{
		private const float Y_POSITION_LIMIT = -30f;
		private const float PROJECTION_INTENSITY_DIVISOR = 3f;

		[SerializeField] private List<Sprite> wingSprites;
		[SerializeField] private Tween projectionTween;
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

#if UNITY_EDITOR
			Debug.DrawLine(transform.position, transform.position + projectionVector, Color.cyan, 2);
#endif
		}

		private Vector3 CalculateProjectionVector(HitInfo hitInfo)
		{
			var impactVector = transform.position - hitInfo.Origin;
			var aimVector = hitInfo.AimedPoint - hitInfo.Origin;
			var proximityIndex = impactVector.magnitude / aimVector.magnitude;
			var projectionIntensityLimiter = 1 - proximityIndex / PROJECTION_INTENSITY_DIVISOR;
			return impactVector.normalized * ((hitInfo.Power * shootPowerProjectionTweaker + proximityProjectionTweaker / proximityIndex) *
			                                  projectionIntensityLimiter);
		}

		private void Update()
		{
			if (transform.position.y < Y_POSITION_LIMIT)
				TargetFactory.Release(this);
		}
	}
}