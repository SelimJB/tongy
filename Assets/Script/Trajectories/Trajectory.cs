using System.Collections;
using UnityEngine;

namespace Pyoro.Trajectories
{
	public interface IMoving
	{
		bool IsActive { get; set; }
	}

	public abstract class Trajectory : MonoBehaviour, IMoving
	{
		protected Vector3 destination;

		public abstract void Initialize(Vector3 destination);
		protected abstract void UpdatePosition();
		public abstract void ResetPosition();

		public virtual void ChangeDestination(Vector3 destination)
		{
			this.destination = destination;
		}

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
}