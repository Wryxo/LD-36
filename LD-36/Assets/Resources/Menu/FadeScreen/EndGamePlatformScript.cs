using UnityEngine;

public class EndGamePlatformScript : MonoBehaviour {
	public Transform Machine;

	void FixedUpdate() {
		transform.position = new Vector3(Machine.position.x, transform.position.y);
	}
}
