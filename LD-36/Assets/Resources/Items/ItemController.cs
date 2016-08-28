using UnityEngine;

public class ItemController : MonoBehaviour {
  public GameObject Bumper;
  public float HodorDropRange;
  public GameObject Player;

  private bool isCarried = false;
  private Transform oldParent;
  private ThrowingController playerController;

  void Start() {
  }

  void FixedUpdate() {
    if (isCarried && distanceTo(transform.parent) > HodorDropRange) {
      Drop();
    }
  }
  private void OnTriggerStay2D(Collider2D collision) {
    if (
      onLayer(collision.gameObject, "Player") && 
      !isCarried && 
      gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.001f
      ) {
      playerController = collision.GetComponent<ThrowingController>();
      Transform hodor = playerController.GetHodor(gameObject);
      
      if (hodor != null) {
        isCarried = true;
        oldParent = transform.parent;
        transform.SetParent(hodor);
        transform.position = hodor.position;
        //Bumper.layer = LayerMask.NameToLayer("Item");
      }
    }
  }

  public void Drop() {
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