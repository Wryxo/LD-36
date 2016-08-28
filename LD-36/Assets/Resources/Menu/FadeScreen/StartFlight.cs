using UnityEngine;

public class StartFlight : StateMachineBehaviour {
	public float FlightDelay;
	private bool triggered = false;

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (!triggered && !animator.IsInTransition(0))
			GameObject.FindGameObjectWithTag("Engine").GetComponent<MachineController>().Invoke("Fly", FlightDelay);
	}
}
