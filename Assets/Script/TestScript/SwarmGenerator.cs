using System.Collections;
using Script;
using UnityEngine;

public class SwarmGenerator : MonoBehaviour
{
	[SerializeField] private MosquitoSwarm swarmPrefab;
	[SerializeField] private LifeManager lifeManager;
	[SerializeField] private float timeBetweenTwoWaves = 5f;
	
	void Start()
	{
		StartCoroutine(GenerateSwarm());
	}

	private IEnumerator GenerateSwarm()
	{
		yield return new WaitForSeconds(0.5f);
		var pos = new Vector2(Random.Range(0f, 1f) > 0.5f ? -15 : 15, Random.Range(-15f, 15f));
		var mosquitoSwarm = Instantiate(swarmPrefab, pos, Quaternion.identity);
		mosquitoSwarm.LifeManager = lifeManager;
		yield return new WaitForSeconds(timeBetweenTwoWaves);
		yield return GenerateSwarm();
	}
}