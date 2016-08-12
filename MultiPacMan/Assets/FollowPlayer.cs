using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private GameObject target;

	public void Follow(GameObject target) {
		this.target = target;
	}
	
	void LateUpdate() {
		if (target == null) {
			return;
		}

		this.transform.position = new Vector3(
			target.transform.position.x, 
			target.transform.position.y, 
			this.transform.position.z
		);
	}
}
