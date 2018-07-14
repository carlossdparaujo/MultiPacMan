using System.Collections;
using System.Collections.Generic;
using MultiPacMan.Player;
using UnityEngine;

namespace MultiPacMan.UI {
    public class ScoreTable : MonoBehaviour {

        [SerializeField]
        private GameObject scoreCardPrefab;

        public void AddPlayer (PlayerStats stats) {
            GameObject scoreCard = Instantiate (scoreCardPrefab, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
            scoreCard.transform.parent = this.gameObject.transform;
            scoreCard.GetComponent<ScoreCard> ().SetInfo (stats);
        }
    }
}