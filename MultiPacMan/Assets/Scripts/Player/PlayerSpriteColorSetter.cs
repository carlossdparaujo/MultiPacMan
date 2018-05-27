﻿using UnityEngine;
using MultiPacMan.Photon.Player;
using System.Collections;

namespace MultiPacMan.Player
{
	public class PlayerSpriteColorSetter : MonoBehaviour {

		[SerializeField]
		private GameObject sprite;

		void Update() {
			IPlayer player = GetComponent<IPlayer>();

			if (player == null) {
				return;
			}

			sprite.GetComponent<SpriteRenderer>().color = player.Color;
		}
	}
}
