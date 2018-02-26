using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterOptions : MonoBehaviour {
	
	[SerializeField]
	private Toggle disableValidationToggle;

	void Update() {
		disableValidationToggle.gameObject.SetActive(PhotonNetwork.isMasterClient);
	}

	public void TurnValidationOff(bool value) {
		GameController.VALIDATION_ON = !value;
	}
}
