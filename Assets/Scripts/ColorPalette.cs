using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;

[Serializable]
public class ColorPalette : ScriptableObject {

	[MenuItem("Assets/Create/Color Palette")]
	public static void CreateColorPalette(){

		if (Selection.activeObject is Texture2D) {

			var selectedTexture = Selection.activeObject as Texture2D;

			var selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

			selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

			var newPalette = CustomAssetUtil.CreateAsset<ColorPalette>(selectionPath);

			newPalette.source = selectedTexture;

			Debug.Log ("Creating a Palette"+selectionPath);
		} else {
			Debug.Log("Cant create a Palette");
		}
	}

	public Texture2D source;
	public List<Color> palette = new List<Color>();
	public List<Color> newPalette = new List<Color>();
}
