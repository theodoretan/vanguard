using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWindow : GenericWindow {

    public void StartGame() {
        Debug.Log("Clicked Start Game");
        manager.Open(4);
    }

    public void SetCharacters() {
        Debug.Log("Clicked Set Character");
        manager.Open(0);
    }

    public void ViewRecord() {
        Debug.Log("Clicked View Game");
        manager.Open(0);
    }

}
