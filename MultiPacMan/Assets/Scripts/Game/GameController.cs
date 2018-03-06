using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Player;
using MultiPacMan.Pellet;

[RequireComponent(typeof(LevelCreator))]
public class GameController : Photon.PunBehaviour {

	public static int EAT_PELLET_EVENT_CODE = 1;
	public static int REMOVE_PELLET_EVENT_CODE = 2;
	public static int END_GAME_EVENT_CODE = 3;

	public static bool VALIDATION_ON = true;

	[SerializeField]
	private bool simulateLag = false;
	[SerializeField]
	private int simulatedLagInMs = 100;
	[SerializeField]
	private GameObject pelletPrefab;

	private LevelCreator levelCreator;
	private static Dictionary<string, PelletBehaviour> pellets = new Dictionary<string, PelletBehaviour>();
	private static List<int> pelletsNotEaten = new List<int>();
	private bool gameInitiliazed = false;
	private bool isPlaying = false;

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

	public static PhotonLocalPlayer GetMyPlayer() {
		return (PhotonLocalPlayer) PhotonNetwork.player.TagObject;
	}

	void Start() {
		if (gameStartedDelegate != null) {
			gameStartedDelegate();
		}

		levelCreator = this.gameObject.GetComponent<LevelCreator>();
		levelCreator.playerDelegate += CreatePlayer;
		levelCreator.pelletDelegate += CreatePellet;

		PhotonNetwork.ConnectUsingSettings("0.0.0");
		PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
		isPlaying = true;
	}

	public void CreatePlayer(Vector2 position) {
		PhotonNetwork.Instantiate("Player", new Vector3(position.x, position.y, -1.0f), Quaternion.identity, 0);
	}

	public void CreatePellet(Vector2 position, int score, Point positionOnMap) {
		Vector3 pos = new Vector3(position.x, position.y, 0.0f);

		GameObject pelletGameObject = GameObject.Instantiate(pelletPrefab, pos, Quaternion.identity) as GameObject;
		PelletBehaviour pellet = pelletGameObject.GetComponent<PelletBehaviour>();
		pellet.Setup(score, positionOnMap.x, positionOnMap.y);

		int id = positionOnMap.GetHashCode();
		pellets.Add(id.ToString(), pellet);
		pelletsNotEaten.Add(id);

		gameInitiliazed = true;
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

		levelCreator.Create();
	}

	public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
		if ((int) eventCode == EAT_PELLET_EVENT_CODE) {
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

			PhotonNetwork.RaiseEvent ((byte) REMOVE_PELLET_EVENT_CODE, 
				new object[2] { pelletId, senderId }, 
				true, options
			);
		} else if ((int) eventCode == REMOVE_PELLET_EVENT_CODE) {
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
		} else if ((int) eventCode == END_GAME_EVENT_CODE) {
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

			PhotonNetwork.RaiseEvent ((byte) END_GAME_EVENT_CODE, 
				null, true, options
			);
		}
	}

	private List<PlayerData> getPlayersData() {
		List<PlayerData> data = new List<PlayerData>();

		foreach (IPlayer player in GetPlayers()) {
			data.Add(new PlayerData(player.PlayerName, player.Score));
		}

		return data;
	}

	private static List<IPlayer> GetPlayers() {
		List<IPlayer> playerList = new List<IPlayer>();

		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			playerList.Add((IPlayer) player.TagObject);
		}

		return playerList;
	}

	public override void OnLeftRoom() {
		pellets.Clear();
		PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
	}
}
