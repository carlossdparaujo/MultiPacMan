using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MultiPacMan.Player;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class PlayerScoreTable : MonoBehaviour {

	private Text label;

	private Dictionary<string, int> playersScores = new Dictionary<string, int>();

	void Start() {
		label = this.gameObject.GetComponent<Text>();
		IPlayer.scoreDelegate += UpdatePlayerScore;
	}

	private void UpdatePlayerScore(string playerName, int score) {
		if (playersScores.ContainsKey(playerName)) {
			playersScores[playerName] = score;
		} else {
			playersScores.Add(playerName, score);
		}
	}

	void Update () {
		string scores = "";

		foreach (string player in playersScores.Keys) {
			scores += player + " : " + playersScores[player] + " / ";
		}

		label.text = scores;
	}
}
