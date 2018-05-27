using System;
using System.Collections.Generic;
using UnityEngine;
using MultiPacMan.Player;

namespace MultiPacMan.Game
{
	public class PlayerCreator {

		private Dictionary<string, Color> allSchemes = new Dictionary<string, Color> {
			{ "Red Falcon", Color.red },
			{ "Pink Crusader", Color.magenta },
			{ "Spacer Gray", Color.gray },
			{ "Green Bullet", Color.green },
			{ "Blue Skies", Color.blue },
			{ "Black Torpedo", Color.black },
			{ "Sun Ship", Color.yellow }
		};
		private Dictionary<string, Color> remainingSchemes;

		public PlayerCreator() {
			remainingSchemes = new Dictionary<string, Color>(allSchemes);
		}

		public PlayerCreator(List<IPlayer> players): this() {
			foreach (IPlayer player in players) {
				if (player == null) {
					continue;
				}

				remainingSchemes.Remove(player.PlayerName);
			}
		}

		public void AllowPlayerCreation(int newPlayerId, IList<Vector2> playersPositions) {
			string name = SelectRandomScheme(remainingSchemes);
			Color color = remainingSchemes[name];
			remainingSchemes.Remove(name);

			Vector2 position = SelectRandomPosition(playersPositions);

			PlayerCreationRequest request = new PlayerCreationRequest(newPlayerId, name, color, position);
			SendCreationMessage(request);
		}

		private string SelectRandomScheme(Dictionary<string, Color> schemes) {
			IList<string> schemeList = new List<string>(schemes.Keys);
			int randomIndex = UnityEngine.Random.Range(0, schemeList.Count);

			return schemeList[randomIndex];
		}

		private Vector2 SelectRandomPosition(IList<Vector2> playersPositions) {
			int randomIndex = UnityEngine.Random.Range(0, playersPositions.Count);
			return playersPositions[randomIndex];
		}
			
		private void SendCreationMessage(PlayerCreationRequest request) {
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

