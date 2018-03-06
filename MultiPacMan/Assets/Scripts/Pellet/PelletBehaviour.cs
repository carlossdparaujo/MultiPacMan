﻿using UnityEngine;
using System.Collections;
using MultiPacMan.Game;

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

		private bool shouldDestroy = false;

		public void Setup(int score, int x, int y) {
			this.score = score;
			this.point = new Point(x, y);

			animator.GetBehaviour<PelletIdleAnimation>().enteredState += () => {
				if (shouldDestroy) {
					Destroy(this.gameObject);
				} else {
					eaten = false;
				}
			};
		}

		public void AnimatePelletEaten() {
			animator.SetBool("eaten", true);
		}

		public void DestroyAfterAnimation() {
			if (!eaten) {
				Destroy(this.gameObject);
				return;			
			}

			shouldDestroy = true;
		}
	}
}
