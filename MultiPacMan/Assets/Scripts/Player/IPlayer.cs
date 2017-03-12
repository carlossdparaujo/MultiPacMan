using System;
using UnityEngine;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerSpriteDirectionChanger))]
	public abstract class IPlayer : MonoBehaviour {

		public delegate void PlayerScoreUpdated(string playerName, int playerScore);
		public static PlayerScoreUpdated scoreDelegate;

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		public abstract string PlayerName {
			get;
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
	}
}

