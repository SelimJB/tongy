using UnityEngine;

namespace Pyoro.Trajectories
{
	public class WingTrajectory : Trajectory
	{
		[SerializeField] private float perlinSpeed = 0.075f;
		[SerializeField] private float perlinAmplitude = 0.5f;

		private Vector3 position;
		private float initialRotation;

		public Vector3 Position { get => position; set => position = value; }
		private float seed;

		public void Initialize()
		{
			position = transform.position;
			initialRotation = transform.rotation.z;
		}

		protected override void UpdatePosition()
		{
			position += Vector3.down * 0.015f;
			transform.position = position + (2f * Mathf.PerlinNoise(Time.time * perlinSpeed, seed) - 1) * Vector3.up * perlinAmplitude + (2f * Mathf.PerlinNoise(seed, Time.time * perlinSpeed * 1.5f) - 1) * perlinAmplitude * 1.5f * Vector3.right;
			transform.rotation = Quaternion.Euler(0, 0, Mathf.PerlinNoise(initialRotation + Time.time * perlinSpeed + seed, 0) * 360);
		}
	}
}