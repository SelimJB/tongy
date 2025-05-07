using System.Collections;
using UnityEngine;

namespace TongueShooter.Background
{
	public abstract class BackgroundEvent : ScriptableObject
	{
		public abstract IEnumerator Trigger();
	}
}