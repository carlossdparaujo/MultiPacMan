using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPelletEater : PelletEater {

		public static int EAT_PELLET_EVENT_CODE = 1;

		void Start() {
			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if ((int) eventCode == EAT_PELLET_EVENT_CODE) {
				object[] data = (object[]) content;

				int pelletScore = (int) data[0];
				int pelletId = (int) data[1];

				if (PhotonNetwork.isMasterClient) {
					GameObject pellet = GameController.PopPellet(pelletId);

					if (pellet != null) {
						PhotonNetwork.Destroy(pellet);
					}
				}

				if (PhotonNetwork.player.ID == senderId) {
					eatPelletDelegate(pelletScore);
				}
			}
		}

		protected override void EatPellet(PelletBehaviour pellet) {
			if (pellet.Eaten) {
				return;
			}

			pellet.Eaten = true;

			int pelletId = pellet.Point.GetHashCode();

			RaiseEventOptions options = new RaiseEventOptions();
			options.CachingOption = EventCaching.DoNotCache;
			options.Receivers = ReceiverGroup.All;

			PhotonNetwork.RaiseEvent((byte) EAT_PELLET_EVENT_CODE, 
				new object[2] { pellet.Score, pelletId }, 
				true, options
			);
		}
	}
}

