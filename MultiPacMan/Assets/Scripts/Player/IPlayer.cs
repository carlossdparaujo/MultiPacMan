using System;
using UnityEngine;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerSpriteDirectionChanger))]
	public abstract class IPlayer : MonoBehaviour, IComparable<IPlayer> {

		public delegate void PlayerScoreUpdated(string playerName, int playerScore);
		public static PlayerScoreUpdated scoreDelegate;

		public delegate void PlayerCreated(string playerName, Color playerColor);
		public static PlayerCreated playerCreatedDelegate;

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		public abstract string PlayerName {
			get;
		}

		protected Color color;
		public Color Color {
			get {
				return color;
			}
		}

		public abstract TurboController TurboController {
			get;
		}

		public PlayerSpriteDirectionChanger SpriteDirectionChanger {
			get {
				return this.GetComponent<PlayerSpriteDirectionChanger>();
			}
		}

		public void UpdateScore(int value) {
			score = value;
			SendScoreUpdatedEvent();
		}

		public void AddToScore(int value) {
			score += value;
			SendScoreUpdatedEvent();
		}

		private void SendScoreUpdatedEvent() {
			if (scoreDelegate != null) {
				scoreDelegate(PlayerName, score);
			}
		}

		public float GetTurboFuelPercentage() {
			return TurboController.GetTurboFuelPercentage();
		}

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}

		public int CompareTo(IPlayer player) {
			if (player.PlayerName == null) {
				return 1;
			}

			return player.PlayerName.CompareTo(this.name);
		}
	}
}
