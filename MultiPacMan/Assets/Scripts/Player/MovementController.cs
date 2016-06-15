using UnityEngine;
using System.Collections;
using MultiPacMan.Player.InputInterpreter;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementInputInterpreter))]
public class MovementController : MonoBehaviour {

	[SerializeField]
	private float speed = 5.0f;

	private Vector3 currentVelocity = Vector3.zero;

	private MovementInputInterpreter inputInterpreter;
	private Rigidbody2D rb;

	void Start() {
		inputInterpreter = GetComponent<MovementInputInterpreter>();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		currentVelocity = inputInterpreter.GetMovementDirection()*speed*Time.fixedDeltaTime;
	}
	
	void Update() {
		rb.MovePosition(rb.position + new Vector2(currentVelocity.x, currentVelocity.y));
	}
}
