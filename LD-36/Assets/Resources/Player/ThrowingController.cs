using UnityEngine;
using System.Collections.Generic;

public class ThrowingController : MonoBehaviour {
	public float Power, Angle;
	public GameObject Hodor;
  public List<Transform> Hodors = new List<Transform>();

  private Vector2 initPosition;
  private bool dying;
	private int facing = 1;
  private Rigidbody2D rb;
  private PlatformerMotor2D platformMotor;
  private BoxCollider2D boxCollider;

  void Start () {
    initPosition = transform.position;
		rb = GetComponent<Rigidbody2D>();
    platformMotor = GetComponent<PlatformerMotor2D>();
    boxCollider = GetComponent<BoxCollider2D>();

    // generate hodors
    for (int i = 0; i < 5; i++) {
      GameObject hodorObject = Instantiate(
        Hodor,
        new Vector2(transform.position.x, transform.position.y + 1.4f + i * 1.0f),
        Quaternion.identity
        ) as GameObject;
      hodorObject.transform.SetParent(gameObject.transform);
      Hodors.Add(hodorObject.transform);
    }
  }

  void FixedUpdate() {
    if (!dying) {
      float xInput = Input.GetAxis("Horizontal");
      if (Mathf.Abs(xInput) > 0.001f)
        facing = xInput > 0 ? 1 : -1;

      if (Input.GetButton("Fire1")) {
        fireAllItems();
      }
    }
	}

  void OnTriggerEnter2D(Collider2D collision) {
    // hit enemy
    if (onLayer(collision.gameObject, "EnemyCollider") && !dying) {
      dying = true;
      // drop items
      foreach (var item in GetComponentsInChildren<ItemController>()) {
        float direction = Random.Range(-45.0f, 45.0f) / 180 * Mathf.PI;
        item.GetComponent<Rigidbody2D>().AddForce(
          new Vector2(Mathf.Sin(direction), Mathf.Cos(direction)) * Power * 3,
          ForceMode2D.Impulse
        );
        item.Drop();
      }

      // throw player
      platformMotor.enabled = false;
      boxCollider.enabled = false;

      rb.velocity = Vector3.zero;
      rb.AddForce(
        Vector2.up * 40,
        ForceMode2D.Impulse
      );
      GetComponent<PlayerSoundController>().SoundHit();
      Invoke("Die", 2.0f);
    }
  }

  private bool onLayer(GameObject go, string layer) {
    return go.layer == LayerMask.NameToLayer(layer);
  }

  private void fireAllItems() {
    foreach(var item in GetComponentsInChildren<ItemController>()) {
      float direction = Angle / 180 * Mathf.PI;
      item.GetComponent<Rigidbody2D>().AddForce(
        new Vector2(Mathf.Sin(direction) * facing, Mathf.Cos(direction)) * Power +
          (GetComponent<PlatformerMotor2D>().velocity * item.GetComponent<Rigidbody2D>().mass),
        ForceMode2D.Impulse
      );
      item.Drop();
    }
  }

  public Transform GetHodor(GameObject item) {
    int position = GetComponentsInChildren<ItemController>().Length;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Hodors[4].localPosition.y, LayerMask.GetMask("Floor"));
    int count = 5;
    if (hit.collider != null)
    {
      for (int i = 0; i < 5; i++)
      {
        if (Hodors[i].position.y > hit.point.y)
        {
          count = i;
          break;
        }
      }
    }
    for (int i = count; i < 5; i++)
    {
      var ic = Hodors[i].GetComponentInChildren<ItemController>();
        if (ic != null)
          ic.Drop();
    }
    return position >= count ? null : Hodors[position];
  }

  public void Die() {
    transform.position = initPosition;
    platformMotor.enabled = true;
    boxCollider.enabled = true;
    dying = false;
  }
}