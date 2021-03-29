using System.Collections;
using System.Collections.Generic;
using Pyoro.Targets;
using Pyoro.Trajectories;
using UnityEngine;

namespace Script.TestScript
{
	public class TestMosquitoResurection : MonoBehaviour
	{
		[SerializeField] private Shooter shooter;
		[SerializeField] private GameObject prefab;
		[SerializeField] private bool resurectWithFactory = true;

		void Start()
		{
			shooter.OnHitTargets += Resurect;
		}

		void Resurect(List<Target> targets, HitInfo hitInfo)
		{
			foreach (var t in targets)
			{
				if (t.GetComponent<Mosquito>())
					if (resurectWithFactory)
						StartCoroutine(ResurectWithFactory(t.transform.position));
					else
						StartCoroutine(Resurect(t.transform.position));
			}
		}

		IEnumerator Resurect(Vector3 pos)
		{
			yield return new WaitForSeconds(1);
			Instantiate(prefab, pos, Quaternion.identity);
		}

		IEnumerator ResurectWithFactory(Vector3 pos)
		{
			yield return new WaitForSeconds(4);
			TargetFactory.Create(pos, prefab);
		}
	}
}