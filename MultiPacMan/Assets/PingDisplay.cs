using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PingDisplay : MonoBehaviour {
	[SerializeField]
	private Text pingText;

	void Update() {
		pingText.text = "Ping: " + PhotonNetwork.networkingPeer.RoundTripTime;
	}
}
