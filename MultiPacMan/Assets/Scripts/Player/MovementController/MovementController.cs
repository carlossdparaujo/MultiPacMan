using UnityEngine;
using System.Collections;
using MultiPacMan.Player.InputInterpreter;

namespace MultiPacMan.Player
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class MovementController : MonoBehaviour {

		protected Rigidbody2D rb;

		public virtual void Start() {
			rb = GetComponent<Rigidbody2D>();
		}
	}
}
