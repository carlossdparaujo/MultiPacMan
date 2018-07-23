using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using MultiPacMan.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class PlayerThumbTable : MonoBehaviour {

        [SerializeField]
        private GameObject playerThumbCellPrefab;
        private Dictionary<string, PlayerThumbCell> playerThumbs = new Dictionary<string, PlayerThumbCell> ();

        public bool showScore = false;

        void Start () {
            GameController.playersStatsDelegate += UpdateCells;
        }

        void OnDestroy () {
            GameController.playersStatsDelegate -= UpdateCells;
        }

        void UpdateCells (PlayersStats allStats) {
            IList<string> players = new List<string> (playerThumbs.Keys);

            foreach (PlayerStats playerStats in allStats.Stats) {
                string name = playerStats.Name;
                players.Remove (name);

                if (playerThumbs.ContainsKey (name)) {
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
            playerThumbs[name].Score = score;
        }

        void SetUpNewPlayerCell (string name, Color color) {
            GameObject playerScore = Instantiate (playerThumbCellPrefab, this.transform) as GameObject;
            PlayerThumbCell cell = playerScore.GetComponent<PlayerThumbCell> ();

            cell.Name = name;
            cell.Score = 0;
            cell.Color = color;
            cell.showScore = showScore;

            playerThumbs.Add (name, cell);
        }

        void DeletePlayerCell (string name) {
            PlayerThumbCell cell = playerThumbs[name];

            playerThumbs.Remove (name);
            GameObject.DestroyImmediate (cell.gameObject);
        }
    }
}