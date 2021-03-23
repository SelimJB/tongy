using System.Collections;
using UnityEngine;

public class TongueAnimator : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private Color color;

	private Vector3 origin;

	void Start()
	{
		origin = transform.position;
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
	}

	public void PlayShootAnimation(Vector3 aimPoint)
	{
		StartCoroutine(ShootAnimation(aimPoint));
	}

	private IEnumerator ShootAnimation(Vector3 aimPoint)
	{
		lineRenderer.SetPosition(1, aimPoint);
		var pos = aimPoint;

		var duration = 0.2f; // TODO change
		var t = 0f;
		var dir = origin - aimPoint;

		while (t < duration)
		{
			pos += dir * Time.deltaTime / duration;
			lineRenderer.SetPosition(1, pos);
			t += Time.deltaTime;
			yield return null;
		}
		lineRenderer.SetPosition(0, origin);
		lineRenderer.SetPosition(1, origin);
	}
}