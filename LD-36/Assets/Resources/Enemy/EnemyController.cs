using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
  public GameObject ItemObject;

  void Start () {
	
	}
	
	void Update () {
	
	}

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && !isMoving(collision.gameObject)) {
      die();
    }
  }

  private bool isMoving(GameObject item) {
    return (item.GetComponent<Rigidbody2D>().velocity.magnitude != 0);
  }

  private void die() {
    Instantiate(ItemObject, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
