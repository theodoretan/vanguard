using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : GenericWindow {

    public void StartGame() {
        Debug.Log("Clicked Start Game");
		manager.Open((int) Windows.GameWindow - 1);
    }

    public void SetCharacters() {
        Debug.Log("Clicked Set Character");
        manager.Open(0);
    }

    public void ViewRecord() {
        Debug.Log("Clicked View Game");
		manager.Open((int) Windows.RecordWindow - 1);
    }

}
