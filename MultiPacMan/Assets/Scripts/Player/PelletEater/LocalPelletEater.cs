using UnityEngine;
using System.Collections;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class LocalPelletEater : PelletEater {
		
		protected override void EatPellet(PelletBehaviour pellet) {
			DestroyImmediate(pellet.gameObject);
		}
	}
}
