using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ScriptableObject {

	public new string name;
	public int health;
	public int maxHealth;
	public int gold;
//	public Vector2 attackRange = Vector2.one;
//
//	public Vector2 spAttackRange = Vector2.one;

	public int attack;
	public int spAttack;
	public int defence;
	public int exp;
	public int speed;
	public float hitRate;

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
//		clone.attackRange = attackRange;
//
//		clone.spAttackRange = spAttackRange;


		clone.attack = attack;
		clone.spAttack = spAttack;
		clone.defence = defence;
		clone.exp = exp;
		clone.speed = speed;
		clone.hitRate = hitRate;
		
		return clone;
	}

	public string ToJSONString() {
		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["name"] = name;
		data ["health"] = health.ToString();
		data ["maxHealth"] = maxHealth.ToString();
		data ["attack"] = attack.ToString ();
		data ["spAttack"] = spAttack.ToString ();
		data ["defence"] = defence.ToString ();
		data ["exp"] = exp.ToString ();
		data ["speed"] = speed.ToString ();
		data ["hitRate"] = hitRate.ToString ();

		return data.ToString ();
	}
}
