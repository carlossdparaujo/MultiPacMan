using System;
using UnityEngine;
using MultiPacMan.Player.Inputs;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerBehaviour))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(InputInterpreter))]
	public class PlayerPropertiesUpdater : MonoBehaviour {

		private InputInterpreter inputInterpreter;
		private Rigidbody2D rb;
		private PlayerBehaviour player;

		void Start() {
			inputInterpreter = GetComponent<InputInterpreter>();
			rb = GetComponent<Rigidbody2D>();
			player = GetComponent<PlayerBehaviour>();
		}

		void Update() {
			player.IsTurboOn = inputInterpreter.IsTurboOn();
			player.PlayerPosition = rb.position;
		}
	}
}

