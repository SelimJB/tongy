using System;
using TongueShooter.Trajectories;
using UnityEngine;

namespace TongueShooter.Targets
{
	public class Target : MonoBehaviour
	{
		[SerializeField] private int score = 100;
		[SerializeField] private float multiplier = 1;
		[SerializeField] private string targetName; // used for pooling

		public event Action<HitInfo> onHit;
		public int ScoreValue => score;
		public float ScoreMultiplier => multiplier;

		private Collider2D collider;

		protected virtual Trajectory Trajectory { get; }
		public string TargetName => string.IsNullOrEmpty(targetName) ? name : targetName;

		protected virtual void Start()
		{
			collider = GetComponent<Collider2D>();
		}

		public virtual void OnHit(HitInfo hitInfo)
		{
			onHit?.Invoke(hitInfo);
		}

		public void EnableCollider(bool enable)
		{
			collider.enabled = enable;
		}

		public virtual void Initialise(Vector3 pos)
		{
			transform.position = pos;
		}

		public virtual void Initialise(Vector3 pos, HitInfo hitInfo)
		{
			Initialise(pos);
		}

		public void ReInitialize(Vector3 pos, HitInfo hitInfo = null)
		{
			EnableCollider(true);

			if (Trajectory != null)
				Trajectory.IsActive = true;

			if (hitInfo != null)
				Initialise(pos, hitInfo);
			else Initialise(pos);
		}
	}
}