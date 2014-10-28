using UnityEngine;
using System.Collections;

public class enemyShipAI : MonoBehaviour {
	public GameObject playerShip;
	public float orbitSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		orbitBase ();

		//transform.rigidbody.AddForce (transform.forward * orbitSpeed * Time.deltaTime);


		float d = Vector3.Distance (transform.position, playerShip.transform.position);

		//Debug.Log ("Enemy distance:  " + d);

		if (d > 200) {
			Debug.Log ("Distance exceeded");

			orbitSpeed *= -1;

				}
	}

	void orbitBase() {
		transform.RotateAround (playerShip.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
		}
}
