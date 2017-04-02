using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConnectSocket {
	
	private static ConnectSocket instance;
	private SocketIOComponent socket;

	private WindowManager windowManager {
		get {
			return GenericWindow.manager;
		}
	}

	private ConnectSocket() {
		GameObject go = GameObject.Find ("SocketIO");
		socket = go.GetComponent<SocketIOComponent> ();

		socket.On ("open", TestOpen);
		socket.On ("loggedIn", LoggedIn);
		socket.On ("error", Error);
	}

	public static ConnectSocket Instance{
		get {
			if (instance == null) {
				instance = new ConnectSocket ();
			}
			return instance;
		}
	}

	// Use this for initialization
//	void Start () {
//		GameObject go = GameObject.Find ("SocketIO");
//		socket = go.GetComponent<SocketIOComponent> ();
//
//		socket.On ("open", TestOpen);
//		socket.On ("loggedIn", LoggedIn);
//		socket.On ("error", Error);
//	}


	public void Login(string username, string password) {
		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["username"] = username;
		data ["password"] = password;

		socket.Emit ("login", new JSONObject(data));
	}

	private void TestOpen(SocketIOEvent e) {
		Debug.Log ("Opened!!");
	}

	private void LoggedIn(SocketIOEvent e) {
		Debug.Log ("returned: " + e.data);

		LoginWindow loginWindow = windowManager.Open((int) Windows.LoginWindow - 1, false) as LoginWindow;
		loginWindow.NextWindow();
	}

	private void Error(SocketIOEvent e) {
		Debug.Log (e.name + " " + e.data);
	}
}
