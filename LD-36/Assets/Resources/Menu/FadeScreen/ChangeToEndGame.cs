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
    animator.SetTrigger("Fade In");

    GameObject machine = getTagged("Engine");
		Collider2D[] endGameColliders = getTagged("EndGamePlatform").GetComponents<Collider2D>();
		foreach (Collider2D col in endGameColliders) {
			col.enabled = true;
		}
		getTagged("EndGamePlatform").transform.GetChild(0).gameObject.SetActive(true);

		getTagged("EndGameStats").GetComponent<Animator>().SetTrigger("Display");

		triggered = true;
	}

	private GameObject getTagged(string tag) {
		return GameObject.FindGameObjectWithTag(tag);
	}
}
