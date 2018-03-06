using System;
using UnityEngine;
using MultiPacMan.Player;

namespace MultiPacMan.Photon.Player
{
	[RequireComponent(typeof(PhotonView))]
	public abstract class PhotonPlayer : IPlayer {

		public override string PlayerName {
			get {
				PhotonView photonView = GetPhotonView();

				if (photonView == null) {
					return "";
				}

				return "Player " + photonView.viewID;
			}
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			PhotonView photonView = GetPhotonView();

			float red = (float) photonView.instantiationData[0];
			float green = (float) photonView.instantiationData[1];
			float blue = (float) photonView.instantiationData[2];
			this.color = new Color(red, green, blue); 
				
			photonView.owner.TagObject = this;

			playerCreatedDelegate(PlayerName, this.color);
		}

		protected PhotonView GetPhotonView() {
			return this.GetComponent<PhotonView>();
		}
	}
}

