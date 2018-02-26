using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterCliendDisplay : MonoBehaviour {
	[SerializeField]
	private Text masterText;

	void Update() {
		masterText.text = "Master: " + PhotonNetwork.isMasterClient.ToString();
	}
}
