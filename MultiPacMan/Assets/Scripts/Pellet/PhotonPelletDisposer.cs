using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class PhotonPelletDisposer : PelletDisposer {

		public override void Dispose() {
			if (PhotonNetwork.isMasterClient) {
				PhotonNetwork.Destroy(this.gameObject);
			}
		}
	}
}
