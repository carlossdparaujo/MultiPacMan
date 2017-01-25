using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PhotonView))]
	public abstract class PhotonPlayer : IPlayer {

		public override string PlayerName {
			get {
				return "Player " + GetPhotonView().viewID;
			}
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			GetPhotonView().owner.TagObject = this;
		}

		protected PhotonView GetPhotonView() {
			return this.GetComponent<PhotonView>();
		}
	}
}

