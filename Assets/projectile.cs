using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float lifeSpan;

	// Use this for initialization
	void Start () {
		if (lifeSpan > 0) {
			Destroy(gameObject, lifeSpan);
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
