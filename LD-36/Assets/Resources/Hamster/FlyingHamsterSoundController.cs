using UnityEngine;

public class FlyingHamsterSoundController : MonoBehaviour {
	public AudioSource FanSource;
	public AudioSource HitSource;
	public float FanTime;
	private float nextFan;
	private bool play;

	void Start() {
		play = false;
		nextFan = FanTime;
	}

	void FixedUpdate() {
		nextFan -= Time.deltaTime;
		if (nextFan <= 0f) {
			play = true;
			nextFan = FanTime;
		}
	}

	void Update() {
		if (play) {
			FanSource.Play();
			play = false;
		}
	}

	public void SoundHit() {
		AudioSource.PlayClipAtPoint(HitSource.clip, transform.position, HitSource.volume);
	}
}
