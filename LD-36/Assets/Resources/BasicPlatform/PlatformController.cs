using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
  public GameObject ItemObject;
  public bool isBreakable;
  public float Power;

	void Start () {
	
	}
	
	void Update () {
	
	}

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && isBreakable) {
      die();
    }
    Debug.Log(collision.gameObject.layer + " " + LayerMask.NameToLayer("Player"));
  }

  private void die() {
    GameObject item = Instantiate(ItemObject, transform.position, Quaternion.identity) as GameObject;
    item.GetComponent<Rigidbody2D>().AddForce(
      Vector2.up * Power,
			ForceMode2D.Impulse
		);
    Destroy(gameObject);
  }

}
