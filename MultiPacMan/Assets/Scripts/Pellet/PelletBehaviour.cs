using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class PelletBehaviour : Photon.MonoBehaviour {

		private bool eaten = false;
		public bool Eaten {
			get {
				return eaten;
			}
			set {
				eaten = value;
			}
		}

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		private Point point = new Point();
		public Point Point {
			get {
				return point;
			}
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			this.score = (int) this.photonView.instantiationData[0];
			int x = (int) this.photonView.instantiationData[1];
			int y = (int) this.photonView.instantiationData[2];

			this.point = new Point(x, y);
		}
	}
}
