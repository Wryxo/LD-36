using PC2D;
using UnityEngine;

public class ChangeToEndGame : StateMachineBehaviour {
	private bool triggered = false;

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (triggered || animator.IsInTransition(0)) return;

		getTagged("Player").SetActive(false);
		getTagged("Level").SetActive(false);

    getTagged("MainCamera").GetComponent<CameraFollow>().Target = getTagged("Engine").transform;
    getTagged("MainCamera").GetComponent<CameraFollow>().FollowingPlayer = false;
    getTagged("MainCamera").GetComponent<Camera>().backgroundColor = new Color(95f / 255f, 205f / 255f, 228f / 255f);
    animator.SetTrigger("Fade In");

    GameObject machine = getTagged("Engine");
		Collider2D[] endGameColliders = getTagged("EndGamePlatform").GetComponents<Collider2D>();
		foreach (Collider2D col in endGameColliders) {
			col.enabled = true;
		}
		getTagged("EndGamePlatform").transform.GetChild(0).gameObject.SetActive(true);

		getTagged("EndGameStats").GetComponent<Animator>().SetTrigger("Display");

		getTagged("CaveBackground").SetActive(false);
		getTagged("OutsideBackground").transform.GetChild(0).gameObject.SetActive(true);

		triggered = true;
	}

	private GameObject getTagged(string tag) {
		return GameObject.FindGameObjectWithTag(tag);
	}
}
