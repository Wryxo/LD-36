﻿using UnityEngine;
using UnityEngine.UI;

public class MachineController : MonoBehaviour {
	public int HamsterCount;
	public float HamsterPower;
	public float MaxAirTime;
	public Text LabelMass;
	public Text LabelAirborne;
	public Text LabelDistance;
	public Animator RestartButtonAnimator;
	public GameObject HamsterInWheel;
    public GameObject Score;
    public GameObject ScorePanel;

    private Rigidbody2D rb;
	private bool flying = false;
	private bool dead = false;
	private ItemController[] items;
  	private Vector3 initPosition;

	private float totalMass;
	private float airTime;
	private Vector3 furthestPoint;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		initPosition = transform.position;
		totalMass = rb.mass;
		airTime = 0f;
	}

	void FixedUpdate() {
		if (flying) Fly();
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.gameObject.tag == "EndGamePlatform")
			die();
	}

	public void Fly() {
    if (dead) return;
		if (!flying) {
      flying = true;
			rb.isKinematic = false;
			items = GetComponentsInChildren<ItemController>();

			bool hasFan = false;
			foreach (ItemController ic in items) {
        foreach (BoxCollider2D collider in ic.GetComponentsInChildren<BoxCollider2D>()) {
          collider.enabled = false;
          if (ic.tag == "Bouncy") {
          	ic.GetComponent<Animator>().SetTrigger("Turn It Up!");
						hasFan = true;
          }
          if (HamsterCount > 0) {
	          foreach (var ac in GetComponentsInChildren<Animator>(true)) {
	          	ac.SetTrigger("StartUp");
	          }
          }
        }
				totalMass += ic.GetComponent<Rigidbody2D>().mass;
			}

			if (hasFan)
				GetComponent<MachineSoundController>().Flying = true;
		}
		if (Vector3.Distance(initPosition, transform.position) > Vector3.Distance(initPosition, furthestPoint))
			furthestPoint = transform.position;

		airTime += Time.deltaTime;

		foreach (ItemController ic in items) {
			if (ic.gameObject.tag != "Bouncy") continue;

			Rigidbody2D gorb = ic.GetComponent<Rigidbody2D>();
			rb.AddForceAtPosition(
				gorb.transform.up * HamsterPower * HamsterCount / items.Length,
				gorb.transform.position
			);
		}

		if (airTime >= MaxAirTime)
			die();
    updateUI();
	}

	private void die() {
    dead = true;
    foreach (ItemController ic in items) {
			if (ic.GetComponent<HingeJoint2D>() != null)
				ic.GetComponent<HingeJoint2D>().enabled = false;
		}

		Score.GetComponent<Text>().text = "" + (int)(totalMass *10+airTime*100+ Vector3.Distance(initPosition, furthestPoint));
		ScorePanel.SetActive(true);
		RestartButtonAnimator.SetTrigger("Display");

		GetComponent<MachineSoundController>().Dead = true;
	}

	private void updateUI() {
		LabelMass.text = string.Format("TOTAL MASS:\n {0:0.00} kg", totalMass);
		LabelAirborne.text = string.Format("AIRBORNE FOR:\n {0:0.00}s", airTime);
		LabelDistance.text = string.Format("TRAVELED:\n {0:0.00}m", Vector3.Distance(initPosition, furthestPoint));
	}

	public void EnableHamsterPower() {
		HamsterInWheel.SetActive(true);
	}
}
