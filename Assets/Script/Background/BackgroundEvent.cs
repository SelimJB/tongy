using System.Collections;
using UnityEngine;

namespace Pyoro.Background
{
	public abstract  class BackgroundEvent : ScriptableObject
	{
		public abstract IEnumerator Trigger();
	} 
}