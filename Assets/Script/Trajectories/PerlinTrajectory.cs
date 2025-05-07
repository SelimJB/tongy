using System.Collections;
using UnityEngine;

namespace TongueShooter.Trajectories
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
		private float speed;

		public PerlinTrajectoryParameters TrajectoryParameters { get { return trajectoryParameters; } set { trajectoryParameters = value; } }

		public override void Initialize(Vector3 destination)
		{
			this.destination = destination;
			progressionTrajectory = transform.position;
			vibration = Vector3.zero;
			perlinNoiseXSeed = Random.Range(0f, 10000f);
			perlinNoiseYSeed = Random.Range(0f, 10000f);
			perlinSpeedVariation = Random.Range(-trajectoryParameters.PerlinSpeedVariation, trajectoryParameters.PerlinSpeedVariation);
			speed = TrajectoryParameters.Speed;
		}

		protected override void UpdatePosition()
		{
			float pulsation = 0;
			if (TrajectoryParameters.Ease == PerlinTrajectoryParameters.EaseType.Cosinus)
				pulsation = (Mathf.Cos(Time.fixedTime * (Mathf.PI * 2) * (1 / TrajectoryParameters.PulsationSpeed)) + 1) / 2;
			else if (TrajectoryParameters.Ease == PerlinTrajectoryParameters.EaseType.Function)
				pulsation = Ease(Time.fixedTime, TrajectoryParameters.PulsationSpeed);
			else if (TrajectoryParameters.Ease == PerlinTrajectoryParameters.EaseType.Curve)
				pulsation = TrajectoryParameters.EaseCurve.Evaluate((Time.fixedTime % TrajectoryParameters.PulsationSpeed) / TrajectoryParameters.PulsationSpeed);

			var perlinTrajectory = (Mathf.PerlinNoise(Time.time * (TrajectoryParameters.PerlinSpeed + perlinSpeedVariation) + perlinNoiseXSeed, 0) - 0.5f) * TrajectoryParameters.PerlinAmplitude * Vector3.up
									+ (Mathf.PerlinNoise(0, Time.time * (TrajectoryParameters.PerlinSpeed + perlinSpeedVariation) + perlinNoiseYSeed) - 0.5f) * TrajectoryParameters.PerlinAmplitude * Vector3.right;

			if ((destination - transform.position).magnitude < magnitudeLimiter)
				magnitudeLimiter = (destination - transform.position).magnitude;

			progressionTrajectory += (destination - transform.position).normalized * Time.deltaTime * speed * magnitudeLimiter;

			vibration = (Vector3)Random.insideUnitCircle * Time.deltaTime * TrajectoryParameters.VibrationAmplitude;

			transform.position = progressionTrajectory + perlinTrajectory * (pulsation * TrajectoryParameters.PulsationIntensity + (1 - TrajectoryParameters.PulsationIntensity)) + vibration;
		}

		private static float EaseIn(float t) => 1 - Mathf.Pow(t, 2);

		private static float EaseOut(float t) => 1 - Mathf.Pow(t - 1, 2);

		private float Ease(float t, float pulsationSpeed)
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
			var initialSpeed = trajectoryParameters.Speed;
			speed = initialSpeed;

			var speedDegression = 0.5f;
			var delta = initialSpeed / speedDegression;

			while (speed > 0)
			{
				speed -= Time.deltaTime * delta;
			}

			speed = 0f;

			yield return new WaitForSeconds(6f);

			while (speed < initialSpeed)
			{
				speed += Time.deltaTime * delta;
			}

			speed = initialSpeed;
		}
	}
}