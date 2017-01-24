using UnityEngine;
using System.Collections;

namespace MultiPacMan.Pellet
{
	public class PelletBehaviour : MonoBehaviour {

		[SerializeField]
		private Animator animator;

		private bool eaten = false;
		public bool Eaten {
			get {
				return eaten;
			}
			set {
				eaten = value;
			}
		}

		private int score = 0;
		public int Score {
			get {
				return score;
			}
		}

		private Point point = new Point();
		public Point Point {
			get {
				return point;
			}
		}

		public void Setup(int score, int x, int y) {
			this.score = score;
			this.point = new Point(x, y);

			animator.GetBehaviour<PelletIdleAnimation>().enteredState += () => {
				eaten = false;
			};
		}

		public void AnimatePelletEaten() {
			animator.SetBool("eaten", true);
		}
	}
}
