using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class RegularPelletEater : PelletEater {
		
		protected override void EatPellet(PelletBehaviour pellet) {
			pellet.Dispose();
		}
	}
}
