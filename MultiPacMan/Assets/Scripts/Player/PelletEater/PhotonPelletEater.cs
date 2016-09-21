using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Pellet;

namespace MultiPacMan.Player
{
	public class PhotonPelletEater : PelletEater {

		public static int EAT_PELLET_EVENT_CODE = 1;
		public static int REMOVE_PELLET_EVENT_CODE = 2;

		void Start() {
			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if ((int) eventCode == EAT_PELLET_EVENT_CODE) {
				object[] data = (object[]) content;

				int pelletScore = (int) data[0];
				int pelletId = (int) data[1];

				if (PhotonNetwork.isMasterClient) {
					RaiseEventOptions options = new RaiseEventOptions();
					options.CachingOption = EventCaching.AddToRoomCacheGlobal;
					options.Receivers = ReceiverGroup.All;

					PhotonNetwork.RaiseEvent((byte) REMOVE_PELLET_EVENT_CODE, 
						new object[3] { pelletScore, pelletId, senderId }, 
						true, options
					);
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

