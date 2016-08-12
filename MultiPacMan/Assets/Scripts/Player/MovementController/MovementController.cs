using UnityEngine;
using System.Collections;
using MultiPacMan.Player.Inputs;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MovementController : MonoBehaviour {

		protected Rigidbody2D rb;

		void Start() {
			rb = GetComponent<Rigidbody2D>();
		}

		public Vector2 GetPosition() {
			if (rb == null) {
				return Vector2.zero;
			}

			return rb.position;
		}
	}
}
