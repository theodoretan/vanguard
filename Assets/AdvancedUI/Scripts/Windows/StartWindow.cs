using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartWindow : GenericWindow {

	public Button continueButton;

	public override void Open ()
	{
		// connect to socket
		var _ = ConnectSocket.Instance;

		var canContinue = true;

		continueButton.gameObject.SetActive (canContinue);

		if (continueButton.gameObject.activeSelf) {
			firstSelected = continueButton.gameObject;
		}


		base.Open ();
	}

	public void NewGame(){
		OnNextWindow ();
	}

	public void Continue(){
		Debug.Log ("Continue Pressed");
        manager.Open((int) Windows.RegisterWindow -1);
	}

	public void Options(){
		Debug.Log ("Options Pressed");
	}


	
}
