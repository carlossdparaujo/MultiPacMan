using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PhotonView))]
	public class PhotonPlayerSetup : PlayerSetup {
		
		protected override bool IsMine() {
			return this.GetComponent<PhotonView>().isMine;
		}

		protected override IPlayer AddNetworkedPlayer() {
			return this.gameObject.AddComponent<PhotonNetworkedPlayerBehaviour>();
		}
	}
}

