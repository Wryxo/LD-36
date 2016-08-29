using UnityEngine;

public class ParallaxImageController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		transform.parent.GetComponent<ScrollingParallaxController>().TriggerCollisionWith(gameObject);
	}
}
