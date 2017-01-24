using UnityEngine;
using System.Collections;
using MultiPacMan.Player.Collision;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public abstract class PelletEater : MonoBehaviour {

		public delegate void DidEatPellet(PelletBehaviour pellet);
		public DidEatPellet eatPelletDelegate;

		public void EatPellet(PelletBehaviour pellet) {
			if (eatPelletDelegate == null || pellet.Eaten) {
				return;
			}

			pellet.Eaten = true;

			eatPelletDelegate(pellet);
		}
	}
}
