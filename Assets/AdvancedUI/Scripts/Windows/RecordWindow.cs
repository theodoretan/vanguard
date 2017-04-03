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
        // get values from server;

        // WinsValue.text = ;
        // LossesValue.text = "";
        // StreakValue.text = "";
        // ScoreValue.text = "";
    }

    public void ClearText() {
        WinsValue.text = "";
        LossesValue.text = "";
        StreakValue.text = "";
        ScoreValue.text = "";
    }

    public override void Open() {
        ClearText();
        UpdateValues();
        base.Open();
    }

    public void BackButton() {
        OnPreviousWindow();
    }

}