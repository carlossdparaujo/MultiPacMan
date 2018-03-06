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
		}
		
		void Update () {
			PhotonLocalPlayer player = GameController.GetMyPlayer();

			if (player == null) {
				return;
			}
				
			slider.normalizedValue = player.GetTurboFuelPercentage();
		}
	}
}
