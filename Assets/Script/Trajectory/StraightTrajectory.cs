using UnityEngine;

namespace Pyoro.Trajectory
{
	public class StraightTrajectory : Trajectory
	{
		[Range(0f, 10f)] [SerializeField] float speed = 3.5f;
		[Range(0f, 10f)] [SerializeField] float vibrationAmplitude = 3f;

		private Vector3 initialPosition;

		public override void Initialize(Vector3 destination)
		{
			initialPosition = transform.position;
			this.destination = destination;
		}

		public override void ResetPosition()
		{
			transform.position = initialPosition;
		}

		public override void UpdatePosition()
		{
			transform.position += (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude
								+ (destination - transform.position).normalized * Time.deltaTime * speed;
		}
	}
}