using System.Collections;
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
		var pos = new Vector2(Random.Range(0f, 1f) > 0.5f ? -22 : 22, Random.Range(-15f, 15f));
		var mosquitoSwarm = Instantiate(swarmPrefab, pos, Quaternion.identity);
		mosquitoSwarm.LifeManager = lifeManager;
		yield return new WaitForSeconds(15f);
		yield return GenerateSwarm();
	}
}