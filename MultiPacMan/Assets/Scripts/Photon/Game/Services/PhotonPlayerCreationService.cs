using System;
using MultiPacMan.Game;
using MultiPacMan.Game.Requests;
using MultiPacMan.Game.Services;
using UnityEngine;

namespace MultiPacMan.Photon.Game.Services {
    public class PhotonPlayerCreationService : PlayerCreationService {
        public void CreatePlayer (PlayerCreationRequest request) {
            Vector3 position = new Vector3 (request.PlayerPosition.x, request.PlayerPosition.y, -1.0f);
            PhotonNetwork.Instantiate ("Player", position, Quaternion.identity, 0, request.asData ());
        }
    }
}