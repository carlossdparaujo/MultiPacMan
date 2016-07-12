using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public class PlayerBehaviour : MonoBehaviour {

		private Vector2 playerPosition;
		public Vector2 PlayerPosition {
			get {
				return playerPosition;
			}
			set {
				playerPosition = value;
			}
		}
	}
}

