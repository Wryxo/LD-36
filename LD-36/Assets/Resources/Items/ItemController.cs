using UnityEngine;

public class ItemController : MonoBehaviour {
  public GameObject Bumper;
  private bool isCarried = false;
  private Transform oldParent;

  void FixedUpdate() {
    if (isCarried && distanceTo(transform.parent) > 1) {
      drop();
    }
  }
  private void OnTriggerStay2D(Collider2D collision) {
    if (
      onLayer(collision.gameObject, "Player") && 
      !isCarried && 
      gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f
      ) {
      ThrowingController playerComponent = collision.GetComponent<ThrowingController>();
      if (playerComponent.GetItem() == gameObject || playerComponent.GetItem() == null) { 
        isCarried = true;
        oldParent = transform.parent;
        transform.SetParent(playerComponent.Holder);
        transform.localPosition = Vector3.zero;
        Bumper.layer = LayerMask.NameToLayer("Item");
      }
    }
  }

  private void drop() {
    isCarried = false;
    transform.SetParent(oldParent);
    Bumper.layer = LayerMask.NameToLayer("StaticItem");
  }

  private float distanceTo(Transform other) {
    return (other.position - transform.position).magnitude;
  }

  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}