using UnityEngine;
using System.Collections;

public class GameController : Photon.PunBehaviour {

	void Start() {
		PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

	public override void OnJoinedLobby() {
		PhotonNetwork.JoinOrCreateRoom("Teste", null, null);
	}

	public override void OnJoinedRoom() {
		base.OnJoinedRoom();
		CreatePellet();
		CreatePlayer();
	}

	private void CreatePellet() {
		if (PhotonNetwork.isMasterClient) {
			PhotonNetwork.InstantiateSceneObject("Pellet", new Vector3(1.0f, 1.0f, 0.0f), Quaternion.identity, 0, null);
		}
	}

	private void CreatePlayer() {
		PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
	}
}
