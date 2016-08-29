using UnityEngine;

public class MachineSoundController : MonoBehaviour {
	public AudioSource FanSource;
	public AudioSource CrashSource;
	public float FanTime;

	public bool Flying;
	public bool Dead;

	private float nextFan;
	private bool play;
	private bool reallyDead;

	void Start() {
		Flying = false;
		Dead = false;
		play = false;
		reallyDead = false;
		nextFan = FanTime;
	}

	void FixedUpdate() {
		if (!Flying || Dead) return;
		nextFan -= Time.deltaTime;
		if (nextFan <= 0f) {
			play = true;
			nextFan = FanTime;
		}
	}

	void Update() {
		if (Dead && !reallyDead) {
			CrashSource.Play();
			reallyDead = true;
		}
		else if (play) {
			FanSource.Play();
			play = false;
		}
	}
}
