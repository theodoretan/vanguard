using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class DifficultyWindow : GenericWindow {

	public ToggleGroup difficultyGroup;

	public float inputDelay = .3f;

	private float delay = 0;

	public int difficulty{
		get {
			var total = difficultyGroup.transform.childCount;

			for (var i = 0; i < total; i++) {
				var toggle = difficultyGroup.transform.GetChild (i).GetComponent<Toggle> ();
				if (toggle.isOn)
					return i;
			}

			return 0;
		}
		set{
			value = (int)Mathf.Repeat (value, difficultyGroup.transform.childCount);

			var currentSelection = difficultyGroup.ActiveToggles ().FirstOrDefault ();

			if (currentSelection != null) {

				currentSelection.isOn = false;

			}

			currentSelection = difficultyGroup.gameObject.transform.GetChild (value).GetComponent<Toggle> ();
			currentSelection.isOn = true;

			Debug.Log ("Difficulty " + value);
		}
	}

	public override void Open ()
	{

		difficulty = PlayerPrefs.GetInt ("Difficulty", 0);

		base.Open ();
	}


	public void OnSelect(){
		OnNextWindow ();
		PlayerPrefs.SetInt ("Difficulty", difficulty);
	}

	// Update is called once per frame
	void Update () {

		delay += Time.deltaTime;

		if (delay > inputDelay) {

			var newDifficulty = difficulty;

			var hDir = Input.GetAxis ("Horizontal");

			if (hDir > 0) {
				newDifficulty++;
			} else if (hDir < 0) {
				newDifficulty--;
			}

			if (newDifficulty != difficulty) {
				difficulty = newDifficulty;
			}

			delay = 0;


		}


		if (Input.GetButtonDown ("Submit")) {
			OnSelect ();
		}
	}
}
