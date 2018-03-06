using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class PelletIdleAnimation : StateMachineBehaviour {

		public delegate void EnteredState();
		public EnteredState enteredState;

		override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			animator.SetBool("eaten", false);
			enteredState();
		}
	}
}
