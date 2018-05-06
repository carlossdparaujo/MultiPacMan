using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using MultiPacMan.Photon.Player;
using MultiPacMan.Game;

namespace MultiPacMan.UI
{
	[RequireComponent(typeof(Slider))]
	public class PlayerTurboMeter : MonoBehaviour {
		
		private Slider slider;

		void Start() {
			slider = this.gameObject.GetComponent<Slider>();
			GameController.playersStatsDelegate += UpdateTurboPercenatage;
		}

		void UpdateTurboPercenatage(PlayersStats stats) {
			slider.normalizedValue = stats.MyPlayerStats.TurboFuelPercent;
		}
	}
}
