using Pyoro.Trajectory;
using UnityEngine;

public class MosquitoSwarm : MonoBehaviour
{
	[SerializeField] int swarmSize = 10;
	[SerializeField] Mosquito mosquitoPrefab;
	[SerializeField] PerlinTrajectoryParameters trajectoryParameters;
	public LifeManager LifeManager { get; set; }

	void Start()
	{
		if (LifeManager == null)
			LifeManager = FindObjectOfType<LifeManager>();

		CreateMosquitoSwarm();
	}

	// TODO : moove into factory
	private void CreateMosquitoSwarm()
	{
		var lifeReceptacle = LifeManager.FindNearestActiveLifeReceptacle(transform.position);
		if (lifeReceptacle == null)
			Debug.LogWarning("There is no life receptacle");

		for (var i = 0; i < swarmSize; i++)
		{
			var mosquito = Instantiate(mosquitoPrefab, transform, true);
			var perlinTrajectory = mosquito.GetComponent<PerlinTrajectory>();
			perlinTrajectory.TrajectoryParameters = trajectoryParameters;
			mosquito.LifeReceptacle = lifeReceptacle;
			mosquito.transform.position = transform.position;
		}
	}
}