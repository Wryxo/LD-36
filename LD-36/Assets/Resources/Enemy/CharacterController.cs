using UnityEngine;

public class CharacterController : MonoBehaviour {
  private float direction;

  public GameObject ItemObject;
  public float Speed;

  void Start() {
    direction = (Random.Range(0, 2) % 2 == 0) ? -1 : 1;
    transform.localScale = new Vector2(transform.localScale.x * direction, transform.localScale.y);
  }

  void FixedUpdate() {
    transform.position = new Vector2(transform.position.x + Speed * direction, transform.position.y);
  }

  void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Waypoint")) {
      direction *= -1;
      transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    } else if (collision.gameObject.layer == LayerMask.NameToLayer("Item") && !isMoving(collision.gameObject)) {
      if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        GetComponent<EnemySoundController>().SoundHit();
      else if (gameObject.layer == LayerMask.NameToLayer("Hamster"))
        GetComponent<HamsterSoundController>().SoundHit();
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
    transform.position = new Vector3(5000, 5000, -5000);
    /**/
    Instantiate(ItemObject, position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform);
    Destroy(gameObject);
  }
}



