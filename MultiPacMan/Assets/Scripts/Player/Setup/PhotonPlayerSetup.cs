using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PhotonView))]
	public class PhotonPlayerSetup : PlayerSetup {
		
		protected override bool IsMine() {
			return this.GetComponent<PhotonView>().isMine;
		}
	}
}

