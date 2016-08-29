using UnityEngine;

public class ScrollingParallaxController : MonoBehaviour {
	public GameObject Engine;
	public GameObject[] Images;

	public void TriggerCollisionWith(GameObject go) {
		if (go.GetInstanceID() == Images[0].GetInstanceID()) {
			Images[2].transform.position = new Vector3(
				Images[0].transform.position.x - Images[2].GetComponent<SpriteRenderer>().bounds.extents.x * 2,
				Images[2].transform.position.y,
				Images[2].transform.position.z
			);

			GameObject temp = Images[2];
			Images[2] = Images[1];
			Images[1] = Images[0];
			Images[0] = temp;
		} else if (go.GetInstanceID() == Images[2].GetInstanceID()) {
			Images[0].transform.position = new Vector3(
				Images[2].transform.position.x + Images[0].GetComponent<SpriteRenderer>().bounds.extents.x * 2,
				Images[0].transform.position.y,
				Images[0].transform.position.z
			);

			GameObject temp = Images[0];
			Images[0] = Images[1];
			Images[1] = Images[2];
			Images[2] = temp;
		}
	}
}
