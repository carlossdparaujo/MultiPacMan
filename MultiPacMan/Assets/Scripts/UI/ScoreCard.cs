using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class ScoreCard : MonoBehaviour {

        [SerializeField]
        private Text playerName;
        [SerializeField]
        private Text playerScore;

        public void SetInfo (PlayerStats stats) {
            playerName.text = stats.Name;
            playerScore.text = stats.Score.ToString ();
        }
    }
}