using System.Collections;
using MultiPacMan.Pellet;
using UnityEngine;

namespace MultiPacMan.Player.PelletEater {
    public class PelletEater : MonoBehaviour {

        public delegate void DidEatPellet (PelletBehaviour pellet);
        public DidEatPellet eatPelletDelegate;

        public void EatPellet (PelletBehaviour pellet) {
            if (eatPelletDelegate == null || pellet.Eaten) {
                return;
            }

            pellet.Eaten = true;

            eatPelletDelegate (pellet);
        }
    }
}