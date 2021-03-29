using System.Collections.Generic;
using UnityEngine;

namespace Pyoro.Targets
{
	public class TargetFactory : MonoBehaviour
	{
		private Dictionary<string, Queue<Target>> availableEntityPerType = new Dictionary<string, Queue<Target>>();
		private Dictionary<string, GameObject> targetParents = new Dictionary<string, GameObject>();
		private static TargetFactory Instance { get; set; }

		public static void Release(Target target)
		{
			CheckType(target.TargetName);
			Queue<Target> availableEnemies = Instance.availableEntityPerType[target.TargetName];
			target.gameObject.SetActive(false);
			availableEnemies.Enqueue(target);
		}

		private static void CheckType(string name)
		{
			if (!Instance.availableEntityPerType.ContainsKey(name))
			{
				Instance.availableEntityPerType.Add(name, new Queue<Target>());
				var parent = new GameObject(name);
				parent.transform.parent = Instance.transform;
				Instance.targetParents.Add(name, parent);
			}
		}

		public static Target Create(Vector2 position, GameObject prefab, HitInfo hitInfo = null)
		{
			string name = prefab.GetComponent<Target>().TargetName;
			CheckType(name);

			Target target = null;
			Queue<Target> availableEnemies = Instance.availableEntityPerType[name];

			if (availableEnemies.Count > 0)
			{
				target = availableEnemies.Dequeue();
				target.gameObject.SetActive(true);
				target.ReInitialize(position, hitInfo);
			}

			if (target == null)
			{
				var go = Instantiate(prefab);
				target = go.GetComponent<Target>();
				target.transform.parent = Instance.gameObject.transform;
				if (hitInfo != null)
					target.Initialise(position, hitInfo);
				else
					target.Initialise(position);
			}

			target.transform.parent = Instance.targetParents[name].transform;

			return target;
		}

		// public static Target GetTarget<T>(Vector2 position, GameObject prefab) where T : Target
		// {
		// 	return GetTarget(position, prefab).GetComponent<T>();
		// }

		private void Awake()
		{
			if (Instance != null)
			{
				Debug.LogError("Multiple instance of singleton Factory");
				return;
			}

			Instance = this;
		}
	}
}