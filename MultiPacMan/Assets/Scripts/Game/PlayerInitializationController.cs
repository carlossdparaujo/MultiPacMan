using System;
using System.Collections.Generic;
using MultiPacMan.Game.Requests;
using MultiPacMan.Game.Services;
using MultiPacMan.Photon.Game.Services;
using MultiPacMan.Player;
using Photon;
using UnityEngine;

namespace MultiPacMan.Game {
    [RequireComponent (typeof (LevelCreator))]
    public class PlayerInitializationController : PunBehaviour {

        private PlayerCreator playerCreator;
        private LevelCreator levelCreator;

        void Start () {
            PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
            GameController.gameStartedDelegate += RequestPlayerCreation;

            PhotonPlayerCreationService service = new PhotonPlayerCreationService ();
            PhotonNetwork.OnEventCall += service.PhotonNetwork_OnEventCall;

            playerCreator = new PlayerCreator (service);
            levelCreator = this.gameObject.GetComponent<LevelCreator> ();
        }

        public void RequestPlayerCreation () {
            RaiseEventOptions options = new RaiseEventOptions ();
            options.CachingOption = EventCaching.AddToRoomCacheGlobal;
            options.Receivers = ReceiverGroup.All;

            PhotonNetwork.RaiseEvent ((byte) Events.NEW_PLAYER_ENTERED, null, true, options);
        }

        public void PhotonNetwork_OnEventCall (byte eventCode, object content, int senderId) {
            if (ReceivedNewPlayerEvent ((int) eventCode)) {
                SendMyUpdatedScore ();

                if (PhotonNetwork.isMasterClient) {
                    playerCreator.AllowPlayerCreation (senderId, levelCreator.GetPlayersPositions ());
                }
            } else if (ReceivedSetPlayerScoreEvent ((int) eventCode)) {
                PlayerScoreRequest request = new PlayerScoreRequest ((object[]) content);
                UpdatePlayerScore (request);
            }
        }

        private bool ReceivedNewPlayerEvent (int eventCode) {
            return eventCode == (int) Events.NEW_PLAYER_ENTERED;
        }

        private void SendMyUpdatedScore () {
            IPlayer myPlayer = GetMyPlayer ();

            if (myPlayer == null) {
                return;
            }

            RaiseEventOptions options = new RaiseEventOptions ();
            options.CachingOption = EventCaching.AddToRoomCacheGlobal;
            options.Receivers = ReceiverGroup.All;

            PhotonNetwork.RaiseEvent ((byte) Events.SET_PLAYER_SCORE,
                new PlayerScoreRequest (myPlayer.PlayerName, myPlayer.Score).asData (),
                true, options
            );
        }

        private IPlayer GetMyPlayer () {
            return (IPlayer) PhotonNetwork.player.TagObject;
        }

        private bool ReceivedSetPlayerScoreEvent (int eventCode) {
            return eventCode == (int) Events.SET_PLAYER_SCORE;
        }

        private void UpdatePlayerScore (PlayerScoreRequest request) {
            IPlayer player = GetPlayer (request.PlayerName);

            if (player != null) {
                player.Score = request.Score;
            }
        }

        private IPlayer GetPlayer (string playerName) {
            foreach (PhotonPlayer photonPlayer in PhotonNetwork.playerList) {
                IPlayer player = (IPlayer) photonPlayer.TagObject;

                if (player == null || player.PlayerName == null) {
                    continue;
                } else if (player.PlayerName.Equals (playerName)) {
                    return player;
                }
            }

            return null;
        }

        public override void OnMasterClientSwitched (PhotonPlayer newMasterClient) {
            base.OnMasterClientSwitched (newMasterClient);

            PlayerCreationService service = new PhotonPlayerCreationService ();
            playerCreator = new PlayerCreator (service, GetPlayers ());
        }

        private List<IPlayer> GetPlayers () {
            List<IPlayer> playerList = new List<IPlayer> ();

            foreach (PhotonPlayer player in PhotonNetwork.playerList) {
                playerList.Add ((IPlayer) player.TagObject);
            }

            return playerList;
        }
    }
}