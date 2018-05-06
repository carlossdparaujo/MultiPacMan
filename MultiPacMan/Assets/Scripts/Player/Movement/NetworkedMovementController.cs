using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player.Movement
{
	public enum NetworkingOptions {
		Default,
		Interpolation,
		InterpolationAndExtrapolation
	};

	public class NetworkedMovementController : MovementController {

		public static NetworkingOptions serializationOption = NetworkingOptions.Default;

		private float t = 0.0f;
		private float timeWaited = 0.0f;
		private Vector2 newPosition = Vector2.zero;
		private Vector2 oldPosition = Vector2.zero;

		private bool started = false;

		public override void OnStart() {
			newPosition = this.transform.root.position;
		}

		public Vector2 GetDirection() {
			return this.currentVelocity.normalized;
		}

		public void UpdatePosition(Vector2 position, Vector2 velocity) {
			if (!started) {
				started = true;
				rb.position = position;
				return;
			}


			this.timeWaited = t;
			this.t = 0.0f;
			this.oldPosition = rb.position;
			this.currentVelocity = velocity;

			switch (serializationOption) {
			case NetworkingOptions.Interpolation:
				newPosition = position;
				break;
			case NetworkingOptions.InterpolationAndExtrapolation:
				newPosition = position + timeWaited*velocity;
				break;
			default:
				this.newPosition = this.oldPosition;
				Move(position);
				break;
			}
		}

		void Update() {
			if (serializationOption == NetworkingOptions.Default) {
				return;
			}

			t += Time.deltaTime;

			if (!oldPosition.Equals(newPosition)) {
				Move(Vector2.Lerp(oldPosition, newPosition, t/timeWaited));
			}
		}

		private void Move(Vector2 position) {
			rb.MovePosition(position);
		}
	}
}
