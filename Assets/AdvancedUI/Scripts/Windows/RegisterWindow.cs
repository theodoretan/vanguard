using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterWindow : GenericWindow {

    private string username;
    private string password;

    public void getUsername(string username) {
        this.username = username;
    }

    public void getPassword(string password) {
        this.password = password;
    }

    public void Register() {
        Debug.Log("Registering: "+this.username+" - "+this.password);

		if (!this.username.Equals ("") && !this.password.Equals ("")) {
			ConnectSocket.Instance.Register (this.username.ToLower(), this.password);
		} else {
			// error window
		}
        //		ConnectSocket.socket.Login (this.username, this.password);
        //        if (this.username == defaultUsername && this.password == defaultPassword) {
        //            OnNextWindow();
        //        }
    }

    public void NextWindow() {
        OnNextWindow();
    }

}
