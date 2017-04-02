using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginWindow : GenericWindow {

    private string username;
    private string password;

    private string defaultUsername = "admin";
    private string defaultPassword = "pass";

    public void getUsername(string username) {
        this.username = username;
    }

    public void getPassword(string password) {
        this.password = password;
    }

    public void Login() {
        Debug.Log(this.password + " " + this.username);
        if (this.username == defaultUsername && this.password == defaultPassword) {
            OnNextWindow();
        }
    }

}
