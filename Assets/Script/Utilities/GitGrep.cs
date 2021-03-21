using UnityEditor;
using UnityEngine;

public class GitGrep
{
	[MenuItem("Assets/Copy Git Grep Guid", false, 100000)]
	public static void CopyGitGrepGUID()
	{
		string command = "";
		int i = 0;

		foreach (var item in Selection.assetGUIDs)
		{
			command += "git grep " + item + (i < Selection.assetGUIDs.Length ? "\r" : "");
			i++;
		}

		Debug.Log(command);
		EditorGUIUtility.systemCopyBuffer = command;
	}
}