using UnityEngine;

public class ParallaxController : MonoBehaviour {
	public GameObject Player;
	public Transform[] Bounds;

	private Vector3 relBounds;
	private Vector3 boundsCenter;
	private Vector3 extraSpace;

	void Start() {
		relBounds = Bounds[1].position - Bounds[3].position;
		boundsCenter = (Bounds[1].position + Bounds[3].position) / 2f;

		Vector3 extents = GetComponent<SpriteRenderer>().bounds.extents;
		extraSpace =  new Vector3(
			extents.x - relBounds.x,
			extents.y - relBounds.y
		);
	}

	void Update() {
		Vector3 relPlayer = Player.transform.position - Bounds[2].position;

		float percentX = 1 + (relPlayer.x / relBounds.x);
		float percentY = relPlayer.y / relBounds.y;

		transform.position = boundsCenter +
			new Vector3(
				extraSpace.x * (percentX - 0.5f) * 0.25f,
				extraSpace.y * (percentY - 0.5f) * 0.25f,
				transform.position.z
			);
	}
}
