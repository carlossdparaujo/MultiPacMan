using System;
using UnityEngine;

namespace MultiPacMan.Player
{
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

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}
	}
}

