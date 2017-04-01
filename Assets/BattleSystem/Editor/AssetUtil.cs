using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetUtil {

	public static void CreateScriptableObject<T>() where T : ScriptableObject {
		var asset = ScriptableObject.CreateInstance<T>();
		var path = AssetDatabase.GetAssetPath(Selection.activeObject);
		var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New" + typeof(T) + ".asset");
		AssetDatabase.CreateAsset(asset, assetPathAndName);
		Selection.activeObject = asset;
		EditorUtility.FocusProjectWindow();
		AssetDatabase.SaveAssets();
	}

}
