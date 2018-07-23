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
    public class GameController : PunBehaviour {

        public delegate void OnRoomEntered ();
        public static OnRoomEntered roomEnteredDelegate;

        public delegate void OnGameStarted ();
        public static OnGameStarted gameStartedDelegate;

        public delegate void OnGameEnded (PlayersStats stats);
        public static OnGameEnded gameEndedDelegate;

        public delegate void GetPlayersStats (PlayersStats stats);
        public static GetPlayersStats playersStatsDelegate;

        public delegate void GetPlayerCount (int playerCount, int maxPlayerCount);
        public static GetPlayerCount playerCountDelegate;

        [SerializeField]
        private int playersToStart = 2;
        private bool gameInitiliazed = false;
        private bool isPlaying = false;

        void Start() {
            PhotonNetwork.OnEventCall += PhotonNetwork_OnEventCall;

            LevelController.gameEndedDelegate += NotifyEndGame;

            isPlaying = true;

            if (PhotonNetwork.connected) {
                OnJoinedLobby();
            } else {
                PhotonNetwork.ConnectUsingSettings ("0.0.0");
            }
        }

        void OnDestroy () {
            PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
        }

        public override void OnJoinedLobby () {
            base.OnJoinedLobby ();

            if (isPlaying == false) {
                return;
            }

            RoomOptions roomOptions = new RoomOptions ();
            roomOptions.MaxPlayers = 4;

            PhotonNetwork.JoinOrCreateRoom ("Game", roomOptions, null);
        }

        public override void OnJoinedRoom () {
            base.OnJoinedRoom ();

            if (isPlaying == false) {
                PhotonNetwork.LeaveRoom ();
                return;
            }

            if (roomEnteredDelegate != null) {
                roomEnteredDelegate ();
            }
        }

        public void PhotonNetwork_OnEventCall (byte eventCode, object content, int senderId) {
            if ((int) eventCode == (int) Events.END_GAME_EVENT_CODE) {
                if (gameEndedDelegate != null) {
                    gameEndedDelegate (playersStats ());
                }

                isPlaying = false;
                PhotonNetwork.LeaveRoom ();
            }
        }

        public void NotifyEndGame () {
            if (PhotonNetwork.isMasterClient && gameInitiliazed && isPlaying) {
                RaiseEventOptions options = new RaiseEventOptions ();
                options.CachingOption = EventCaching.AddToRoomCacheGlobal;
                options.Receivers = ReceiverGroup.All;

                PhotonNetwork.RaiseEvent ((byte) Events.END_GAME_EVENT_CODE,
                    null, true, options
                );
            }
        }

        void Update () {
            if (!gameInitiliazed && PhotonNetwork.playerList.Length >= playersToStart) {
                if (gameStartedDelegate != null) {
                    gameStartedDelegate ();
                }

                gameInitiliazed = true;
            }

            playerCountDelegate (GetPlayers ().Count, playersToStart);

            try {
                if (playersStatsDelegate != null) {
                    playersStatsDelegate (playersStats ());
                }
            } catch (InvalidOperationException) {
                // Waiting for my player to connect
            }
        }

        private PlayersStats playersStats () {
            IList<IPlayer> players = GetPlayers ();
            IList<PlayerStats> allStats = new List<PlayerStats> ();

            foreach (IPlayer player in players) {
                if (player == null) {
                    continue;
                }

                PlayerStats playerStats = player.GetStats ();
                allStats.Add (playerStats);
            }

            IPlayer myPlayer = (IPlayer) PhotonNetwork.player.TagObject;
            if (myPlayer == null) {
                throw new InvalidOperationException ("My player still not connected.");
            }

            return new PlayersStats (allStats, myPlayer.PlayerName);
        }

        private List<IPlayer> GetPlayers () {
            List<IPlayer> playerList = new List<IPlayer> ();

            foreach (PhotonPlayer player in PhotonNetwork.playerList) {
                playerList.Add ((IPlayer) player.TagObject);
            }

            return playerList;
        }

        public override void OnLeftRoom () {
            PhotonNetwork.OnEventCall -= PhotonNetwork_OnEventCall;
        }

        private IPlayer GetMyPlayer () {
            return (IPlayer) PhotonNetwork.player.TagObject;
        }
    }
}