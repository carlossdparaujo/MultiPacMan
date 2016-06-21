using UnityEngine;
using System.Collections;

public class LocalPelletEater : PelletEater {
	
	protected override void EatPellet(GameObject pellet) {
		Destroy(pellet);
	}
}
