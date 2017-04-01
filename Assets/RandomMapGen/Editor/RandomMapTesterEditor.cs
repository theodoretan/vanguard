using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(RandomMapTester))]
public class RandomMapTesterEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();

		var script = (RandomMapTester)target;

		if (GUILayout.Button ("Generate Island")) {
			if (Application.isPlaying) {
				script.MakeMap ();
			}
		}

		if (GUILayout.Button ("Create Player")) {
			if (Application.isPlaying) {
				script.CreatePlayer ();
			}
		}

		if (GUILayout.Button ("Reset")) {
			if (Application.isPlaying) {
				script.Reset ();
			}
		}

		if (GUILayout.Button ("Start Battle")) {
			if (Application.isPlaying) {
				script.StartBattle ();
			}
		}

		if (GUILayout.Button ("End Battle")) {
			if (Application.isPlaying) {
				script.EndBattle ();
			}
		}

	}

}
