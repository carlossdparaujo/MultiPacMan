using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class PlayerCounter : MonoBehaviour {

        [SerializeField]
        private Text countText;

        void Start () {
            GameController.playerCountDelegate += UpdatePlayerCount;
        }

        void OnDestroy () {
            GameController.playerCountDelegate -= UpdatePlayerCount;
        }

        void UpdatePlayerCount (int playerCount, int maxPlayerCount) {
            countText.text = playerCount + "/" + maxPlayerCount;
        }
    }

}