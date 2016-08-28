using UnityEngine;
using System.Collections.Generic;

public class ThrowingController : MonoBehaviour {
	public float Power, Angle;
	public GameObject Hodor;

	private Vector2 initPosition;
	private int facing = 1;
  private List<Transform> hodors = new List<Transform>();
  private Rigidbody2D rb;

	void Start () {
    initPosition = transform.position;
		rb = GetComponent<Rigidbody2D>();

    // generate hodors
    for (int i = 0; i < 5; i++) {
      GameObject hodorObject = Instantiate(
        Hodor,
        new Vector2(transform.position.x, transform.position.y + 0.75f + i * 2.0f / 3.0f),
        Quaternion.identity
        ) as GameObject;
      hodorObject.transform.SetParent(gameObject.transform);
      hodors.Add(hodorObject.transform);
    }
  }

  void FixedUpdate() {
    float xInput = Input.GetAxis("Horizontal");
    if (Mathf.Abs(xInput) > 0.001f)
      facing = xInput > 0 ? 1 : -1;

    if (Input.GetButton("Fire1")) {
      fireAllItems();   
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (onLayer(collision.gameObject, "Enemy")) {
      foreach (var item in GetComponentsInChildren<ItemController>()) {
        float direction = Random.Range(-45.0f, 45.0f) / 180 * Mathf.PI;
        item.GetComponent<Rigidbody2D>().AddForce(
          new Vector2(Mathf.Sin(direction), Mathf.Cos(direction)) * Power * 3,
          ForceMode2D.Impulse
        );
        item.Drop();
      }
      Invoke("die", 0.1f);
    }
  }

  void OnTriggerStay2D(Collider2D collision) {
    if ((onLayer(collision.gameObject, "Item") || onLayer(collision.gameObject, "ItemHamster")) &&
			  collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f) {
      //pickUpItem(collision.gameObject);
    }
  }

  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }

  private void die() { 
    transform.position = initPosition;
  }

  private void fireAllItems() {
    //for (int i = 0; i < items.Count; i++) {
    foreach(var item in GetComponentsInChildren<ItemController>()) {
      float direction = Angle / 180 * Mathf.PI;
      item.GetComponent<Rigidbody2D>().AddForce(
        new Vector2(Mathf.Sin(direction) * facing, Mathf.Cos(direction)) * Power,
        ForceMode2D.Impulse
      );
    }


  }

  public Transform GetHodor(GameObject item) {
    int position = GetComponentsInChildren<ItemController>().Length;
    return position >= 5 ? null : hodors[position];
  }
}
