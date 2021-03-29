using System.Collections;
using UnityEngine;

namespace Pyoro.Trajectories
{
	public class PerlinTrajectory : TrajectoryWithDestination
	{
		[SerializeField] public PerlinTrajectoryParameters trajectoryParameters;

		private Vector3 vibration;
		private Vector3 progressionTrajectory;
		private float perlinNoiseXSeed;
		private float perlinNoiseYSeed;
		private float magnitudeLimiter = 1f; // Is used when the mosquito has reached his goal, so as not to nomalize mangnitudes which are below 1, so that the attraction effect of the goal is quickly limited
		private float perlinSpeedVariation;

		public PerlinTrajectoryParameters TrajectoryParameters { get { return trajectoryParameters; } set { trajectoryParameters = value; } }

		public override void Initialize(Vector3 destination)
		{
			this.destination = destination;
			progressionTrajectory = transform.position;
			vibration = Vector3.zero;
			perlinNoiseXSeed = Random.Range(0f, 10000f);
			perlinNoiseYSeed = Random.Range(0f, 10000f);
			perlinSpeedVariation = Random.Range(-trajectoryParameters.perlinSpeedVariation, trajectoryParameters.perlinSpeedVariation);
		}
		
		protected override void UpdatePosition()
		{
			float pulsation = 0;
			if (TrajectoryParameters.easeType == PerlinTrajectoryParameters.EaseType.Cosinus)
				pulsation = (Mathf.Cos(Time.fixedTime * (Mathf.PI * 2) * (1 / TrajectoryParameters.pulsationSpeed)) + 1) / 2;
			else if (TrajectoryParameters.easeType == PerlinTrajectoryParameters.EaseType.Function)
				pulsation = Ease(Time.fixedTime, TrajectoryParameters.pulsationSpeed);
			else if (TrajectoryParameters.easeType == PerlinTrajectoryParameters.EaseType.Curve)
				pulsation = TrajectoryParameters.easeCurve.Evaluate((Time.fixedTime % TrajectoryParameters.pulsationSpeed) / TrajectoryParameters.pulsationSpeed);

			var perlinTrajectory = (Mathf.PerlinNoise(Time.time * (TrajectoryParameters.perlinSpeed + perlinSpeedVariation) + perlinNoiseXSeed, 0) - 0.5f) * TrajectoryParameters.perlinAmplitude * Vector3.up
									+ (Mathf.PerlinNoise(0, Time.time * (TrajectoryParameters.perlinSpeed + perlinSpeedVariation) + perlinNoiseYSeed) - 0.5f) * TrajectoryParameters.perlinAmplitude * Vector3.right;

			if ((destination - transform.position).magnitude < magnitudeLimiter)
				magnitudeLimiter = (destination - transform.position).magnitude;

			progressionTrajectory += (destination - transform.position).normalized * Time.deltaTime * TrajectoryParameters.speed * magnitudeLimiter;

			vibration = (Vector3)Random.insideUnitCircle * Time.deltaTime * TrajectoryParameters.vibrationAmplitude;

			transform.position = progressionTrajectory + perlinTrajectory * (pulsation * TrajectoryParameters.pulsationIntensity + (1 - TrajectoryParameters.pulsationIntensity)) + vibration;
		}

		public static float EaseIn(float t) => 1 - Mathf.Pow(t, 2);

		public static float EaseOut(float t) => 1 - Mathf.Pow(t - 1, 2);

		public float Ease(float t, float pulsationSpeed)
		{
			var quotient = (int)(t / (pulsationSpeed / 2f));
			var remainder = t - quotient * (pulsationSpeed / 2f);
			if (quotient % 2 == 0)
				return EaseIn(remainder / (pulsationSpeed / 2f));
			else
				return EaseOut(remainder / (pulsationSpeed / 2f));
		}

		public override void ChangeDestination(Vector3 destination)
		{
			this.destination = destination;
			magnitudeLimiter = 1;
		}

		public override IEnumerator Immobilize()
		{
			var initialSpeed = trajectoryParameters.speed;
			trajectoryParameters.speed = 0f;

			var speedDegression = 0.5f;
			var delta = initialSpeed / speedDegression;

			while (trajectoryParameters.speed > 0)
			{
				trajectoryParameters.speed -= Time.deltaTime * delta;
			}

			trajectoryParameters.speed = 0f;
			
			yield return new WaitForSeconds(2f);

			while (trajectoryParameters.speed < initialSpeed)
			{
				trajectoryParameters.speed += Time.deltaTime * delta;
			}

			trajectoryParameters.speed = initialSpeed;
		}
	}
}