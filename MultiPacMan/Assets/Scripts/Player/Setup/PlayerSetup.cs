using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public abstract class PlayerSetup : MonoBehaviour {

		void Awake() {
			IPlayer player;

			if (IsMine()) {
				player = this.gameObject.AddComponent<PhotonPlayerBehaviour>();
			} else {
				player = AddNetworkedPlayer();
			}

			player.Setup();
		}

		protected abstract bool IsMine();

		protected abstract IPlayer AddNetworkedPlayer();
	}
}
