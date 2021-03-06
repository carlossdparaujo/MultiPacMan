using System;
using System.Collections.Generic;
using MultiPacMan.Game.Requests;
using MultiPacMan.Game.Services;
using MultiPacMan.Player;
using UnityEngine;

namespace MultiPacMan.Game {
    public class PlayerCreator {

        private Dictionary<string, Color> allSchemes = new Dictionary<string, Color> { { "Red Falcon", Color.red },
            { "Pink Crusader", Color.magenta },
            { "Spacer Gray", Color.gray },
            { "Green Bullet", Color.green },
            { "Blue Skies", Color.blue },
            { "Black Torpedo", Color.black },
            { "Sun Ship", Color.yellow }
        };
        private Dictionary<string, Color> remainingSchemes;

        public PlayerCreator (PlayerCreationService service) {
            this.remainingSchemes = new Dictionary<string, Color> (allSchemes);
        }

        public PlayerCreator (PlayerCreationService service, List<IPlayer> players) : this (service) {
            foreach (IPlayer player in players) {
                if (player == null) {
                    continue;
                }

                this.remainingSchemes.Remove (player.PlayerName);
            }
        }

        public PlayerCreationRequest GeneratePlayer (int newPlayerId, IList<Vector2> playersPositions) {
            string name = SelectRandomScheme (remainingSchemes);
            Color color = remainingSchemes[name];
            remainingSchemes.Remove (name);

            Vector2 position = SelectRandomPosition (playersPositions);

            return new PlayerCreationRequest (newPlayerId, name, color, position);
        }

        private string SelectRandomScheme (Dictionary<string, Color> schemes) {
            IList<string> schemeList = new List<string> (schemes.Keys);
            int randomIndex = UnityEngine.Random.Range (0, schemeList.Count);

            return schemeList[randomIndex];
        }

        private Vector2 SelectRandomPosition (IList<Vector2> playersPositions) {
            int randomIndex = UnityEngine.Random.Range (0, playersPositions.Count);
            return playersPositions[randomIndex];
        }
    }
}