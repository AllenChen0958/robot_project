using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScene : MonoBehaviour {
	[SerializeField]
	private bool enable=true;
	[SerializeField]
	private GameObject [] scenes;

	void Start() {
		
		//UnityEngine.Random.InitState (System.DateTime.Now.Second);
		if (enabled) {
			int i = 0;
			int tarScenesIdx = Random.Range (0, scenes.Length);
			for (; i < scenes.Length; i++) {
				if (tarScenesIdx == i) {
					scenes [i].SetActive (true);
				} else {
					scenes [i].SetActive (false);
				}
			}
		}
	}
}
