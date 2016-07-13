using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public class PlayerBehaviour : MonoBehaviour {

		[SerializeField]
		private Vector2 playerPosition;
		public Vector2 PlayerPosition {
			get {
				return playerPosition;
			}
			set {
				playerPosition = value;
			}
		}

		[SerializeField]
		private bool isTurboOn;
		public bool IsTurboOn {
			get {
				return isTurboOn;
			}
			set {
				isTurboOn = value;
			}
		}
	}
}

