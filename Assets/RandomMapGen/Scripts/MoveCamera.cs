using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float speed = 4f;
	public GameObject target;

	private Vector3 startPos;
	private bool moving;

	void FixedUpdate(){

		if (Input.GetMouseButtonDown (1)) {
			startPos = Input.mousePosition;
			moving = true;
		}

		if (Input.GetMouseButtonUp (1) && moving) {
			moving = false;
		}

		if (moving) {

			Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - startPos);
			Vector3 move = new Vector3 (pos.x * speed, pos.y * speed, 0);
			transform.Translate (move, Space.Self);

		} else if (target != null) {

			var pos = target.transform.position;
			pos.z = Camera.main.transform.position.z;

			Camera.main.transform.position = pos;

		}

	}


}
