using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPelletEater : PelletEater {

		protected override void EatPellet(PelletBehaviour pellet) {
			PhotonNetwork.Destroy(pellet.gameObject);
		}
	}
}

