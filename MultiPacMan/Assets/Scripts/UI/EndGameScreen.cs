using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using MultiPacMan.Player;

namespace MultiPacMan.UI
{
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

		void HandleOnGameEnded(PlayersStats playersStats) {
			int maxScore = 0;
			string winner = "";
			scores.text = "";

			foreach (PlayerStats stats in playersStats.Stats) {
				if (stats.Score > maxScore) {
					maxScore = stats.Score;
					winner = stats.Name;
				} else if (stats.Score == maxScore) {
					if (winner.CompareTo(stats.Name) > 0) {
						maxScore = stats.Score;
						winner = stats.Name;
					}
				}
					
				scores.text += stats.Name + ": " + stats.Score + "\n";
			}

			winnerName.text = winner;

			this.gameObject.SetActive(true);
		}
	}
}