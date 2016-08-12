using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public abstract class PlayerSetup : MonoBehaviour {

		[SerializeField]
		private TextMesh playerName;

		void Awake() {
			IPlayer player;

			if (IsMine()) {
				player = this.gameObject.AddComponent<PhotonPlayerBehaviour>();
				Camera.main.GetComponent<FollowPlayer>().Follow(this.gameObject);
			} else {
				player = AddNetworkedPlayer();
			}

			player.Setup();
			playerName.text = player.GetName();
		}

		protected abstract bool IsMine();

		protected abstract IPlayer AddNetworkedPlayer();
	}
}
