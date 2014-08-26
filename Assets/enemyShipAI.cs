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
	}

	void orbitBase() {
		transform.RotateAround (playerShip.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
		}
}
