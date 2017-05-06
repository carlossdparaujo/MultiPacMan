using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PingDisplay : MonoBehaviour {
	public Text pingText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		pingText.text = "Ping: " + PhotonNetwork.networkingPeer.RoundTripTime;
	}
}
