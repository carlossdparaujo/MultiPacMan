using System;
using UnityEngine;
using MultiPacMan.Player;
using MultiPacMan.Game;
using MultiPacMan.Game.Requests;

namespace MultiPacMan.Photon.Player
{
	[RequireComponent(typeof(PhotonView))]
	public class PhotonPlayerSetup : PlayerSetup {

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			PhotonView photonView = this.gameObject.GetComponent<PhotonView>();
			PlayerCreationRequest request = new PlayerCreationRequest(photonView.instantiationData);
		
			PlayerStats stats = new PlayerStats(request.PlayerName, request.PlayerColor, 0, 0.0f);
			StartSetup(stats, isPlayerMine(request.OwnerId));

			photonView.owner.TagObject = this.GetComponent<IPlayer>();
        }

		private bool isPlayerMine(int ownerId) {
			return PhotonNetwork.player.ID == ownerId;
		}

		protected override IPlayer SetLocalPlayer() {
			return this.gameObject.AddComponent<PhotonLocalPlayer>();
		}

		protected override IPlayer SetNetworkedPlayer() {
			return this.gameObject.AddComponent<PhotonNetworkedPlayer>();
		}
	}
}

