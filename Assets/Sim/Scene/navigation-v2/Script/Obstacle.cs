using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	[SerializeField]
	private float delay_sec=1.0f;

	[SerializeField]
	private string player_tag = "Player";
	
	private IEnumerator WaitAndDestroy(float sec){
		yield return new WaitForSeconds (sec);
		Destroy (this.gameObject);
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag (player_tag))
			StartCoroutine(WaitAndDestroy (delay_sec));
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag (player_tag))
			StartCoroutine(WaitAndDestroy (delay_sec));
	}
}
