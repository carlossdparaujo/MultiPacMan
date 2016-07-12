using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	[RequireComponent(typeof(PelletDisposer))]
	public class PelletBehaviour : MonoBehaviour {

		private PelletDisposer pelletDisposer;

		void Start() {
			pelletDisposer = this.GetComponent<PelletDisposer>();
		}
		
		public void Dispose() {
			pelletDisposer.Dispose();
		}
	}
}
