using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MultiPacMan.UI {
	public class PlayAgainButton : MonoBehaviour {
		public void PlayAgain() {
			Scene scene = SceneManager.GetActiveScene(); 
			SceneManager.LoadScene(scene.name);
		}
	}
}
	
