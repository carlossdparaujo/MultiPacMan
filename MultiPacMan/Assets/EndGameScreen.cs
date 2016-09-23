using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EndGameScreen : MonoBehaviour {

	[SerializeField]
	private Text winnerName;
	[SerializeField]
	private Text scores;

	void Awake() {
		GameController.gameStartedDelegate += HandleOnGameStarted;
		GameController.gameEndedDelegate += HandleOnGameEnded;
	}

	void OnDestroy() {
		GameController.gameStartedDelegate -= HandleOnGameStarted;
		GameController.gameEndedDelegate -= HandleOnGameEnded;
	}

	void HandleOnGameStarted() {
		this.gameObject.SetActive(false);
	}

	void HandleOnGameEnded(List<GameController.PlayerData> players) {
		int maxScore = 0;
		string winner = "";
		scores.text = "";

		foreach (GameController.PlayerData data in players) {
			if (data.score > maxScore) {
				maxScore = data.score;
				winner = data.name;
			} else if (data.score == maxScore) {
				if (winner.CompareTo(data.name) > 0) {
					maxScore = data.score;
					winner = data.name;
				}
			}
				
			scores.text += data.name + ": " + data.score + "\n";
		}

		winnerName.text = winner;

		this.gameObject.SetActive(true);
	}
}
