using UnityEngine;

public class ChainCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		Chain.isFired = false;

		if (col.CompareTag("Ball")) {
			col.GetComponent<Ball>().Split();
		}
	}
}
