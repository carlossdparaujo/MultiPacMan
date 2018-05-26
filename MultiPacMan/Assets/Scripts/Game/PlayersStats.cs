using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MultiPacMan.Player;
using UnityEngine;

namespace MultiPacMan.Game
{
	public class PlayersStats {

		private ReadOnlyCollection<PlayerStats> playersStats;
		private PlayerStats myPlayerStats;

		public PlayersStats(IList<PlayerStats> playersStats, string myPlayerName) {
			this.playersStats = new ReadOnlyCollection<PlayerStats>(playersStats);

			foreach (PlayerStats playerStats in playersStats) {
				if (playerStats.Name.Equals(myPlayerName)) {
					this.myPlayerStats = playerStats;
					break;
				}
			}

		}

		public ReadOnlyCollection<PlayerStats> Stats { 
			get { 
				return this.playersStats;
			}
		}

		public PlayerStats MyPlayerStats { 
			get { 
				return this.myPlayerStats;
			}
		}
	}
}

