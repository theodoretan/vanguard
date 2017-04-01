using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBattleAction : ScriptableObject {

	public new string name;
	protected string message = "Undefined Battle Action Message";

	public virtual void Action(Actor target1, Actor target2) {
		// override with action logic
	}

	public override string ToString() {
		return message;
	}
}
