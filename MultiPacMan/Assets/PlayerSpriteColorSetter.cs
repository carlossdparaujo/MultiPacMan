using UnityEngine;
using MultiPacMan.Player;
using System.Collections;

public class PlayerSpriteColorSetter : MonoBehaviour {

	[SerializeField]
	private GameObject sprite;

	void Update() {
		IPlayer player = GetComponent<MultiPacMan.Player.PhotonPlayer>();

		if (player == null) {
			return;
		}

		sprite.GetComponent<SpriteRenderer>().color = player.Color;
	}
}
