using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[Serializable]
public class ColorPalette : ScriptableObject {

	[MenuItem("Assets/Create/Color Palette")]
	public static void CreateColorPalette(){

		if (Selection.activeObject is Texture2D) {
			Debug.Log ("Creating a Palette");
		} else {
			Debug.Log("Cant create a Palette");
		}
	}
}
