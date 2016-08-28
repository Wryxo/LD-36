using UnityEngine;
using System.Collections;

public class HamsterController : MonoBehaviour {
  public GameObject HamsterObject;
  public float WakeUpTimer;
  public bool wasThrown;

  private bool wakingUp = false;

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && wasThrown && !wakingUp) {
      Invoke("wakeUp", WakeUpTimer);      
    }
  }

  private void wakeUp() {
    wakingUp = true;
    Instantiate(HamsterObject, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
