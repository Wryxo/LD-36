using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
  public GameObject ItemObject;
  public bool isBreakable;

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
    Instantiate(ItemObject, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

}
