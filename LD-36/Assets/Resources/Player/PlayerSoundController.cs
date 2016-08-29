using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
	public AudioSource HitSource;
	public AudioSource JumpSource;

	void Start() {
		GetComponent<PlatformerMotor2D>().onJump = SoundJump;
	}

	public void SoundHit() {
		HitSource.Play();
	}

	public void SoundJump() {
		AudioSource.PlayClipAtPoint(JumpSource.clip, transform.position, JumpSource.volume);
	}
}
