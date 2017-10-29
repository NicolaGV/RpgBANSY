using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Speed
	public float moveSpeed;

	// UI
		// Health
	public Text healthText;
	public Image healthBar;


	// Health
	public float maxHeatlh;
	private float currentHealth;

	// Animator
	private Animator animator;
	private bool isMove;
	private Vector2 lastMove;

	void Start () {

		// Health
		currentHealth = maxHeatlh;
		DisplayHealth ();


		animator = GetComponent<Animator> ();
	}
	
	void Update () {

		if (Input.GetKeyDown (KeyCode.R))
			ReloadGame ();


		isMove = false;

		// Movements
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			
			transform.Translate (new Vector2 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f));
			lastMove = new Vector2 (Input.GetAxisRaw ("Horizontal"), 0f);
			isMove = true;
		}

		if (Input.GetAxisRaw ("Vertical") != 0) {
			transform.Translate (new Vector2 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime));
			lastMove = new Vector2 (0f, Input.GetAxisRaw ("Vertical"));
			isMove = true;
		}

		// Update animators parameters
		animator.SetFloat ("MoveX", Input.GetAxisRaw ("Horizontal"));
		animator.SetFloat ("MoveY", Input.GetAxisRaw ("Vertical"));
		animator.SetFloat ("LastMoveX", lastMove.x);
		animator.SetFloat ("LastMoveY", lastMove.y);
		animator.SetBool ("IsMove", isMove);


		// Health = 0
		if (currentHealth <= 0) {
			
			ReloadGame ();
		}
			


			
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag ("Fireball")) {
			currentHealth--;
		}
			
		DisplayHealth ();
	}

	void DisplayHealth() {
		
		healthText.text = currentHealth.ToString();
		healthBar.rectTransform.localScale = new Vector2(currentHealth / maxHeatlh, 1f);
	}
		

	void ReloadGame() {
		
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}