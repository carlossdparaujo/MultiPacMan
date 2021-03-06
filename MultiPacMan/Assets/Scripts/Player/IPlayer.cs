using System;
using MultiPacMan.Player.Turbo;
using UnityEngine;

namespace MultiPacMan.Player {
    [RequireComponent (typeof (PlayerSpriteDirectionChanger))]
    public abstract class IPlayer : MonoBehaviour, IComparable<IPlayer> {

        private int score = 0;
        public int Score {
            get {
                return score;
            }
            set {
                score = value;
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
                return this.GetComponent<PlayerSpriteDirectionChanger> ();
            }
        }

        public void Setup (PlayerStats stats) {
            this.playerName = stats.Name;
            this.color = stats.Color;
            this.score = stats.Score;

            AddComponents ();
        }

        protected abstract void AddComponents ();

        public void UpdateScore (int value) {
            score = value;
        }

        public void AddToScore (int value) {
            score += value;
        }

        public float GetTurboFuelPercentage () {
            return TurboController.GetTurboFuelPercentage ();
        }

        public PlayerStats GetStats () {
            return new PlayerStats (PlayerName, Color, Score, GetTurboFuelPercentage ());
        }

        protected T Add<T> () where T : MonoBehaviour {
            return this.gameObject.AddComponent<T> ();
        }

        public int CompareTo (IPlayer player) {
            if (player.PlayerName == null) {
                return 1;
            }

            return player.PlayerName.CompareTo (this.name);
        }
    }
}