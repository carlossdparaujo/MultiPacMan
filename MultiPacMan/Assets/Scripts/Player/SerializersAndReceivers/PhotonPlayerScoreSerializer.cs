using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public class PhotonPlayerScoreSerializer : MonoBehaviour {

		public static int UPDATE_SCORE_EVENT_CODE = 0;

		public void UpdateScore(int score) {
			RaiseEventOptions options = new RaiseEventOptions();
			options.CachingOption = EventCaching.DoNotCache;
			options.Receivers = ReceiverGroup.Others;

			PhotonNetwork.RaiseEvent((byte) UPDATE_SCORE_EVENT_CODE, CompressScore (score), true, options);
		}

		// Será que não é melhor fazer T ao invés de object?
		protected virtual object CompressScore(int data) {
			return data;
		}
	}
}

