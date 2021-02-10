using Pyoro.Trajectory;

public class Mosquito : Enemy
{
	protected override void Start()
	{
		base.Start();

		trajectory = GetComponent<Trajectory>();
		trajectory.Initialize(Destination);
	}

	private void Update()
	{
		trajectory.UpdatePosition();
	}
}