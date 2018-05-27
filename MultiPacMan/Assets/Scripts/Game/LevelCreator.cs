using System;
using System.Collections.Generic;
using UnityEngine;
using MultiPacMan.Pellet;

namespace MultiPacMan.Game
{
	public struct Point : IEquatable<Point> {
		public readonly int x;
		public readonly int y;

		public Point(int x, int y) {
			this.x = x;
			this.y = y;
		}
			
		public bool Equals(Point other) {
			return (this.x == other.x) && (this.y == other.y);
		}

		public override bool Equals (object obj) {
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((Point) obj);
		}

		public override int GetHashCode() {
			return x*100 + y;
		}
	}

	public class LevelCreator : MonoBehaviour {

		public delegate void PelletCreated(PelletBehaviour pellet, Point positionOnMap);
		public PelletCreated pelletCreated; 

		private static int LOW_PELLET_SCORE = 1;
		private static int HIGH_PELLET_SCORE = 5;

		[SerializeField]
		private GameObject wallPrefab;
		[SerializeField]
		private GameObject pelletPrefab;

		private int[,] maze = new int[25, 23] {
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 0 },
			{ 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
			{ 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
			{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
			{ 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0 },
			{ 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 2, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 0, 2, 0 },
			{ 0, 0, 0, 0, 0, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 1, 0, 0, 0, 2, 0 },
			{ 0, 3, 2, 2, 2, 1, 2, 2, 0, 3, 3, 2, 3, 3, 0, 2, 2, 1, 2, 2, 2, 3, 0 },
			{ 0, 2, 0, 0, 0, 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 2, 0, 0, 0, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 2, 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 2, 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0 },
			{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
			{ 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
			{ 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0 },
			{ 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0 },
			{ 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0 },
			{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
			{ 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
			{ 0, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
		};

		public IList<Vector2> GetPlayersPositions() {
			IList<Vector2> positions = new List<Vector2>();

			for (int i = 0; i < maze.GetLength(0); ++i) {
				for (int j = 0; j < maze.GetLength(1); ++j) {
					if (maze[i, j] == 5) {
						Vector3 spriteExtents = wallPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents;
						Vector3 wallScale = wallPrefab.transform.localScale;

						Vector2 position = new Vector2(
							spriteExtents.x * wallScale.x * 2 * j,
							spriteExtents.y * wallScale.y * 2 * i
						);

						positions.Add(position);
					}
				}
			}

			return positions;
		}

		public void Create() {
			Vector2 position = Vector2.zero;
			Vector3 spriteExtents = wallPrefab.GetComponent<SpriteRenderer> ().sprite.bounds.extents;
			Vector3 wallScale = wallPrefab.transform.localScale;

			for (int i = 0; i < maze.GetLength(0); ++i) {
				for (int j = 0; j < maze.GetLength(1); ++j) {
					CreateCell(maze[i, j], position, new Point(i, j));
					position.x += spriteExtents.x * wallScale.x * 2;
				}

				position.x = 0.0f;
				position.y += spriteExtents.y * wallScale.y * 2;
			}
		}

		private void CreateCell(int value, Vector2 position, Point positionOnMap) {
			switch (value) {
			case 0:
				CreateWall(position);
				break;
			case 1:
				CreatePellet(LOW_PELLET_SCORE, position, positionOnMap);
				break;
			case 3:
				CreatePellet(HIGH_PELLET_SCORE, position, positionOnMap);
				break;
			default:
				break;
			}
		}

		private void CreateWall(Vector2 position) {
			Instantiate(wallPrefab, position, Quaternion.identity);
		}

		private void CreatePellet(int score, Vector2 position, Point positionOnMap) {
			Vector3 pos = new Vector3(position.x, position.y, 0.0f);

			GameObject pelletGameObject = GameObject.Instantiate(pelletPrefab, pos, Quaternion.identity) as GameObject;
			PelletBehaviour pellet = pelletGameObject.GetComponent<PelletBehaviour>();
			pellet.Setup(score, positionOnMap.x, positionOnMap.y);

			if (pelletCreated != null) {
				pelletCreated(pellet, positionOnMap);
			}
		}
	}
}
