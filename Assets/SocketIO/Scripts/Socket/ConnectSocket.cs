﻿using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConnectSocket {
	
	private static ConnectSocket instance;
	private SocketIOComponent socket;
	private Dictionary<string, string> user = new Dictionary<string, string> ();

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
		socket.On ("registered", Registered);

		socket.On ("menu", ShowMenu);

		socket.On ("gotScore", ShowScore);
		socket.On ("scoreUpdated", UpdatedScore);

		socket.On ("gotCharacter", ShowCharacters);
//		socket.On ("setCharacter", ShowCharacters);

		// socket.On("inqueue", Queued);
		// socket.On("paired", Paired);


		socket.On ("error", Error);
		socket.On ("errorUsernameTaken", UsernameTaken);
	}

	public static ConnectSocket Instance{
		get {
			if (instance == null) {
				instance = new ConnectSocket ();
			}
			return instance;
		}
	}

	// public variables
	public void Login(string username, string password) {
		Debug.Log ("Logging in");
		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["username"] = username;
		data ["password"] = password;

		socket.Emit ("login", new JSONObject(data));
	}

	public void Register(string username, string password) {
		Debug.Log ("Registering");
		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["username"] = username;
		data ["password"] = password;

		socket.Emit ("register", new JSONObject (data));
	}
		
	public void GetScore() {
		Debug.Log ("Getting Score");

		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["id"] = user["_id"];

		socket.Emit ("getScore", new JSONObject (data));
	}

	public void UpdateScore(int win, int loss) {
		Debug.Log ("Update Score");

		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["id"] = user ["_id"];
		data ["wins"] = win.ToString ();
		data ["losses"] = loss.ToString ();

		socket.Emit ("updateScore", new JSONObject (data));
	}

	public void GetCharacters() {
		Debug.Log ("get characters");

		Dictionary<string, string> data = new Dictionary<string, string> ();

		data ["username"] = user ["username"];

		socket.Emit ("getCharacter", new JSONObject (data));
	}
		
//	public void SetCharacters() {
//		Debug.Log ("set characters");
//
//		Dictionary<string, string> data = new Dictionary<string, string> ();
//
//
//
//		socket.Emit ("setCharacter", new JSONObject (data));
//	}


	// Returned stuff
	private void TestOpen(SocketIOEvent e) {
		Debug.Log ("Opened!!");
	}

	private void LoggedIn(SocketIOEvent e) {
		Debug.Log ("[LoggedIn] returned: " + e.data);

		user = e.data.ToDictionary();

		LoginWindow loginWindow = windowManager.Open((int) Windows.LoginWindow - 1, false) as LoginWindow;
		loginWindow.NextWindow();
	}

	private void Registered(SocketIOEvent e) {
		Debug.Log ("[Registered] returned: " + e.data);

		user = e.data.ToDictionary();

		RegisterWindow registerWindow = windowManager.Open((int) Windows.RegisterWindow - 1, false) as RegisterWindow;
		registerWindow.NextWindow();
	}

	private void ShowScore(SocketIOEvent e) {
		Debug.Log("[ShowScore] returned: " + e.data);

		RecordWindow recordWindow = windowManager.Open((int) Windows.RecordWindow - 1, false) as RecordWindow;
		recordWindow.SetValues(e.data["wins"].ToString(), e.data["losses"].ToString(), e.data["streak"].ToString(), e.data["score"].ToString());
	}

	private void ShowMenu(SocketIOEvent e) {
		Debug.Log("[ShowMenu] returned: " + e.data);

		// MenuWindow menuWindow = windowManager.Open((int) Windows.MenuWindow - 1, false) as MenuWindow;
		// menuWindow.Open();
	}

	private void UpdatedScore(SocketIOEvent e) {
		Debug.Log ("[UpdatedScore] returned: " + e.data);


	}

	private void ShowCharacters(SocketIOEvent e) {
		Debug.Log ("[ShowCharacters] returned: " + e.data);

		// CharactersWindow charactersWindow = windowManager.Open((int) Windows.CharactersWindow -1, false) as CharactersWindow;
		// charactersWindow.Open();
	}

	// Errors
	private void Error(SocketIOEvent e) {
		// show a pop-up maybe
		Debug.Log (e.name + " " + e.data);
	}

	private void UsernameTaken(SocketIOEvent e) {

		Debug.Log ("[UsernameTaken] returned: " + e.data);
	}
}
