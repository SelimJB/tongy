using Pyoro.Trajectories;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private LifeReceptacle lifeReceptacle;
	protected Trajectory trajectory;

	public LifeReceptacle LifeReceptacle { get => lifeReceptacle; set => lifeReceptacle = value; }
	protected Vector3 Destination => lifeReceptacle != null ? lifeReceptacle.Position : Vector2.zero;

	protected virtual void Start()
	{
		if (lifeReceptacle != null)
			lifeReceptacle.OnDie += ChangeCible;

		trajectory = GetComponent<Trajectory>();
		trajectory.Initialize(Destination);
	}

	private void ChangeCible()
	{
		lifeReceptacle.OnDie -= ChangeCible;
		lifeReceptacle = lifeReceptacle.LifeManager.FindNearestActiveLifeReceptacle(transform.position);
		if (lifeReceptacle != null)
		{
			trajectory.ChangeDestination(lifeReceptacle.Position);
			lifeReceptacle.OnDie += ChangeCible;
		}
	}

	protected virtual void OnDestroy()
	{
		if (lifeReceptacle != null)
			lifeReceptacle.OnDie -= ChangeCible;
	}
}