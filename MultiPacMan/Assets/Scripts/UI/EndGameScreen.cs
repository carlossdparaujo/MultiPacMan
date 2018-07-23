using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MultiPacMan.Game;
using MultiPacMan.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class EndGameScreen : MonoBehaviour {

        [SerializeField]
        private Text winnerName;
        [SerializeField]
        private ScoreTable scores;

        void Awake () {
            GameController.gameEndedDelegate += HandleOnGameEnded;
            this.gameObject.SetActive (false);
        }

        void OnDestroy () {
            GameController.gameEndedDelegate -= HandleOnGameEnded;
        }

        void HandleOnGameEnded (PlayersStats playersStats) {
            List<PlayerStats> allStats = OrderByScore (playersStats);

            SetWinnerText (allStats[0]);

            foreach (PlayerStats stats in allStats) {
                scores.AddPlayer (stats);
            }

            this.gameObject.SetActive (true);
        }

        private List<PlayerStats> OrderByScore (PlayersStats playersStats) {
            return playersStats.Stats.OrderByDescending (stats => stats.Score).ToList ();
        }

        private void SetWinnerText (PlayerStats winnerStats) {
            winnerName.text = winnerStats.Name;
            winnerName.color = winnerStats.Color;
        }
    }
}