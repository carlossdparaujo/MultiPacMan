using UnityEngine;
using System.Collections;
using MultiPacMan.Player.Inputs;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MovementController : MonoBehaviour {

		protected Rigidbody2D rb;
		protected Vector2 currentVelocity = Vector2.zero;

		void Start() {
			rb = GetComponent<Rigidbody2D>();
			OnStart();
		}

		public virtual void OnStart() {
		}

		public Vector2 GetPosition() {
			if (rb == null) {
				return Vector2.zero;
			}

			return rb.position;
		}

		public Vector2 GetVelocity() {
			return currentVelocity;
		}
	}
}
