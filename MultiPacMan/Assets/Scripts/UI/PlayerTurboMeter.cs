using System.Collections;
using MultiPacMan.Game;
using MultiPacMan.Photon.Player;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    [RequireComponent (typeof (Slider))]
    public class PlayerTurboMeter : MonoBehaviour {

        private Slider slider;

        void Start () {
            slider = this.gameObject.GetComponent<Slider> ();
            GameController.playersStatsDelegate += UpdateTurboPercenatage;
        }

        void OnDestroy () {
            GameController.playersStatsDelegate -= UpdateTurboPercenatage;
        }

        void UpdateTurboPercenatage (PlayersStats stats) {
            slider.normalizedValue = stats.MyPlayerStats.TurboFuelPercent;
        }
    }
}