using System;
using UnityEngine;
using MultiPacMan.Game;
using MultiPacMan.Game.Requests;
using MultiPacMan.Game.Services;

namespace MultiPacMan.Photon.Game.Services
{
	public class PhotonPlayerCreationService : PlayerCreationService {

		public void SendCreationMessage(PlayerCreationRequest request) {
			RaiseEventOptions options = new RaiseEventOptions();
			options.CachingOption = EventCaching.AddToRoomCacheGlobal;
			options.Receivers = ReceiverGroup.All;

			PhotonNetwork.RaiseEvent ((byte) Events.ALLOW_PLAYER_CREATION,
				request.asData(),
				true, options
			);
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if (!ReceivedAllowCreationEvent(eventCode)) {
				return;
			}

			CreatePlayerForOwner((object[]) content);
		}

		private bool ReceivedAllowCreationEvent(int eventCode) {
			return eventCode == (int) Events.ALLOW_PLAYER_CREATION;
		}

		private void CreatePlayerForOwner(object[] data) {
			PlayerCreationRequest request = new PlayerCreationRequest(data);

			if (ImOwner(request.OwnerId)) {
				CreatePlayerOnNetwork(request);
			}
		}

		private bool ImOwner(int ownerId) {
			return PhotonNetwork.player.ID == ownerId;
		}

		private void CreatePlayerOnNetwork(PlayerCreationRequest request) {
			Vector3 position = new Vector3(request.PlayerPosition.x, request.PlayerPosition.y, -1.0f);
			PhotonNetwork.Instantiate("Player", position, Quaternion.identity, 0, request.asData());
		}
	}
}

