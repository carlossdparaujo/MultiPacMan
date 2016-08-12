using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public abstract class IPlayer : Photon.MonoBehaviour {

		private int score = 0;

		public abstract void Setup();

		public string GetName() {
			return "Player " + this.photonView.viewID;  
		}

		public void UpdateScore(int value) {
			score = value;
		}

		public void AddToScore(int value) {
			score += value;
		}

		public int GetScore() {
			return score;
		}

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}

		void OnPhotonInstantiate(PhotonMessageInfo info) {
			this.photonView.owner.TagObject = this;
		}
	}
}

