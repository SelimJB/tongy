using Script;
using UnityEngine;

namespace TongueShooter.Targets
{
	[RequireComponent(typeof(Target))]
	public class Mosquito : MonoBehaviour
	{
		[SerializeField] private MosquitoWing mosquitoWingPrefab;

		private Target target;

		public void LoseWings(HitInfo hitInfo)
		{
			var wingApparitionPerimeterSize = 0.5f;
			var p1 = transform.position + (Vector3)Random.insideUnitCircle * wingApparitionPerimeterSize;
			var p2 = transform.position + (Vector3)Random.insideUnitCircle * wingApparitionPerimeterSize;
			TargetFactory.Create(p1, mosquitoWingPrefab.gameObject, hitInfo);
			TargetFactory.Create(p2, mosquitoWingPrefab.gameObject, hitInfo);
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
}