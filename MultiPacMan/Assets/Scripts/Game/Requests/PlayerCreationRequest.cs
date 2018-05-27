using System;
using UnityEngine;

namespace MultiPacMan.Game.Requests
{
	public class PlayerCreationRequest : Request {

		private int ownerId;
		public int OwnerId {
			get {
				return this.ownerId;
			}
		}

		private string playerName;
		public string PlayerName {
			get {
				return this.playerName;
			}
		}

		private Color playerColor;
		public Color PlayerColor {
			get {
				return this.playerColor;
			}
		}

		private Vector2 playerPosition;
		public Vector2 PlayerPosition {
			get {
				return this.playerPosition;
			}
		}

		public PlayerCreationRequest(object[] data) {
			this.ownerId = (int) data[0];
			this.playerName = (string) data[1];
			this.playerColor =  new Color((float) data[2], (float) data[3], (float) data[4]);
			this.playerPosition = new Vector2((float) data[5], (float) data[6]);
		}

		public PlayerCreationRequest(int ownerId, string playerName, Color playerColor, Vector2 playerPosition) {
			this.ownerId = ownerId;
			this.playerName = playerName;
			this.playerColor =  playerColor;
			this.playerPosition = playerPosition;
		}

		public object[] asData() {
			return new object[7] { 
				ownerId, 
				playerName, 
				playerColor.r, playerColor.g, playerColor.b, 
				playerPosition.x, playerPosition.y 
			};
		}
	}
}

