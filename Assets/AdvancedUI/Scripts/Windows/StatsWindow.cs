using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class StatsWindow : GenericWindow {

	public Actor target;
	public Text valueLabel;

	public void UpdateStats() {
		if (target == null || valueLabel == null) {
			return;
		}

		var sb = new StringBuilder();

		sb.Append(target.health.ToString("D2"));
		sb.Append("/");
		sb.Append(target.maxHealth.ToString("D2"));
		sb.Append("\n");
		sb.Append(target.gold.ToString("D5"));

		valueLabel.text = sb.ToString();
	}
}
