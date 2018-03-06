using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiPacMan.UI
{
	public class AdvancedMenuButton : MonoBehaviour {

		[SerializeField]
		private GameObject menu;

		public void ToggleMenu() {
			menu.SetActive(!menu.activeSelf);
		}
	}
}
