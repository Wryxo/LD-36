using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  private Vector2 initPosition;
  private GameObject Item = null;

  public Transform Holder;

	void Start () {
    initPosition = transform.position;
	}
	
	void Update () {
	
	}

  private void OnTriggerEnter2D(Collider2D collision) {
    if (onLayer(collision.gameObject, "Item") && (Item == null)) {
      Item = collision.gameObject;
    }

    Debug.Log("item++" + collision.name);
  }

  protected bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }

}
