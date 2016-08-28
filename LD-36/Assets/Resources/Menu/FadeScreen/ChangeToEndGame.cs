using PC2D;
using UnityEngine;

public class ChangeToEndGame : StateMachineBehaviour {
	private bool triggered = false;

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (triggered || animator.IsInTransition(0)) return;

		getTagged("Player").SetActive(false);
		getTagged("Level").SetActive(false);

    // TODO: destroy all enemies and dropped items
    // TODO: set new camera center as machine
    getTagged("MainCamera").GetComponent<CameraFollow>().Target = getTagged("Engine").transform;
    getTagged("MainCamera").GetComponent<CameraFollow>().FollowingPlayer = false;
    animator.SetTrigger("Fade In");

    GameObject machine = getTagged("Engine");
		//machine.transform.position = new Vector3(machine.transform.position.x, -7f, -2f);
		getTagged("EndGamePlatform").GetComponent<Collider2D>().enabled = true;
		getTagged("EndGamePlatform").transform.GetChild(0).gameObject.SetActive(true);

		triggered = true;
	}

	private GameObject getTagged(string tag) {
		return GameObject.FindGameObjectWithTag(tag);
	}
}
