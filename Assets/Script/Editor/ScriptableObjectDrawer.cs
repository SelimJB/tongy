﻿using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawer
{
	private Editor editor;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.PropertyField(position, property, label, true);

		if (property.objectReferenceValue != null)
		{
			property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
		}

		if (property.isExpanded)
		{
			EditorGUI.indentLevel++;

			if (!editor)
				Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
			editor.OnInspectorGUI();

			EditorGUI.indentLevel--;
		}
	}
}