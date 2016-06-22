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
		CreatePlayer();
	}

	private void CreatePlayer() {
		PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
	}
}
