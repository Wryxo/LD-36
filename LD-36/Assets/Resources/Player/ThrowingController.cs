using UnityEngine;

public class ThrowingController : MonoBehaviour {
	public float Power, Angle;
	public Transform Holder;

	private Vector2 initPosition;
	private int facing = 1;
  private GameObject item = null;
	private Rigidbody2D rb;

	void Start () {
    initPosition = transform.position;
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		float xInput = Input.GetAxis("Horizontal");
		if (Mathf.Abs(xInput) > 0.001f)
			facing = xInput > 0 ? 1 : -1;

		if (Input.GetButton("Fire1") && item != null) {
			float direction = Angle / 180 * Mathf.PI;
			item.GetComponent<Rigidbody2D>().AddForce(
				new Vector2(Mathf.Sin(direction) * facing, Mathf.Cos(direction)) * Power + rb.velocity,
				ForceMode2D.Impulse
			);

			item = null;
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
    if (
			(
				onLayer(collision.gameObject, "Item") ||
				onLayer(collision.gameObject, "ItemHamster")
			) &&
			item == null &&
			collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f) {
      item = collision.gameObject;
    }
  }

  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }

  public GameObject GetItem() {
    return item;
  }
}
