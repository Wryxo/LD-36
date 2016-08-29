using UnityEngine;

public class HamsterItemSoundController : MonoBehaviour {
	public AudioSource StoreSource;

	public void SoundStore() {
		AudioSource.PlayClipAtPoint(StoreSource.clip, transform.position, StoreSource.volume);
	}
}
