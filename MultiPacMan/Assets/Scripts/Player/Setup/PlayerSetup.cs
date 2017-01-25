﻿using UnityEngine;
using System.Collections;

namespace MultiPacMan.Player
{
	public abstract class PlayerSetup : MonoBehaviour {

		[SerializeField]
		private TextMesh playerName;

		void Awake() {
			IPlayer player;

			if (IsMine()) {
				player = SetLocalPlayer();
				Camera.main.GetComponent<FollowPlayer>().Follow(this.gameObject);
			} else {
				player = SetNetworkedPlayer();
			}

			playerName.text = player.PlayerName;
		}

		protected abstract bool IsMine();
		protected abstract IPlayer SetLocalPlayer();
		protected abstract IPlayer SetNetworkedPlayer();
	}
}
