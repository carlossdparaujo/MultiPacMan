using System;
using UnityEngine;

namespace MultiPacMan.Game.Requests {
    public class PlayerScoreRequest : Request {

        private string playerName;
        public string PlayerName {
            get {
                return this.playerName;
            }
        }

        private int score;
        public int Score {
            get {
                return this.score;
            }
        }

        public PlayerScoreRequest (object[] data) {
            this.playerName = (string) data[0];
            this.score = (int) data[1];
        }

        public PlayerScoreRequest (string playerName, int score) {
            this.playerName = playerName;
            this.score = score;
        }

        public object[] asData () {
            return new object[2] {
                playerName,
                score
            };
        }
    }
}