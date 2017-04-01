using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : GenericWindow {

	private RandomMapTester tester;

	protected override void Awake () {
		tester = GetComponent<RandomMapTester>();
		base.Awake();
	}

	public override void Open() {
		base.Open();
		tester.Reset();
		Camera.main.GetComponent<MoveCamera>().enabled = true;
	}

	public override void Close() {
		base.Close();
		tester.Shutdown();
		Camera.main.GetComponent<MoveCamera>().enabled = false;
	}
}
