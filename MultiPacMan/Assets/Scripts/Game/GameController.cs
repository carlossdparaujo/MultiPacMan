using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Player;
using MultiPacMan.Photon.Player;
using MultiPacMan.Pellet;
using Photon;

namespace MultiPacMan.Game
{
	[RequireComponent(typeof(LevelCreator))]
	public class GameController : PunBehaviour {

		public static bool VALIDATION_ON = true;

		[SerializeField]
		private bool simulateLag = false;
		[SerializeField]
		private int simulatedLagInMs = 100;

		private LevelCreator levelCreator;
		private static Dictionary<string, PelletBehaviour> pellets = new Dictionary<string, PelletBehaviour>();
		private static List<int> pelletsNotEaten = new List<int>();
		private bool gameInitiliazed = false;
		private bool isPlaying = false;
		private PlayerCreator playerCreator = new PlayerCreator();

		public struct PlayerData {
			public readonly string name;
			public readonly int score;

			public PlayerData(string name, int score) {
				this.name = name;
				this.score = score;
			}
		}

		public delegate void OnGameStarted();
		public static OnGameStarted gameStartedDelegate;

		public delegate void OnGameEnded(List<PlayerData> players);
		public static OnGameEnded gameEndedDelegate;

		public delegate void GetPlayersStats(PlayersStats stats);
		public static GetPlayersStats playersStatsDelegate;

		public static List<IPlayer> GetPlayers() {
			List<IPlayer> playerList = new List<IPlayer>();

			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				playerList.Add((IPlayer) player.TagObject);
			}

			return playerList;
		}

		void Start() {
			if (gameStartedDelegate != null) {
				gameStartedDelegate();
			}

			levelCreator = this.gameObject.GetComponent<LevelCreator>();
			levelCreator.pelletCreated += RegisterPellet;

			PhotonNetwork.ConnectUsingSettings("0.0.0");
			PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
			PhotonNetwork.OnEventCall += playerCreator.PhotonNetwork_OnEventCall;
			isPlaying = true;
		}

		public void RegisterPellet(PelletBehaviour pellet, Point positionOnMap) {
			int id = positionOnMap.GetHashCode();
			pellets.Add(id.ToString(), pellet);
			pelletsNotEaten.Add(id);
		}

		public override void OnJoinedLobby() {
			if (isPlaying == false) {
				return;
			}

			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = 4;

			PhotonNetwork.JoinOrCreateRoom("Game", roomOptions, null);
		}

		public override void OnJoinedRoom() {
			base.OnJoinedRoom();

			if (isPlaying == false) {
				PhotonNetwork.LeaveRoom();
				return;
			}

			RequestPlayerCreation();
			levelCreator.Create();

			gameInitiliazed = true;
		}
			
		private void RequestPlayerCreation() {
			RaiseEventOptions options = new RaiseEventOptions();
			options.CachingOption = EventCaching.AddToRoomCacheGlobal;
			options.Receivers = ReceiverGroup.All;

			PhotonNetwork.RaiseEvent((byte) Events.NEW_PLAYER_ENTERED, null, true, options);
		}

		public override void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
			base.OnMasterClientSwitched(newMasterClient);

			playerCreator = new PlayerCreator(GetPlayers());
		}

		public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
			if ((int)eventCode == (int) Events.EAT_PELLET_EVENT_CODE) {
				if (!PhotonNetwork.isMasterClient) {
					return;
				}

				object[] data = (object[]) content;
				int pelletId = (int) data[0];

				if (VALIDATION_ON) {
					if (!pelletsNotEaten.Remove(pelletId)) {
						return;
					}
				}

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.AddToRoomCacheGlobal;
				options.Receivers = ReceiverGroup.All;

				PhotonNetwork.RaiseEvent((byte) Events.REMOVE_PELLET_EVENT_CODE,
					new object[2] { pelletId, senderId },
					true, options
				);
			} else if ((int) eventCode == (int) Events.REMOVE_PELLET_EVENT_CODE) {
				object[] data = (object[]) content;

				int pelletId = (int) data[0];
				int playerId = (int) data[1];

				PelletBehaviour pellet = GetPellet(pelletId);
				if (pellet != null) {
					pellet.DestroyAfterAnimation();
				}

				IPlayer player = GetPlayer(playerId);
				if (player != null) {
					player.AddToScore(pellet.Score);
				}

				pelletsNotEaten.Remove(pelletId);
			} else if ((int) eventCode == (int) Events.NEW_PLAYER_ENTERED) {
				if (PhotonNetwork.isMasterClient) {
					playerCreator.AllowPlayerCreation(senderId, levelCreator.GetPlayersPositions());
				} 

				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.AddToRoomCacheGlobal;
				options.Receivers = ReceiverGroup.All;

				IPlayer myPlayer = GetMyPlayer();

				if (myPlayer == null) {
					return;
				}

				PhotonNetwork.RaiseEvent ((byte) Events.SET_PLAYER_SCORE,
					new object[2] { myPlayer.PlayerName, myPlayer.Score },
					true, options
				);
			} else if ((int) eventCode == (int) Events.SET_PLAYER_SCORE) {
				object[] data = (object[]) content;

				string playerName = (string) data[0];
				int playerScore = (int) data[1];

				IPlayer player = GetPlayer(playerName);

				if (player != null) {
					player.Score = playerScore;
				}
			} else if ((int) eventCode == (int) Events.END_GAME_EVENT_CODE) {
				if (gameEndedDelegate != null) {
					gameEndedDelegate(getPlayersData());
				}

				isPlaying = false;
				PhotonNetwork.LeaveRoom();
			}
		}

		private static PelletBehaviour GetPellet(int pelletId) {
			string key = pelletId.ToString();

			if (pellets.ContainsKey(key)) {
				PelletBehaviour pellet = pellets[key];
				return pellet;
			}

			return null;
		}

		private static IPlayer GetPlayer(string playerName) {
			foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList) {
				IPlayer player = (IPlayer) photonPlayer.TagObject;

				if (player == null || player.PlayerName == null) {
					continue;
				} else if (player.PlayerName.Equals(playerName)) {
					return player;
				}
			}

			return null;
		}

		private static IPlayer GetPlayer(int id) {
			foreach (PhotonPlayer player in PhotonNetwork.playerList) {
				if (player.ID == id) {
					return (IPlayer) player.TagObject;
				}
			}

			return null;
		}

		void Update() {
			PhotonNetwork.networkingPeer.IsSimulationEnabled = simulateLag;
			PhotonNetwork.networkingPeer.NetworkSimulationSettings.IncomingLag = simulatedLagInMs;
			PhotonNetwork.networkingPeer.NetworkSimulationSettings.OutgoingLag = simulatedLagInMs;

			if (PhotonNetwork.isMasterClient && pelletsNotEaten.Count == 0 && gameInitiliazed && isPlaying) {
				RaiseEventOptions options = new RaiseEventOptions();
				options.CachingOption = EventCaching.AddToRoomCacheGlobal;
				options.Receivers = ReceiverGroup.All;

				PhotonNetwork.RaiseEvent ((byte) Events.END_GAME_EVENT_CODE,
					null, true, options
				);
			}

			try {
				playersStatsDelegate(playersStats());
			} catch (InvalidOperationException) {
				Debug.Log("Waiting for my player to connect.");
			}
		}

		private PlayersStats playersStats() {
			IList<IPlayer> players = GetPlayers();
			IList<PlayerStats> allStats = new List<PlayerStats>();

			foreach (IPlayer player in players) {
				if (player == null) {
					Debug.Log(player + " is  anull player");
					continue;
				}

				Debug.Log(player.PlayerName + " is a player");

				PlayerStats playerStats = player.GetStats();
				allStats.Add(playerStats);
			}

			IPlayer myPlayer = (IPlayer) PhotonNetwork.player.TagObject;
			if (myPlayer == null) {
				throw new InvalidOperationException("My player still not connected.");
			}

			return new PlayersStats(allStats, myPlayer.PlayerName);
		}

		private List<PlayerData> getPlayersData() {
			List<PlayerData> data = new List<PlayerData>();

			foreach (IPlayer player in GetPlayers()) {
				data.Add(new PlayerData(player.PlayerName, player.Score));
			}

			return data;
		}

		public override void OnLeftRoom() {
			pellets.Clear();
			PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
		}

		private IPlayer GetMyPlayer() {
			return (IPlayer) PhotonNetwork.player.TagObject;
		}
	}
}
