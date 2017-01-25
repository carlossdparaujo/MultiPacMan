using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public abstract class IPlayer : MonoBehaviour {

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
		}

		public void AddToScore(int value) {
			score += value;
		}

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}
	}
}

