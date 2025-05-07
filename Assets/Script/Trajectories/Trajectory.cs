using System.Collections;
using UnityEngine;

namespace TongueShooter.Trajectories
{
	public abstract class Trajectory : MonoBehaviour
	{
		protected abstract void UpdatePosition();

		void Update()
		{
			if (IsActive)
				UpdatePosition();
		}

		public virtual IEnumerator Immobilize()
		{
			// TODO : Improve by adding creating immobility / escape pattern 
			IsActive = false;
			yield return new WaitForSeconds(2f);
			IsActive = true;
		}

		public bool IsActive { get; set; } = true;
	}

	public abstract class TrajectoryWithDestination : Trajectory
	{
		protected Vector3 destination;
		protected Vector3 Destination => destination;

		public virtual void ChangeDestination(Vector3 destination)
		{
			this.destination = destination;
		}

		public abstract void Initialize(Vector3 destionation);
	}
}