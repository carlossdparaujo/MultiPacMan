using System;
using UnityEngine;

namespace MultiPacMan.Player.Turbo {
    public class LocalTurboController : TurboController {

        [SerializeField]
        private float turboCapacity = 100.0f;
        [SerializeField]
        private float currentTurboCapacity = 100.0f;
        [SerializeField]
        private float turboPerSecond = 30.0f;
        [SerializeField]
        private float turboRecoveryPerSecond = 10.0f;

        public delegate bool IsTurboInUse ();
        public IsTurboInUse turboDelegate;

        void Update () {
            if (turboDelegate == null) {
                return;
            }

            if (IsTurboOn ()) {
                trail.enabled = true;
            } else {
                trail.enabled = false;
            }

            if (turboDelegate ()) {
                float turboUsed = (turboPerSecond * Time.deltaTime);
                currentTurboCapacity = Mathf.Max (currentTurboCapacity - turboUsed, 0);
            } else {
                float turboRecovered = (turboRecoveryPerSecond * Time.deltaTime);
                currentTurboCapacity = Mathf.Min (currentTurboCapacity + turboRecovered, turboCapacity);
            }
        }

        public override bool IsTurboOn () {
            if (turboDelegate == null) {
                return false;
            }

            return turboDelegate () && (currentTurboCapacity > 0);
        }

        public override float GetTurboFuelPercentage () {
            return currentTurboCapacity / turboCapacity;
        }
    }
}