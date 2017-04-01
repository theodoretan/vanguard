using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour {

	[HideInInspector]
	public GenericWindow[] windows;
	public int currentWindowID;
	public int defaultWindowID;

	public GenericWindow GetWindow(int value){
		return windows [value];
	}

	private void ToggleVisability(int value, bool closeAllOpen = true){
		var total = windows.Length;

		if (closeAllOpen) {
			for (var i = 0; i < total; i++) {
				var window = windows [i];

				if (window.gameObject.activeSelf) {
					 window.Close();
				}
			}
		}

		GetWindow(value).Open();
		
	}

	public GenericWindow Open(int value, bool closeAllOpen = true){
		if (value < 0 || value >= windows.Length)
			return null;

		currentWindowID = value;

		ToggleVisability (currentWindowID, closeAllOpen);

		return GetWindow (currentWindowID);
	}

	void Start(){
		GenericWindow.manager = this;
		Open (defaultWindowID);
	}
}
