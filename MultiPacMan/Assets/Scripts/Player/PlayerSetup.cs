using System.Collections;
using MultiPacMan.Game;
using UnityEngine;

namespace MultiPacMan.Player {
    public abstract class PlayerSetup : MonoBehaviour {

        [SerializeField]
        private TextMesh playerName;

        public void StartSetup (PlayerStats stats, bool isMine) {
            IPlayer player;

            if (isMine) {
                player = SetLocalPlayer ();
                Camera.main.GetComponent<FollowPlayer> ().Follow (this.gameObject);
            } else {
                player = SetNetworkedPlayer ();
            }

            playerName.text = player.PlayerName;
            player.Setup (stats);
        }

        protected abstract IPlayer SetLocalPlayer ();
        protected abstract IPlayer SetNetworkedPlayer ();
    }
}