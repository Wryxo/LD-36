using UnityEngine;
using System.Collections;

public class HamsterController : MonoBehaviour {
  public GameObject HamsterObject;
  public float WakeUpTimer;

  private bool wakingUp = false;

  void Awake() {
    wakingUp = true;
    Invoke("wakeUp", WakeUpTimer * 3);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && !wakingUp) {
      wakingUp = true;
      Invoke("wakeUp", WakeUpTimer);      
    }
  }

  private void wakeUp() {
    Debug.Log("log");
    if (!(transform.parent != null && transform.parent.name.Contains("Hodor"))) {
        GameObject hamster = Instantiate(HamsterObject, transform.position, Quaternion.identity) as GameObject;
      hamster.transform.SetParent(GameObject.Find("Hamsters").transform);  
      Destroy(gameObject);
    }
    wakingUp = false;
  }
}
