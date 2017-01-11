using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	enum NetworkingOptions {
		Default,
		Interpolation,
		InterpolationAndExtrapolation
	};

	public class NetworkedMovementController : MovementController {

		[SerializeField]
		private NetworkingOptions option = NetworkingOptions.Default;

		private float t = 0.0f;
		private float timeWaited = 0.0f;
		private Vector2 newPosition = Vector2.zero;

		private bool started = false;

		public override void OnStart() {
			newPosition = this.transform.root.position;
			started = true;
		}

		public void UpdatePosition(Vector2 position, Vector2 velocity) {
			switch (option) {
			case NetworkingOptions.Interpolation:
				MoveWithInterpolation(position, velocity);
				break;
			case NetworkingOptions.InterpolationAndExtrapolation:
				MoveWithExtrapolation(position, velocity);
				break;
			default:
				Move(position);
				break;
			}
		}

		private void MoveWithInterpolation(Vector2 position, Vector2 velocity) {
			timeWaited = t;
			newPosition = position;
		}

		private void MoveWithExtrapolation(Vector2 position, Vector2 velocity) {
			timeWaited = t;
			newPosition = position + timeWaited*velocity;
		}

		private void Move(Vector2 position) {
			rb.MovePosition(position);
		}

		void Update() {
			if (!started || option == NetworkingOptions.Default) {
				t = 0.0f;
				timeWaited = 0.0f;
				newPosition = Vector2.zero;
				return;
			}

			t += Time.deltaTime;
			Vector2.Lerp(rb.position, newPosition, t/timeWaited);
		}
	}
}