using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
  private bool isCarried = false;
  private Transform objectParent;

  void Start () {
	
	}
	
	void Update () {
	
	}

  private void OnTriggerEnter2D(Collider2D collision) {
    if (onLayer(collision.gameObject, "Player") && !isCarried) {
      isCarried = true;
      objectParent = transform.parent;
      transform.SetParent(collision.GetComponent<PlayerController>().Holder);
      transform.localPosition = Vector3.zero;
    }

    Debug.Log("enter" + collision.name);
  }

  /*private void OnTriggerExit2D(Collider2D collision) {
    if (onLayer(collision.gameObject, "Player") && isCarried) {
      isCarried = false;
    }
    transform.SetParent(objectParent);
    Debug.Log("exit" + collision.name);
  }*/

  protected bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }
}
