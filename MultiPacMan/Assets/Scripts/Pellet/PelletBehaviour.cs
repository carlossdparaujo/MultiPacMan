using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class PelletBehaviour : Photon.MonoBehaviour {

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			this.score = (int) this.photonView.instantiationData[0];
		}
	}
}
