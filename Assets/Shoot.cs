using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject player;
	public GameObject prefab;

	public float shootRate;
	public float projectileDuration = 5;
	public float projectileSpeed;

	private float shootCountdown = 0f;

	private Vector2 dir;


	// For shootAround
	public float rotationShootSpeed;
	float angle;
	private Vector2 relativeState;
	private int side;

	void Start () {

		side = 0;
		angle = 360;
		relativeState = new Vector2 (transform.position.y - player.transform.position.y, transform.position.y - player.transform.position.y);
	}
	
	void Update () {

		shootAroundRelativeToPlayer();
	}


	// Built-in patterns

	Vector2 shootToPlayer() {

		dir = relativeToPlayer();
		shoot (dir);
		return dir;
	}

	void shoot4StraightDirections() {

		shoot (new Vector2 (-1, 0));
		shoot (new Vector2 (1, 0));
		shoot (new Vector2 (0, -1));
		shoot (new Vector2 (0, 1));
	}

	void shoot4DiagonalDirections() {
		
		shoot (new Vector2 (-1, -1));
		shoot (new Vector2 (-1, 1));
		shoot (new Vector2 (1, -1));
		shoot (new Vector2 (1, 1));
	}

	Vector2 shootAround() { // 0 = clockwise, 1 = counterclockwise

		if (side == 0)
			angle -= 1 * rotationShootSpeed;
		else
			angle += 1 * rotationShootSpeed;
		
		dir = new Vector2 (Mathf.Cos((Mathf.PI / 180) * angle), Mathf.Sin((Mathf.PI / 180) *angle));
		shoot (dir);
		return dir;
	}

	Vector2 shootAroundRelativeToPlayer() {

		Vector2 dirP = relativeToPlayer ();

		if (relativeState.x < 0 && relativeState.y < 0) {
			if ((dirP.x > 0 && dirP.y < 0) || (dirP.x < 0 && dirP.y > 0)) {
				side = side + 1 % 2;
			}
		}

		if (relativeState.x > 0 && relativeState.y < 0) {
			if ((dirP.x < 0 && dirP.y < 0) || (dirP.x > 0 && dirP.y > 0)) {
				side = side + 1 % 2;
			}
		}

		if (relativeState.x < 0 && relativeState.y > 0) {
			if ((dirP.x > 0 && dirP.y > 0) || (dirP.x < 0 && dirP.y < 0)) {
				side = side + 1 % 2;
			}
		}

		if (relativeState.x > 0 && relativeState.y > 0) {
			if ((dirP.x < 0 && dirP.y > 0) || (dirP.x > 0 && dirP.y < 0)) {
				side = side + 1 % 2;
			}
		}

		return shootAround ();
	}

	// Shoot

	void shoot(Vector2 direction) {
		
		if (shootCountdown <= 0) {
			shootV (direction);
			shootCountdown = 1 / shootRate;
		}

		shootCountdown -= Time.deltaTime;
	}

	void shootV(Vector2 direction) {	

		GameObject projectile;
		projectile = Instantiate(prefab, this.transform.position, Quaternion.identity);
		projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;

		Destroy (projectile, projectileDuration);
	}

	// Helpers
	
	Vector2 relativeToPlayer(){
				
		return new Vector2 (transform.position.x - player.transform.position.x, transform.position.y - player.transform.position.y);
	}
}