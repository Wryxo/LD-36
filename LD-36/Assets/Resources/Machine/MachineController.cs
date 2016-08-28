using UnityEngine;

public class MachineController : MonoBehaviour {
	public int HamsterCount;
	public float HamsterPower;

	private Rigidbody2D rb;
	private bool flying = false;
	private ItemController[] items;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (flying) Fly();
	}

	public void Fly() {
		if (!flying) {
			flying = true;
			rb.isKinematic = false;
			items = GetComponentsInChildren<ItemController>();
		}

		foreach (ItemController ic in items) {
			if (ic.gameObject.tag != "Bouncy") continue;

			Rigidbody2D gorb = ic.GetComponent<Rigidbody2D>();
			rb.AddForceAtPosition(
				gorb.transform.up * HamsterPower * HamsterCount / items.Length,
				gorb.transform.position
			);
		}
	}
}
