using UnityEngine;
using System.Collections;

public class HamsterController : MonoBehaviour {
  public GameObject HamsterObject;
  public float WakeUpTimer;
  public bool wasThrown;

  private bool wakingUp = false;

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && wasThrown && !wakingUp) {
      wakingUp = true;
      Invoke("wakeUp", WakeUpTimer);      
    }
  }

  private void wakeUp() {
    if (!(transform.parent != null && transform.parent.name.Contains("Hodor"))) {
      Instantiate(HamsterObject, transform.position, Quaternion.identity);
      Destroy(gameObject);
    }    
  }
}
