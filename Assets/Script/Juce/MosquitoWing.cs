using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MosquitoWing : MonoBehaviour
{
	[SerializeField] private List<Sprite> wingSprites;
	[SerializeField] private Tween projectionTween;
	[SerializeField] private bool debug = true;

	[SerializeField] private float shootPowerProjectionTweaker = 1f;
	[SerializeField] private float proximityProjectionTweaker = 1f;
	[SerializeField] private float perlinSpeed = 0.075f;
	[SerializeField] private float perlinAmplitude = 0.5f;

	private Vector3 projectionVector;
	private Vector3 trajectory;
	private float seed;

	public void Initialize(HitInfo hitInfo)
	{
		var impactVector = transform.position - hitInfo.Origin;
		var aimVector = hitInfo.AimedPoint - hitInfo.Origin;
		var proximityIndex = impactVector.magnitude / aimVector.magnitude; // [0,1] - the closer the target, the closer this value is to 0 
		var projectionIntensityLimiter = 1 - proximityIndex / 3f;

		projectionVector = impactVector.normalized * (hitInfo.Power * shootPowerProjectionTweaker + proximityProjectionTweaker / proximityIndex) * projectionIntensityLimiter;

		trajectory = transform.position;
		seed = Random.value * 1000f;

		if (debug)
			Debug.DrawLine(transform.position, transform.position + projectionVector, Color.cyan, 2);
	}

	void Start()
	{
		GetComponent<SpriteRenderer>().sprite = wingSprites[Random.Range(0, wingSprites.Count)];
		StartCoroutine(projectionTween.Coroutine(x => trajectory += projectionVector * x));
	}

	void Update()
	{
		trajectory += Vector3.down * 0.015f;
		transform.position = trajectory + (2f * Mathf.PerlinNoise(Time.time * perlinSpeed, seed) - 1) * Vector3.up * perlinAmplitude + (2f * Mathf.PerlinNoise(seed, Time.time * perlinSpeed * 1.5f) - 1) * perlinAmplitude * 1.5f * Vector3.right;
		transform.rotation = Quaternion.Euler(0, 0, Mathf.PerlinNoise(Time.time * perlinSpeed + seed, 0) * 360);

		if (transform.position.y < -30) // TODO : use real value
			Destroy(gameObject);
	}
}