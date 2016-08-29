using UnityEngine;

public class HamsterSoundController : MonoBehaviour {
	public AudioSource StepSource;
	public AudioSource HitSource;
	public float StepTime;
	private float nextStep;
	private bool play;

	void Start() {
		play = false;
		nextStep = StepTime;
	}

	void FixedUpdate() {
		nextStep -= Time.deltaTime;
		if (nextStep <= 0f) {
			play = true;
			nextStep = StepTime;
		}
	}

	void Update() {
		if (play) {
			StepSource.Play();
			play = false;
		}
	}

	public void SoundHit() {
		AudioSource.PlayClipAtPoint(HitSource.clip, transform.position, HitSource.volume);
	}
}
