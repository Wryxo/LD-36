using UnityEngine;
using System.Collections;

public class Flying : MonoBehaviour {
  private float facing;
  private Vector3 direction;

  public GameObject HamsterObject;
  public GameObject FanObject;
  public float Speed;

  void Start () {
    float angle = Random.Range(0f, 359f) / 180 * Mathf.PI;
    facing = direction.x >= 0f ? 1 : -1;
    direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    transform.localScale = new Vector2(transform.localScale.x * facing, transform.localScale.y);
  }

  void FixedUpdate() {
    transform.position += direction * Speed;
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) {
      float angle = Random.Range(0f, 359f) / 180 * Mathf.PI;
      direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

      facing = direction.x >= 0f ? 1 : -1;
      transform.localScale = new Vector2(facing, transform.localScale.y);
    }
  }

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && !isMoving(collision.gameObject)) {
      die(); 
    }
  }

  private bool isMoving(GameObject item) {
    return (item.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f);
  }

  private void die() {
    /* dirty fix */
    GetComponent<SpriteRenderer>().enabled = false;
    Vector3 position = transform.position;
    transform.position = new Vector3(5000,5000,-5000);
    /**/
    Instantiate(HamsterObject, position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
    Instantiate(FanObject, position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
    Destroy(gameObject);
  }
}
