using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
  private float direction;

  public GameObject ItemObject;
  public float Speed;

  void Start() {
    direction = (Random.Range(0, 2) % 2 == 0) ? 1 : -1;
  }

  void FixedUpdate() {
    transform.position = new Vector2(transform.position.x + Speed * direction, transform.position.y);
  }

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Waypoint")) {
      direction *= -1;
    } else if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && !isMoving(collision.gameObject)) {
      die();
    }
  }

  private bool isMoving(GameObject item) {
    return (item.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f);
  }

  private void die() {
    Instantiate(ItemObject, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}



