using System;
using UnityEngine;
using MultiPacMan.Pellet;
using MultiPacMan.Player;
using Photon;
using System.Collections.Generic;

namespace MultiPacMan.Game
{
	[RequireComponent(typeof(LevelCreator))]
	public class LevelController : PunBehaviour {

		public static bool VALIDATION_ON = true;

		public delegate void GameEnded();
		public static GameEnded gameEndedDelegate;

		private LevelCreator levelCreator;
		private static Dictionary<string, PelletBehaviour> pellets = new Dictionary<string, PelletBehaviour>();
		private static List<int> pelletsNotEaten = new List<int>();

		void Start() {
			levelCreator = this.gameObject.GetComponent<LevelCreator>();
			levelCreator.pelletCreated += RegisterPellet;

			GameController.gameStartedDelegate += levelCreator.Create;
			GameController.gameEndedDelegate += (PlayersStats stats) => pellets.Clear();

			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
		}

		public void RegisterPellet(PelletBehaviour pellet, Point positionOnMap) {
			int id = positionOnMap.GetHashCode();
			pellets.Add(id.ToString(), pellet);
			pelletsNotEaten.Add(id);
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if ((int) eventCode == (int) Events.EAT_PELLET_EVENT_CODE) {
				if (!PhotonNetwork.isMasterClient) {
					return;
				}

				object[] data = (object[]) content;
				int pelletId = (int)data [0];

				if (VALIDATION_ON) {
					if (!pelletsNotEaten.Remove(pelletId)) {
						return;
					}
				}

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.AddToRoomCacheGlobal;
				options.Receivers = ReceiverGroup.All;

				PhotonNetwork.RaiseEvent ((byte) Events.REMOVE_PELLET_EVENT_CODE,
					new object[2] { pelletId, senderId },
					true, options
				);
			} else if ((int) eventCode == (int) Events.REMOVE_PELLET_EVENT_CODE) {
				object[] data = (object[])content;

				int pelletId = (int)data [0];
				int playerId = (int)data [1];

				PelletBehaviour pellet = GetPellet(pelletId);
				if (pellet != null) {
					pellet.DestroyAfterAnimation();
				}

				IPlayer player = GetPlayer(playerId);
				if (player != null) {
					player.AddToScore(pellet.Score);
				}

				pelletsNotEaten.Remove(pelletId);

				if (PhotonNetwork.isMasterClient) {
					if (pelletsNotEaten.Count == 0) {
						gameEndedDelegate();
					}
				}
			}
		}

		private PelletBehaviour GetPellet(int pelletId) {
			string key = pelletId.ToString();

			if (pellets.ContainsKey(key)) {
				PelletBehaviour pellet = pellets[key];
				return pellet;
			}

			return null;
		}

		private IPlayer GetPlayer(int id) {
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				if (player.ID == id) {
					return (IPlayer) player.TagObject;
				}
			}

			return null;
		}
	}
}

