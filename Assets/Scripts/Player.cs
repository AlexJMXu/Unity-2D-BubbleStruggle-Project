using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 4f;

	public Rigidbody2D rb;

	private float movement = 0f;

	public float slowness = 10f;

	private bool isDead = false;
	
	// Update is called once per frame
	void Update () {
		if (!isDead) movement = Input.GetAxisRaw("Horizontal") * speed;
	}

	void FixedUpdate() {
		rb.MovePosition(rb.position + Vector2.right * movement * Time.fixedDeltaTime);
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Ball") {
			isDead = true;
			movement = 0f;
			Vector2 dir = col.gameObject.GetComponent<Ball>().rb.position - rb.position;
			rb.AddForce(10000f * dir, ForceMode2D.Force);
			StartCoroutine(RestartLevel());
		}
	}

	IEnumerator RestartLevel() {
		Time.timeScale = 1f/slowness;
		Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

		yield return new WaitForSeconds(1f/slowness);

		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
