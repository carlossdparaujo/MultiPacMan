using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public abstract class IPlayer : Photon.MonoBehaviour {

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		private string playerName = "";
		public string PlayerName {
			get {
				return playerName;
			}
		}

		void Start() {
			playerName = "Player " + this.photonView.viewID;
		}

		public abstract void Setup();

		public void UpdateScore(int value) {
			score = value;
		}

		public void AddToScore(int value) {
			score += value;
		}

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			this.photonView.owner.TagObject = this;
		}
	}
}

