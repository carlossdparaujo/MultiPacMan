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

        private PhotonPlayerCreationService playerService;
        private PlayerCreator playerCreator;
        private LevelCreator levelCreator;

		private bool canCreatePlayer = false;
		private PlayerCreationRequest playerRequest = null;

        void Start () {
            PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;
            GameController.roomEnteredDelegate += RequestPlayerCreation;
			GameController.gameStartedDelegate += EnablePlayerCreation;
            GameController.gameEndedDelegate += DestroyPlayers;

            playerService = new PhotonPlayerCreationService ();
			playerCreator = new PlayerCreator (playerService);
            levelCreator = this.gameObject.GetComponent<LevelCreator> ();

            RaiseEventOptions options = new RaiseEventOptions ();
            options.CachingOption = EventCaching.AddToRoomCacheGlobal;
            options.Receivers = ReceiverGroup.All;

            PhotonNetwork.RaiseEvent ((byte) Events.NEW_PLAYER_ENTERED, null, true, options);
        }

        void OnDestroy () {
            PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
            GameController.roomEnteredDelegate -= RequestPlayerCreation;
            GameController.gameEndedDelegate -= DestroyPlayers;
        }

		public void RequestPlayerCreation () {
			RaiseEventOptions options = new RaiseEventOptions ();
			options.CachingOption = EventCaching.AddToRoomCacheGlobal;
			options.Receivers = ReceiverGroup.All;

			PhotonNetwork.RaiseEvent ((byte) Events.NEW_PLAYER_ENTERED, null, true, options);
		}

		public void EnablePlayerCreation () {
			canCreatePlayer = true;

			if (playerRequest != null) {
				CreatePlayer (playerRequest);
			}
		}

		public void DestroyPlayers (PlayersStats playersStats) {
			canCreatePlayer = false;
			playerRequest = null;

			foreach (PlayerStats stats in playersStats.Stats) {
				Destroy(GetPlayer(stats.Name).gameObject);
			} 
		}

        public void PhotonNetwork_OnEventCall (byte eventCode, object content, int senderId) {
            if (ReceivedNewPlayerEvent ((int) eventCode)) {
                SendMyUpdatedScore ();

                if (PhotonNetwork.isMasterClient) {
                    PlayerCreationRequest request = playerCreator.GeneratePlayer (senderId, levelCreator.GetPlayersPositions ());

                    RaiseEventOptions options = new RaiseEventOptions ();
                    options.CachingOption = EventCaching.AddToRoomCacheGlobal;
                    options.Receivers = ReceiverGroup.All;

                    PhotonNetwork.RaiseEvent ((byte) Events.ALLOW_PLAYER_CREATION,
                        request.asData (),
                        true, options
                    );
                }
            } else if (ReceivedSetPlayerScoreEvent ((int) eventCode)) {
                PlayerScoreRequest request = new PlayerScoreRequest ((object[]) content);
                UpdatePlayerScore (request);
            } else if (ReceivedAllowPlayerCreationEvent ((int) eventCode)) {
                PlayerCreationRequest request = new PlayerCreationRequest ((object[]) content);

				if (PhotonNetwork.player.ID == request.OwnerId) {
					playerRequest = request;

					if (canCreatePlayer) {
						CreatePlayer (request);
					}
                }
            }
        }

        private bool ReceivedNewPlayerEvent (int eventCode) {
            return eventCode == (int) Events.NEW_PLAYER_ENTERED;
        }

        private bool ReceivedSetPlayerScoreEvent (int eventCode) {
            return eventCode == (int) Events.SET_PLAYER_SCORE;
        }

        private bool ReceivedAllowPlayerCreationEvent (int eventCode) {
            return eventCode == (int) Events.ALLOW_PLAYER_CREATION;
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

        private void CreatePlayer (PlayerCreationRequest request) {
            if (request != null) {
                playerService.CreatePlayer (request);
            }
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