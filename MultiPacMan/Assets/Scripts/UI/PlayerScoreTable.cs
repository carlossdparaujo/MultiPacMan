using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using MultiPacMan.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class PlayerScoreTable : MonoBehaviour {

        [SerializeField]
        private GameObject playerScoreCellPrefab;
        private Dictionary<string, PlayerScoreCell> playersScores = new Dictionary<string, PlayerScoreCell> ();

        void Start () {
            GameController.playersStatsDelegate += UpdateCells;
        }

        void UpdateCells (PlayersStats allStats) {
            IList<string> players = new List<string> (playersScores.Keys);

            foreach (PlayerStats playerStats in allStats.Stats) {
                string name = playerStats.Name;
                players.Remove (name);

                if (playersScores.ContainsKey (name)) {
                    UpdatePlayerCell (name, playerStats.Score);
                } else {
                    SetUpNewPlayerCell (name, playerStats.Color);
                }
            }

            foreach (string disconnectedPlayer in players) {
                DeletePlayerCell (disconnectedPlayer);
            }
        }

        void UpdatePlayerCell (string name, int score) {
            playersScores[name].Score = score;
        }

        void SetUpNewPlayerCell (string name, Color color) {
            GameObject playerScore = Instantiate (playerScoreCellPrefab, this.transform) as GameObject;
            PlayerScoreCell cell = playerScore.GetComponent<PlayerScoreCell> ();

            cell.Name = name;
            cell.Score = 0;
            cell.Color = color;

            playersScores.Add (name, cell);
        }

        void DeletePlayerCell (string name) {
            PlayerScoreCell cell = playersScores[name];

            playersScores.Remove (name);
            GameObject.DestroyImmediate (cell.gameObject);
        }
    }
}