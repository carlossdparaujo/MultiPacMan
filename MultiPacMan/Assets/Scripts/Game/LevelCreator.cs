using System;
using UnityEngine;

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

	public delegate void CreatePlayer(Vector2 position);
	public CreatePlayer playerDelegate; 

	public delegate void CreatePellet(Vector2 position, int score, Point positionOnMap);
	public CreatePellet pelletDelegate; 

	[SerializeField]
	private GameObject wallPrefab;

	private int[,] maze = new int[25, 23] {
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
		{ 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
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
		{ 0, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 },
		{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
	};

	public void Create() {
		Vector2 position = Vector2.zero;

		for (int i = 0; i < maze.GetLength(0); ++i) {
			for (int j = 0; j < maze.GetLength(1); ++j) {
				CreateCell(maze[i, j], position, new Point(i, j));
				position.x += wallPrefab.GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2;
			}

			position.x = 0.0f;
			position.y += wallPrefab.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * 2;
		}
	}

	private void CreateCell(int value, Vector2 position, Point positionOnMap) {
		switch (value) {
		case 0:
			Instantiate(wallPrefab, position, Quaternion.identity);
			break;
		case 1:
			if (pelletDelegate == null) {
				return;
			}

			pelletDelegate(position, 1, positionOnMap);
			break;
		case 3:
			if (pelletDelegate == null) {
				return;
			}

			pelletDelegate(position, 5, positionOnMap);
			break;
		case 5:
			if (playerDelegate == null) {
				return;
			}

			playerDelegate(position);
			break;
		default:
			break;
		}
	}
}

