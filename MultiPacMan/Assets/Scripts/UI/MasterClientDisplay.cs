using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI
{
	public class MasterClientDisplay : MonoBehaviour {
		[SerializeField]
		private Text masterText;

		void Update() {
			masterText.text = "Master: " + PhotonNetwork.isMasterClient.ToString();
		}
	}
}
