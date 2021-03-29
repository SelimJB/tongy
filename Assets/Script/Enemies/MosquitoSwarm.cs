using Pyoro.Trajectories;
using Script;
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

	private void CreateMosquitoSwarm()
	{
		var lifeReceptacle = LifeManager.FindNearestActiveLifeReceptacle(transform.position);
		if (lifeReceptacle == null)
			Debug.LogWarning("There is no life receptacle");

		for (var i = 0; i < swarmSize; i++)
		{
			var mosquito = TargetFactory.Create(transform.position, mosquitoPrefab.gameObject).GetComponent<Enemy>();
			var perlinTrajectory = mosquito.GetComponent<PerlinTrajectory>();
			perlinTrajectory.TrajectoryParameters = trajectoryParameters;
			mosquito.LifeReceptacle = lifeReceptacle;
			mosquito.transform.position = transform.position;
		}
	}
}