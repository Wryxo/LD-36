using UnityEngine;

public class ItemController : MonoBehaviour {
  private bool isCarried = false;
  private Transform oldParent;

  private void OnTriggerEnter2D(Collider2D collision) {
    if (onLayer(collision.gameObject, "Player") && !isCarried) {
      isCarried = true;
      oldParent = transform.parent;
      transform.SetParent(collision.GetComponent<ThrowingController>().Holder);
      transform.localPosition = Vector3.zero;
    }

    Debug.Log("enter" + collision.name);
  }

  /*private void OnTriggerExit2D(Collider2D collision) {
    if (onLayer(collision.gameObject, "Player") && isCarried) {
      isCarried = false;
    }
    transform.SetParent(oldParent);
    Debug.Log("exit" + collision.name);
  }*/

  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}
