using System;
using UnityEngine;
using MultiPacMan.Player;

namespace MultiPacMan.Photon.Player
{
	[RequireComponent(typeof(PhotonView))]
	public class PhotonPlayerSetup : PlayerSetup {
		
		protected override bool IsMine() {
			return this.GetComponent<PhotonView>().isMine;
		}

		protected override IPlayer SetLocalPlayer() {
			return this.gameObject.AddComponent<PhotonLocalPlayer>();
		}

		protected override IPlayer SetNetworkedPlayer() {
			return this.gameObject.AddComponent<PhotonNetworkedPlayer>();
		}
	}
}

