using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    [RequireComponent (typeof (Image))]
    public class PlayerScoreCell : MonoBehaviour {

        [SerializeField]
        private Text text;

        private string name;
        public string Name {
            set {
                this.name = value;
            }
        }

        private int score;
        public int Score {
            set {
                this.score = value;
            }
        }

        private Color color;
        public Color Color {
            set {
                this.color = value;
            }
        }

        void Start () {
            this.GetComponent<Image> ().color = color;
        }

        void Update () {
            text.text = name + "\nSCORE: " + score;
        }
    }
}