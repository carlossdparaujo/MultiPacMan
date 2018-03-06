using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MultiPacMan.Player;
using System.Collections.Generic;

namespace MultiPacMan.UI
{
	public class PlayerScoreTable : MonoBehaviour {

		[SerializeField]
		private GameObject playerScoreCellPrefab;
		private Dictionary<string, PlayerScoreCell> playersScores = new Dictionary<string, PlayerScoreCell>();

		void Start() {
			IPlayer.playerCreatedDelegate += SetUpNewPlayerCell;
			IPlayer.scoreDelegate += UpdatePlayerCell;
		}

		void SetUpNewPlayerCell(string name, Color color) {
			GameObject playerScore = Instantiate(playerScoreCellPrefab, this.transform) as GameObject;
			PlayerScoreCell cell = playerScore.GetComponent<PlayerScoreCell>();

			cell.Name = name;
			cell.Score = 0;
			cell.Color = color;

			playersScores.Add(name, cell);
		}

		void UpdatePlayerCell(string name, int score) {
			playersScores[name].Score = score;
		}
	}
}
