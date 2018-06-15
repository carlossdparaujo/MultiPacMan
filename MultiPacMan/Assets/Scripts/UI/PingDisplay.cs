using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MultiPacMan.UI {
    public class PingDisplay : MonoBehaviour {
        [SerializeField]
        private Text pingText;

        void Update () {
            pingText.text = "Ping: " + PhotonNetwork.networkingPeer.RoundTripTime;
        }
    }
}