using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LevelCreator))]
public class GameController : Photon.PunBehaviour {

	[SerializeField]
	private bool simulateLag = false;
	[SerializeField]
	private int simulatedLagInMs = 100;

	private LevelCreator levelCreator;

	void Start() {
		levelCreator = this.gameObject.GetComponent<LevelCreator>();
		levelCreator.playerDelegate += CreatePlayer;
		levelCreator.pelletDelegate += CreatePellet;

		PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

	public void CreatePlayer(Vector2 position) {
		PhotonNetwork.Instantiate("Player", position, Quaternion.identity, 0);
	}

	public void CreatePellet(Vector2 position, int score) {
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.InstantiateSceneObject("Pellet", new Vector3(position.x, position.y, 0.0f), Quaternion.identity, 0, new object[] { score } );
		}
	}

	public override void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom("Game", null, null);
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom();

		levelCreator.Create();
	}

	void Update() {
		PhotonNetwork.networkingPeer.IsSimulationEnabled = simulateLag;
		PhotonNetwork.networkingPeer.NetworkSimulationSettings.IncomingLag = simulatedLagInMs;
		PhotonNetwork.networkingPeer.NetworkSimulationSettings.OutgoingLag = simulatedLagInMs;
	}
}
