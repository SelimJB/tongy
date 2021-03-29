using Pyoro.Trajectories;
using UnityEngine;

namespace Pyoro.Targets
{
	public class Enemy : Target
	{
		private TrajectoryWithDestination trajectory;

		public LifeReceptacle LifeReceptacle { get; set; }
		private Vector3 Destination => LifeReceptacle != null ? LifeReceptacle.Position : Vector2.zero;
		protected override Trajectory Trajectory => trajectory;

		protected override void Start()
		{
			base.Start();

			if (LifeReceptacle != null)
				LifeReceptacle.OnDie += ChangeCible;

			Initialise(transform.position);
		}

		private void ChangeCible()
		{
			LifeReceptacle.OnDie -= ChangeCible;
			LifeReceptacle = LifeReceptacle.LifeManager.FindNearestActiveLifeReceptacle(transform.position);
			if (LifeReceptacle != null)
			{
				trajectory.ChangeDestination(LifeReceptacle.Position);
				LifeReceptacle.OnDie += ChangeCible;
			}
		}

		public override void Initialise(Vector3 position)
		{
			if (trajectory == null)
				trajectory = GetComponent<TrajectoryWithDestination>();

			transform.position = position;
			trajectory.Initialize(Destination);
		}

		public void Immobilize()
		{
			StartCoroutine(trajectory.Immobilize());
		}

		protected virtual void OnDestroy()
		{
			if (LifeReceptacle != null)
				LifeReceptacle.OnDie -= ChangeCible;
		}
	}
}