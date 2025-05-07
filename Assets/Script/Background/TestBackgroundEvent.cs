using System.Collections;
using UnityEngine;

namespace TongueShooter.Background
{
	[CreateAssetMenu(menuName = "TongueShooter/Test BackgroundEvent")]
	public class TestBackgroundEvent : BackgroundEvent
	{
		public string message;

		public override IEnumerator Trigger()
		{
			yield return new WaitForSeconds(1);
			Debug.LogError(message);
			yield return new WaitForSeconds(1);
			Debug.LogError("Fin");
		}
	}
}