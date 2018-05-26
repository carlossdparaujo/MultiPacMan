using System;
using System.Collections.Generic;
using UnityEngine;
using MultiPacMan.Player;

namespace MultiPacMan.Game
{
	public class PlayerCreator {

		private Dictionary<string, Color> schemes = new Dictionary<string, Color> {
			{ "Red Falcon", Color.red },
			{ "Pink Crusader", Color.magenta },
			{ "Spacer Gray", Color.gray },
			{ "Green Bullet", Color.green },
			{ "Blue Skies", Color.blue },
			{ "Black Torpedo", Color.black },
			{ "Sun Ship", Color.yellow }
		};

		public void CreatePlayer(Vector2 position, IList<IPlayer> players) {
			Dictionary<string, Color> schemes = GetRemainingSchemes(players);

			string name = SelectRandomScheme(schemes);
			Color color = schemes[name];

			object[] data = new object[4] { color.r, color.g, color.b, name };
			PhotonNetwork.Instantiate("Player", new Vector3(position.x, position.y, -1.0f), Quaternion.identity, 0, data);
		}

		private Dictionary<string, Color> GetRemainingSchemes(IList<IPlayer> players) {
			IList<string> takenNames = GetTakenNames(players);
			Dictionary<string, Color> remaining = new Dictionary<string, Color>(schemes);

			foreach (string name in takenNames) {
				remaining.Remove(name);
			}

			return remaining;
		}

		private IList<string> GetTakenNames(IList<IPlayer> players) {
			IList<string> takenNames = new List<string>();

			foreach (IPlayer player in players) {
				if (player == null) {
					continue;
				}
					
				takenNames.Add(player.PlayerName);
			}

			return takenNames;
		}

		private string SelectRandomScheme(Dictionary<string, Color> schemes) {
			IList<string> schemeList = new List<string>(schemes.Keys);
			int randomIndex = UnityEngine.Random.Range(0, schemeList.Count);

			return schemeList[randomIndex];
		}
	}
}

