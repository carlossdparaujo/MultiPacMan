using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class StartGameScreen : MonoBehaviour {
        void Awake () {
            GameController.gameStartedDelegate += HandleOnGameStarted;
        }

        void HandleOnGameStarted () {
            this.gameObject.SetActive (false);
        }
    }
}