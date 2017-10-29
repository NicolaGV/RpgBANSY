using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	private Rigidbody2D body;

	public GameObject player;

	private Vector2 direction;

	private float leftBound;
	private float rightBound;
	private float upBound;
	private float downBound;

	public float enemySpeed;

	void Start () {

		body = this.GetComponent<Rigidbody2D> ();

		//To test:
		leftBound = -9f;
		rightBound = 9f;
		upBound = 4;
		downBound = -4;

		// Start direction
		changeDirection (direction);
	}
	
	void Update () {
		changeDirection(moveToPlayer());
	}


	// Built-in enemy movements


	// Start

	Vector2 randomDirection() {
	
		return new Vector2 (Random.Range (-10, 10), Random.Range (-10, 10)).normalized; 
	}

	Vector2 randomDiagonalDirection() {
		
		switch ((int)Random.Range (0, 4)) {
			
		case 0:
			return new Vector2 (-1, -1).normalized;
		case 1:
			return new Vector2 (-1, 1).normalized;
		case 2:
			return new Vector2 (1, -1).normalized;
		case 3:
			return new Vector2 (1, 1).normalized;
		default:
			return new Vector2 (0, 0);
		}
	}

	Vector2 randomStraightDirection() {
			switch ((int)Random.Range (0, 4)) {
			
		case 0:
			return new Vector2 (-1, 0);
		case 1:
			return new Vector2 (1, 0);
		case 2:
			return new Vector2 (0, -1);
		case 3:
			return new Vector2 (0, 1);
		default:
			return new Vector2 (0, 0);
		}
	}

	Vector2 random8Direction() {

		switch ((int)Random.Range (0, 8)) {

		case 0:
			return new Vector2 (-1, -1).normalized;
		case 1:
			return new Vector2 (-1, 1).normalized;
		case 2:
			return new Vector2 (1, -1).normalized;
		case 3:
			return new Vector2 (1, 1).normalized;
		case 4:
			return new Vector2 (-1, 0);
		case 5:
			return new Vector2 (1, 0);
		case 6:
			return new Vector2 (0, -1);
		case 7:
			return new Vector2 (0, 1);

		default:
			return new Vector2 (0, 0);
		}

	}



	// Update
	Vector2 moveToPlayer() {

		Vector2 dir = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;

		return dir;
	}

	Vector2 bounceBoundary(Vector2 dir) {

		if (transform.position.x < leftBound || transform.position.x > rightBound)
			dir = new Vector2 (-dir.x, dir.y);

		if (transform.position.y < downBound || transform.position.y > upBound)
			dir = new Vector2 (dir.x, -dir.y);

		return dir;
	}


	// Final change of directions
	void changeDirection(Vector2 newDirection) {

		direction = newDirection;
		body.velocity = enemySpeed * direction;
	}
}
