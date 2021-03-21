using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Tween
{
	[SerializeField] AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
	[SerializeField] float duration = 1;
	[SerializeField] float amplitude = 1;

	public IEnumerator Coroutine(Action<float> Transformation)
	{
		float t = 0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			var v = curve.Evaluate(t / duration) * amplitude;
			if (Transformation != null)
				Transformation(v);
			else
				Debug.LogError("The transformation Action is Empty !");
			yield return null;
		}
	}
}