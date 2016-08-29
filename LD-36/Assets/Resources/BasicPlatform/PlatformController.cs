using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {
  public GameObject ItemObject;
  public GameObject WaypointObject;
  public bool isBreakable;
  public float Power;
  public float WaypointPositionUp;
  public float WaypointPositionSides;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && isBreakable) {
      die();
    }
  }

  private void die() {
    GameObject item = Instantiate(ItemObject, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform) as GameObject;
    item.GetComponent<Rigidbody2D>().AddForce(
      Vector2.up * Power,
			ForceMode2D.Impulse
		);
    spawnWaypoint(new Vector3(transform.position.x - WaypointPositionSides, transform.position.y + WaypointPositionUp, 0));
    spawnWaypoint(new Vector3(transform.position.x + WaypointPositionSides, transform.position.y + WaypointPositionUp, 0));
    Destroy(gameObject);
  }

  private void spawnWaypoint(Vector3 newPosition) {
    Instantiate(WaypointObject, newPosition, Quaternion.identity);
  }
}