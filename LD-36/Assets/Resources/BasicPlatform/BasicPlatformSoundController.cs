using UnityEngine;

public class BasicPlatformSoundController : MonoBehaviour {
	public AudioSource HitSource;

	public void SoundHit() {
		AudioSource.PlayClipAtPoint(HitSource.clip, transform.position, HitSource.volume);
	}
}
