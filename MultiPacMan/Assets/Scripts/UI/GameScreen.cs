using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using UnityEngine;

namespace MultiPacMan.UI {
    public class GameScreen : MonoBehaviour {

        void Awake () {
            GameController.gameStartedDelegate += HandleOnGameStarted;
            GameController.gameEndedDelegate += HandleOnGameEnded;
        }

        void OnDestroy () {
            GameController.gameStartedDelegate -= HandleOnGameStarted;
            GameController.gameEndedDelegate -= HandleOnGameEnded;
        }

        void HandleOnGameStarted () {
            this.gameObject.SetActive (true);
        }

        void HandleOnGameEnded (PlayersStats players) {
            this.gameObject.SetActive (false);
        }
    }
}