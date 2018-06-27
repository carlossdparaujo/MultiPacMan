using System;
using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Game.Services;
using MultiPacMan.Pellet;
using MultiPacMan.Photon.Game.Services;
using MultiPacMan.Photon.Player;
using MultiPacMan.Player;
using Photon;
using UnityEngine;

namespace MultiPacMan.Game {
    public class LagSimulationController : PunBehaviour {

        [SerializeField]
        private bool simulateLag = false;
        [SerializeField]
        private int simulatedLagInMs = 100;

        void Update () {
            PhotonNetwork.networkingPeer.IsSimulationEnabled = simulateLag;
            PhotonNetwork.networkingPeer.NetworkSimulationSettings.IncomingLag = simulatedLagInMs;
            PhotonNetwork.networkingPeer.NetworkSimulationSettings.OutgoingLag = simulatedLagInMs;
        }
    }
}