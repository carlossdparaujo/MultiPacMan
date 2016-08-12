using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public class PhotonPlayerScoreReceiver : MonoBehaviour {

		public delegate void SetPlayerScore(int score);
		public SetPlayerScore scoreDelegate;

		void Start() {
			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if (scoreDelegate == null) {
				return;
			}

			if ((int) eventCode == PhotonPlayerScoreSerializer.UPDATE_SCORE_EVENT_CODE) {
				int score = DecompressScore(content);
				scoreDelegate(score);
			}
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual int DecompressScore(object data) {
			return (int) data;
		}

		void OnDestroy() {
			PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
		}
	}
}

