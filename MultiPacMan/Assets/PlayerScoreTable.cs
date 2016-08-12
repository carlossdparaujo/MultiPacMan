using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MultiPacMan.Player;

[RequireComponent(typeof(Text))]
public class PlayerScoreTable : MonoBehaviour {

	private Text label;

	void Start() {
		label = this.gameObject.GetComponent<Text>();
	}

	void Update () {
		string scores = "";

		foreach (IPlayer player in GameController.GetPlayers()) {
			if (player == null) { 
				continue;
			}

			scores += player.GetName() + " : " + player.GetScore() + " | ";
		}

		label.text = scores;
	}
}
