using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class MasterOptions : MonoBehaviour {

        [SerializeField]
        private Toggle disableValidationToggle;

        void Update () {
            disableValidationToggle.gameObject.SetActive (PhotonNetwork.isMasterClient);
        }

        public void TurnValidationOff (bool value) {
            LevelController.VALIDATION_ON = !value;
        }
    }
}