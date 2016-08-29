using UnityEngine;

public class CrashController : MonoBehaviour {
	public GameObject Particles;
	public int ParticleCount;

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("Item"))
			return;

		Vector2 collisionWeighted = collision.contacts[0].point;
		foreach (ContactPoint2D colPoint in collision.contacts) {
			collisionWeighted = (collisionWeighted + colPoint.point) / 2f;
		}
		collisionWeighted -= new Vector2(transform.position.x, transform.position.y);

		Vector3 emitVector = collisionWeighted.normalized * 360f;
		emitVector = new Vector3(emitVector.y, emitVector.x, emitVector.z);
		Particles.transform.localEulerAngles = emitVector;

		Particles.GetComponent<ParticleSystem>().Emit(ParticleCount);
	}
}
