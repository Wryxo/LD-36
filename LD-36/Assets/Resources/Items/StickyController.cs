using UnityEngine;

public class StickyController : MonoBehaviour {
	public float BouncePower, BounceAngle;
	private bool stuck = false;

	void OnTriggerEnter2D(Collider2D collider) {
    GameObject thisGO = transform.parent.gameObject;
    GameObject otherGO = collider.gameObject;

    if (
			!onLayer(otherGO, "StaticItem") ||
			stuck
		)
			return;

		if (onLayer(thisGO, "ItemHamster") && onLayer(otherGO, "StaticItem")) {
			GameObject.FindGameObjectWithTag("Engine").GetComponent<MachineController>().HamsterCount += 1;
			Destroy(thisGO);
		} else if (!onLayer(thisGO, "ItemHamster") && otherGO.tag == "Bouncy") {
			float direction = BounceAngle / 180 * Mathf.PI;
			float facing = thisGO.GetComponent<Rigidbody2D>().velocity.x >= 0 ? 1 : -1;

			thisGO.GetComponent<Rigidbody2D>().AddForce(
				new Vector2(Mathf.Sin(direction) * facing, Mathf.Cos(direction)) * BouncePower,
				ForceMode2D.Impulse
			);
		} else if (
      !onLayer(thisGO, "ItemHamster") &&
			onLayer(otherGO, "StaticItem") &&
			(otherGO.tag == "Engine" || otherGO.tag == "Sticky")
		)	{
			Sprite otherSprite = otherGO.GetComponent<SpriteRenderer>().sprite;
			Sprite thisSprite = thisGO.GetComponent<SpriteRenderer>().sprite;
			float offset = otherGO.transform.lossyScale.x / (collider.gameObject.tag == "Engine" ? 1 : 2) + thisGO.transform.lossyScale.x / 2;

			Vector3 colVector = thisGO.transform.position - otherGO.transform.position;
			if (Mathf.Abs(colVector.x) > Mathf.Abs(colVector.y)) {
				if (colVector.x <= 0) {
					// left side
					thisGO.transform.position = new Vector3(
						otherGO.transform.position.x - offset,
						thisGO.transform.position.y
					);
					thisGO.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
				} else {
					// right side
					thisGO.transform.position = new Vector3(
						otherGO.transform.position.x + offset,
						thisGO.transform.position.y
					);
					thisGO.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
				}
			} else {
				if (colVector.y >= 0) {
					// top side
					thisGO.transform.position = new Vector3(
						thisGO.transform.position.x,
						otherGO.transform.position.y + offset
					);
					thisGO.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				} else {
					// bottom side
					thisGO.transform.position = new Vector3(
						thisGO.transform.position.x,
						otherGO.transform.position.y - offset
					);
					thisGO.transform.rotation = Quaternion.Euler(180f, 0f, 0f);
				}
			}

			thisGO.GetComponent<ItemController>().Drop();
			thisGO.transform.SetParent(GameObject.FindGameObjectWithTag("Engine").transform);

			thisGO.layer = LayerMask.NameToLayer("StaticItem");
			gameObject.layer = LayerMask.NameToLayer("StaticItem");
			thisGO.tag = thisGO.tag == "Fan" ? "Bouncy" : "Sticky";
			if (thisGO.tag == "Bouncy") {
				thisGO.GetComponent<Animator>().SetTrigger("Slowly");
			}
			thisGO.transform.position = new Vector3(
				thisGO.transform.position.x, thisGO.transform.position.y, -2f
			);

			thisGO.GetComponent<HingeJoint2D>().connectedBody = otherGO.GetComponent<Rigidbody2D>();
			thisGO.GetComponent<HingeJoint2D>().enabled = true;

			stuck = true;
		}
	}

	private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}
