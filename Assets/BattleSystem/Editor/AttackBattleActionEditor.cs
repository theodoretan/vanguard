using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AttackBattleAction))]
public class AttackBattleActionEditor : Editor {

	[MenuItem("Assets/Create/Attack Action")]
	public static void CreateAction() {
		AssetUtil.CreateScriptableObject<AttackBattleAction>();
	}
}
