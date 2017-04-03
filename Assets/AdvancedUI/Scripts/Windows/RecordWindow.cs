using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordWindow : GenericWindow {

    public Text WinsValue;
    public Text LossesValue;
    public Text StreakValue;
    public Text ScoreValue;

    public void UpdateValues() {
		var socket = ConnectSocket.Instance;

		socket.GetScore ();
    }

//    public void ClearText() {
//        WinsValue.text = "";
//        LossesValue.text = "";
//        StreakValue.text = "";
//        ScoreValue.text = "";
//    }

    public override void Open() {
		base.Open();
//        ClearText();
//		UpdateValues ();
    }

	public void Start() {
		UpdateValues ();
	}

    public void BackButton() {
        OnPreviousWindow();
    }

	public void SetValues(string wins, string losses, string streak, string score) {
		 WinsValue.text = wins;
		 LossesValue.text = losses;
		 StreakValue.text = streak;
		 ScoreValue.text = score;
	}

}