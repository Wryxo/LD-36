using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {
  private float direction;
  public float Speed;

  void Start () {
    direction = (Random.Range(0, 2) % 2 == 0) ? 1 : -1;
  }
	
	void FixedUpdate () {
    transform.position = new Vector2(transform.position.x + Speed * direction, transform.position.y);
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Waypoint")) {
      direction *= -1;
    }
  }

  protected bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}