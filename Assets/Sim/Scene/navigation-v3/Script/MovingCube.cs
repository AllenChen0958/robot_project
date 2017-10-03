using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour {
	[SerializeField]
	private Transform end;

	[SerializeField]
	private float duration = 10.0f;

	private float time = 0.0f;
	private float t;

	private Vector3 start_vec;
	private Vector3 end_vec;
	// Use this for initialization
	void Start () {
		start_vec = transform.position;
		end_vec = end.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, end_vec) < 0.0001f) {
			Vector3 temp;
			temp = start_vec;
			start_vec = end_vec;
			end_vec = temp;
			time = 0.0f;
		}	

		time += Time.deltaTime;
		t = time / duration;
		transform.position = Vector3.Lerp(start_vec, end_vec, t);
	}
}
