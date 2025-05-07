using System.Collections;
using TongueShooter.Targets;
using TongueShooter.Trajectories;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
	private const float INITIAL_DELAY = 0.5f;
	private const float SPAWN_RANGE = 15f;

	[SerializeField] private LifeManager lifeManager;
	[SerializeField] private Mosquito mosquitoPrefab;
	[SerializeField] private float timeBetweenTwoWaves = 5f;
	[SerializeField] private int swarmSize = 10;
	[SerializeField] private PerlinTrajectoryParameters trajectoryParameters;

	private void Start()
	{
		StartCoroutine(GenerateSwarm());
	}

	private IEnumerator GenerateSwarm()
	{
		yield return new WaitForSeconds(INITIAL_DELAY);
		var pos = new Vector2(Random.Range(0f, 1f) > 0.5f ? -SPAWN_RANGE : SPAWN_RANGE, Random.Range(-SPAWN_RANGE, SPAWN_RANGE));

		var lifeReceptacle = lifeManager.FindNearestActiveLifeReceptacle(transform.position);
		if (lifeReceptacle == null)
		{
			Debug.LogWarning($"[{nameof(SwarmGenerator)}] No active life receptacle found");
			yield return new WaitForSeconds(timeBetweenTwoWaves);
			yield return GenerateSwarm();
			yield break;
		}

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