﻿using UnityEngine;
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
			newPalette.ResetPalette();
			
			Debug.Log("Creating a Palette "+selectionPath);
		} else {
			Debug.Log("Can't create a Palette");
		}
		
	}
	
	public Texture2D source;
	public List<Color> palette = new List<Color>();
	public List<Color> newPalette = new List<Color>();
	
	private List<Color> BuildPalette(Texture2D texture){
		
		List<Color> palette = new List<Color> ();
		
		var colors = texture.GetPixels ();
		
		foreach (var color in colors) {
			
			if(!palette.Contains(color)){
				
				if(color.a == 1){
					palette.Add(color);
				}
			}
			
		}
		
		return palette;
		
	}
	
	public void ResetPalette(){
		palette = BuildPalette (source);
		newPalette = new List<Color> (palette);
	}
	
}

[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor{
	
	public ColorPalette colorPalette;
	
	void OnEnable(){
		colorPalette = target as ColorPalette;
	}
	
	override public void OnInspectorGUI(){
		
		GUILayout.Label ("Source Texture");
		
		colorPalette.source = EditorGUILayout.ObjectField (colorPalette.source, typeof(Texture2D), false) as Texture2D;
		
		
		EditorGUILayout.BeginHorizontal ();
		
		GUILayout.Label ("Current Color");
		GUILayout.Label ("New Color");
		
		EditorGUILayout.EndHorizontal ();
		
		for (var i = 0; i < colorPalette.palette.Count; i++) {
			
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.ColorField(colorPalette.palette[i]);
			
			colorPalette.newPalette[i] = EditorGUILayout.ColorField(colorPalette.newPalette[i]);
			
			EditorGUILayout.EndHorizontal();
		}

		if(GUILayout.Button("Revert Palette")){
			colorPalette.ResetPalette();
		}

		EditorUtility.SetDirty (colorPalette);
		
	}
	
}









