using System.Collections;
using Pyoro.Enemies;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
	[SerializeField] private MosquitoSwarm swarmPrefab;
	[SerializeField] private LifeManager lifeManager;

	void Start()
	{
		StartCoroutine(GenerateSwarm());
	}

	private IEnumerator GenerateSwarm()
	{
		var pos = new Vector2(Random.Range(0f, 1f) > 0.5f ? -28 : 28, Random.Range(-18f, 18f));
		var mosquitoSwarm = Instantiate(swarmPrefab, pos, Quaternion.identity);
		mosquitoSwarm.LifeManager = lifeManager;
		yield return new WaitForSeconds(10f);
		yield return GenerateSwarm();
	}
}