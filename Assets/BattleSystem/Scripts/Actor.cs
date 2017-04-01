using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ScriptableObject {

	public new string name;
	public int health;
	public int maxHealth;
	public int gold;
	public Vector2 attackRange = Vector2.one;

	public bool alive {
		get {
			return health > 0;
		}
	}

	public void DecreaseHealth(int value) {
		health = Mathf.Max(health - value, 0); // no negatives
	}

	public void ResetHealth() {
		health = maxHealth;
	}

	public void IncreaseGold(int value) {
		gold += value;
	}

	public T Clone<T>() where T : Actor {
		var clone = ScriptableObject.CreateInstance<T>();

		clone.name = name;
		clone.health = health;
		clone.maxHealth = maxHealth;
		clone.gold = gold;
		clone.attackRange = attackRange;
		
		return clone;
	}
}
