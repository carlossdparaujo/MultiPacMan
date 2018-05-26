using System;
using UnityEngine;

namespace MultiPacMan.Player
{
	public class PlayerStats {

		private string name;
		private Color color;
		private int score;
		private float turboFuelPercent;

		public PlayerStats(string name, Color color, int score, float turboFuelPercent) {
			this.name = name;
			this.color = color;
			this.score = score;
			this.turboFuelPercent = turboFuelPercent;
		}

		public string Name { 
			get { 
				return this.name;
			}
		}

		public Color Color { 
			get { 
				return this.color;
			}
		}

		public int Score { 
			get { 
				return this.score;
			}
		}

		public float TurboFuelPercent { 
			get { 
				return this.turboFuelPercent;
			}
		}
	}
}

