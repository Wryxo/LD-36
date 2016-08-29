using UnityEngine;

public class ItemSoundController : MonoBehaviour {
	public AudioSource HitSource;

	public void SoundHit() {
		AudioSource.PlayClipAtPoint(HitSource.clip, transform.position, HitSource.volume);
	}
}
