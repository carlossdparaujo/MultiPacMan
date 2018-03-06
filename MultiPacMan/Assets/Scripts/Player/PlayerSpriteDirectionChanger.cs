using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public class PlayerSpriteDirectionChanger : MonoBehaviour {

		public delegate Vector2 PlayerDirectionUpdated();
		public PlayerDirectionUpdated directionDelegate;

		[SerializeField]
		private GameObject sprite;

		void Update() {
			if (directionDelegate == null) {
				return;
			}

			Vector2 vectorDirection = directionDelegate();
			sprite.transform.up = vectorDirection;
		}
	}
}
