using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;

namespace MultiPacMan.UI
{
	public class GameScreen : MonoBehaviour {

		void Awake() {
			GameController.gameStartedDelegate += HandleOnGameStarted;
			GameController.gameEndedDelegate += HandleOnGameEnded;
		}

		void OnDestroy() {
			GameController.gameStartedDelegate -= HandleOnGameStarted;
			GameController.gameEndedDelegate -= HandleOnGameEnded;
		}

		void HandleOnGameStarted() {
			this.gameObject.SetActive(true);
		}

		void HandleOnGameEnded(List<GameController.PlayerData> players) {
			this.gameObject.SetActive(false);
		}
	}
}
