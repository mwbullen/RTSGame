using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hitCollider) {
		Debug.Log ("Waypointhit");


		hitCollider.gameObject.SendMessage ("HitWaypoint", gameObject.tag);
	}
}
