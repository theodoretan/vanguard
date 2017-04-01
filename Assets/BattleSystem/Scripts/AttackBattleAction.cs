using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class AttackBattleAction : GenericBattleAction {

	public override void Action (Actor target1, Actor target2) {
		// base.Action(target1, target2);
		var attackValue = (int) Random.Range(target1.attackRange.x, target1.attackRange.y);
		target2.DecreaseHealth(attackValue);

		var sb = new StringBuilder();

		if (attackValue >= target1.attackRange.y - 1) {
			sb.Append("Critical hit! ");
		}

		sb.Append(target1.name);
		sb.Append(" atacks ");
		sb.Append(target2.name);
		sb.Append(". ");

		if (attackValue > 0) {
			sb.Append(target2.name);
			sb.Append(" loses ");
			sb.Append(attackValue);
			sb.Append(" hp.");
		} else {
			sb.Append(target1.name);
			sb.Append(" misses!");
		}

		
		message = sb.ToString();
	}
}
