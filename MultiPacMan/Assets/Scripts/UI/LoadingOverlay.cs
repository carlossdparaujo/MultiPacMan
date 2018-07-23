using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MultiPacMan.Player;
using MultiPacMan.Game;

namespace MultiPacMan.UI {
    public class LoadingOverlay : MonoBehaviour {
        void Start () {
            GameController.playersStatsDelegate += DeactivateOnMyPlayerLoad;
        }

		void OnDestroy () {
            GameController.playersStatsDelegate -= DeactivateOnMyPlayerLoad;
        }

		void DeactivateOnMyPlayerLoad (PlayersStats playersStats) {
			if (playersStats.MyPlayerStats != null) {
				this.gameObject.SetActive (false);
			}
		}
    }
}