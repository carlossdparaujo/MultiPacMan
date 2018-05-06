using System;
using UnityEngine;
using MultiPacMan.Player;

namespace MultiPacMan.Photon.Player
{
	[RequireComponent(typeof(PhotonView))]
	public abstract class PhotonBasePlayer : IPlayer {

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			PhotonView photonView = GetPhotonView();

			float red = (float) photonView.instantiationData[0];
			float green = (float) photonView.instantiationData[1];
			float blue = (float) photonView.instantiationData[2];
			this.color = new Color(red, green, blue); 

			this.playerName = (string) photonView.instantiationData[3];
				
			photonView.owner.TagObject = this;
		}

		protected PhotonView GetPhotonView() {
			return this.GetComponent<PhotonView>();
		}
	}
}

