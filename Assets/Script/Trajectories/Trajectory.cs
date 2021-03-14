using UnityEngine;

namespace Pyoro.Trajectories
{
	public abstract class Trajectory : MonoBehaviour
	{
		protected Vector3 destination;

		public abstract void Initialize(Vector3 destination);
		public abstract void UpdatePosition();
		public abstract void ResetPosition();

		public virtual void ChangeDestination(Vector3 destination)
		{
			this.destination = destination;
		}
	}
}