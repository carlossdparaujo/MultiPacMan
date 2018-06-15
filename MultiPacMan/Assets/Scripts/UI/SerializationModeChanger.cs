using System.Collections;
using MultiPacMan.Player.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class SerializationModeChanger : MonoBehaviour {

        [SerializeField]
        private Toggle defaultToggle;
        [SerializeField]
        private Toggle interpolationToggle;
        [SerializeField]
        private Toggle interpolationAndExtrapolationToggle;

        public void TurnDefaultSerializationOn (bool value) {
            if (value) {
                NetworkedMovementController.serializationOption = NetworkingOptions.Default;
                interpolationToggle.isOn = false;
                interpolationAndExtrapolationToggle.isOn = false;
            }
        }

        public void TurnInterpolationSerializationOn (bool value) {
            if (value) {
                NetworkedMovementController.serializationOption = NetworkingOptions.Interpolation;
                defaultToggle.isOn = false;
                interpolationAndExtrapolationToggle.isOn = false;
            }
        }

        public void TurnInterpolationAndExtrapolationSerializationOn (bool value) {
            if (value) {
                NetworkedMovementController.serializationOption = NetworkingOptions.InterpolationAndExtrapolation;
                defaultToggle.isOn = false;
                interpolationToggle.isOn = false;
            }
        }
    }
}