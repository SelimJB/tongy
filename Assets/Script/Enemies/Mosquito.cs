using UnityEngine;

[RequireComponent(typeof(Target))]
public class Mosquito : MonoBehaviour
{
	[SerializeField] private MosquitoWing mosquitoWingPrefab;

	private Target target;

	public void LoseWings(HitInfo hitInfo)
	{
		// TODO move factory
		var wingApparitionPerimeterSize = 0.5f;
		var w1 = Instantiate(mosquitoWingPrefab, transform.position + (Vector3)Random.insideUnitCircle * wingApparitionPerimeterSize, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		w1.Initialize(hitInfo);
		var w2 = Instantiate(mosquitoWingPrefab, transform.position + (Vector3)Random.insideUnitCircle * wingApparitionPerimeterSize, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		w2.Initialize(hitInfo);
	}

	private void Start()
	{
		target = GetComponent<Target>();
		target.onHit += LoseWings;
	}

	protected void OnDestroy()
	{
		target.onHit -= LoseWings;
	}
}