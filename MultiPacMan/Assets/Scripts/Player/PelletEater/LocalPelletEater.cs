using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class LocalPelletEater : PelletEater {
		
		protected override void EatPellet(GameObject pellet) {
			Destroy(pellet);
		}
	}
}
