﻿using System;
using UnityEngine;
using MultiPacMan.Player.Turbo;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(PlayerSpriteDirectionChanger))]
	public abstract class IPlayer : MonoBehaviour, IComparable<IPlayer> {

		public delegate void PlayerCreated(string playerName, Color playerColor);
		public static PlayerCreated playerCreatedDelegate;

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		protected string playerName;
		public string PlayerName {
			get {
				return playerName;
			}
		}

		protected Color color;
		public Color Color {
			get {
				return color;
			}
		}

		public abstract TurboController TurboController {
			get;
		}

		public PlayerSpriteDirectionChanger SpriteDirectionChanger {
			get {
				return this.GetComponent<PlayerSpriteDirectionChanger>();
			}
		}

		public void UpdateScore(int value) {
			score = value;
		}

		public void AddToScore(int value) {
			score += value;
		}

		public float GetTurboFuelPercentage() {
			return TurboController.GetTurboFuelPercentage();
		}

		protected T Add<T>() where T : MonoBehaviour {
			return this.gameObject.AddComponent<T>();
		}

		public int CompareTo(IPlayer player) {
			if (player.PlayerName == null) {
				return 1;
			}

			return player.PlayerName.CompareTo(this.name);
		}
	}
}
