using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Player;
using MultiPacMan.Pellet;

[RequireComponent(typeof(LevelCreator))]
public class GameController : Photon.PunBehaviour {
	
	[SerializeField]
	private bool simulateLag = false;
	[SerializeField]
	private int simulatedLagInMs = 100;
	[SerializeField]
	private GameObject pelletPrefab;

	private LevelCreator levelCreator;
	private static Dictionary<string, GameObject> pellets = new Dictionary<string, GameObject>();

	public delegate void OnGameEnded(List<IPlayer> players);
	public static OnGameEnded gameEndedDelegate;

	public static List<IPlayer> GetPlayers() {
		List<IPlayer> playerList = new List<IPlayer>();

		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			playerList.Add((IPlayer) player.TagObject);
		}

		return playerList;
	}

	public static IPlayer GetPlayer(int id) {
		List<IPlayer> playerList = new List<IPlayer>();

		foreach (PhotonPlayer player in PhotonNetwork.playerList) {
			if (player.ID == id) {
				return (IPlayer) player.TagObject;
			}
		}

		return null;
	}

	public static PhotonPlayerBehaviour GetMyPlayer() {
		return (PhotonPlayerBehaviour) PhotonNetwork.player.TagObject;
	}

	public static GameObject PopPellet(int pelletId) {
		string key = pelletId.ToString();

		if (pellets.ContainsKey(key)) {
			GameObject pellet = pellets[key];
			pellets.Remove(key);
			return pellet;
		}

		return null;
	}

	void Start() {
		levelCreator = this.gameObject.GetComponent<LevelCreator>();
		levelCreator.playerDelegate += CreatePlayer;
		levelCreator.pelletDelegate += CreatePellet;

		PhotonNetwork.ConnectUsingSettings("0.0.0");
		PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
	}

	public void CreatePlayer(Vector2 position) {
		PhotonNetwork.Instantiate("Player", new Vector3(position.x, position.y, -1.0f), Quaternion.identity, 0);
	}

	public void CreatePellet(Vector2 position, int score, Point positionOnMap) {
		Vector3 pos = new Vector3(position.x, position.y, 0.0f);

		GameObject pellet = GameObject.Instantiate(pelletPrefab, pos, Quaternion.identity) as GameObject;
		pellet.GetComponent<PelletBehaviour>().Setup(score, positionOnMap.x, positionOnMap.y);

		pellets.Add(positionOnMap.GetHashCode().ToString(), pellet);
	}

	public override void OnJoinedLobby() {
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.maxPlayers = 4;

		PhotonNetwork.JoinOrCreateRoom("Game", roomOptions, null);
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom();

		levelCreator.Create();
	}

	public void PhotonNetwork_OnEventCall(byte eventCode, object content, int senderId) {
		if ((int) eventCode == PhotonPelletEater.REMOVE_PELLET_EVENT_CODE) {
			object[] data = (object[]) content;

			int pelletScore = (int) data[0];
			int pelletId = (int) data[1];
			int playerId = (int) data[2];

			GameObject pellet = PopPellet(pelletId);

			if (pellet != null) {
				GameObject.DestroyImmediate(pellet);
			}

			IPlayer player = GetPlayer(playerId);

			if (player != null) {
				player.AddToScore(pelletScore);
			}
		}
	}

	void Update() {
		PhotonNetwork.networkingPeer.IsSimulationEnabled = simulateLag;
		PhotonNetwork.networkingPeer.NetworkSimulationSettings.IncomingLag = simulatedLagInMs;
		PhotonNetwork.networkingPeer.NetworkSimulationSettings.OutgoingLag = simulatedLagInMs;
	}

	public override void OnLeftRoom() {
		pellets.Clear();
		PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
	}
}
