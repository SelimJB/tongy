using System.Collections;
using Pyoro.Trajectories;
using Script;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
	[SerializeField] private LifeManager lifeManager;
	[SerializeField] private Mosquito mosquitoPrefab;
	[SerializeField] private float timeBetweenTwoWaves = 5f;
	[SerializeField] private int swarmSize = 10;
	[SerializeField] PerlinTrajectoryParameters trajectoryParameters;

	void Start()
	{
		StartCoroutine(GenerateSwarm());
	}

	private IEnumerator GenerateSwarm()
	{
		yield return new WaitForSeconds(0.5f);
		var pos = new Vector2(Random.Range(0f, 1f) > 0.5f ? -15 : 15, Random.Range(-15f, 15f));

		var lifeReceptacle = lifeManager.FindNearestActiveLifeReceptacle(transform.position);
		if (lifeReceptacle == null)
			Debug.LogWarning("There is no life receptacle");

		CreateMosquitoSwarm(pos, mosquitoPrefab, lifeReceptacle, trajectoryParameters, swarmSize);

		yield return new WaitForSeconds(timeBetweenTwoWaves);
		yield return GenerateSwarm();
	}

	private static void CreateMosquitoSwarm(Vector3 position, Mosquito mosquitoPrefab, LifeReceptacle lifeReceptacle, PerlinTrajectoryParameters trajectoryParameters, int swarmSize)
	{
		for (var i = 0; i < swarmSize; i++)
		{
			var mosquito = TargetFactory.Create(position, mosquitoPrefab.gameObject).GetComponent<Enemy>();
			var perlinTrajectory = mosquito.GetComponent<PerlinTrajectory>();
			perlinTrajectory.TrajectoryParameters = trajectoryParameters;
			mosquito.LifeReceptacle = lifeReceptacle;
		}
	}
}