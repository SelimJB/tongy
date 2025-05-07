using UnityEngine;

namespace TongueShooter.Trajectories
{
	public class StraightTrajectory : TrajectoryWithDestination
	{
		[Range(0f, 10f)] [SerializeField] float speed = 3.5f;
		[Range(0f, 10f)] [SerializeField] float vibrationAmplitude = 3f;

		public override void Initialize(Vector3 destination)
		{
			this.destination = destination;
		}

		protected override void UpdatePosition()
		{
			transform.position += (Vector3)Random.insideUnitCircle * Time.deltaTime * vibrationAmplitude
								+ (destination - transform.position).normalized * Time.deltaTime * speed;
		}
	}
}