using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPelletEater : PelletEater {

		protected override void EatPellet(PelletBehaviour pellet) {
			if (PhotonNetwork.isMasterClient) {
				eatPelletDelegate(pellet.Score);
				PhotonNetwork.Destroy(pellet.gameObject);
			}
		}
	}
}

