using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public abstract class PlayerSetup : MonoBehaviour {

		[SerializeField]
		private Behaviour[] myScripts;
		[SerializeField]
		private Behaviour[] notMineScripts;

		void Awake() {
			if (IsMine()) {
				RemoveScripts(notMineScripts);
			} else {
				RemoveScripts(myScripts);
			}
		}

		private void RemoveScripts(Behaviour[] scripts) {
			foreach (Behaviour script in scripts) {
				Destroy(script);
			}
		}

		protected abstract bool IsMine();
	}
}
