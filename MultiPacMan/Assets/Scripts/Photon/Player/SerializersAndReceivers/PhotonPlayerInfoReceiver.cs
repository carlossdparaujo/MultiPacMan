using System.Collections;
using UnityEngine;

namespace MultiPacMan.Photon.Player.SerializersAndReceivers {
    public class PhotonPlayerInfoReceiver : MonoBehaviour, IPunObservable {

        public delegate void SetPlayerPosition (Vector2 position, Vector2 velocity);
        public SetPlayerPosition positionDelegate;

        public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
            if (positionDelegate == null) {
                return;
            }

            if (stream.isWriting == false) {
                Vector2 pos = DecompressPosition (stream.ReceiveNext ());
                Vector2 vel = DecompressVelocity (stream.ReceiveNext ());
                positionDelegate (pos, vel);
            }
        }

        // Será que não é melhor fazer T ao invés de object?
        protected virtual Vector2 DecompressPosition (object data) {
            return (Vector2) data;
        }

        protected virtual Vector2 DecompressVelocity (object data) {
            return (Vector2) data;
        }
    }
}