using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class LocalPelletDisposer : PelletDisposer {

		public override void Dispose() {
			DestroyImmediate(this.gameObject);
		}
	}
}