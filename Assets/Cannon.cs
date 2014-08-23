using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject cannonBall;

	public float cannonVelocity;

	private float timeSinceLastFire;
	public float fireInterval;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastFire += Time.deltaTime;
	}

	void Fire () {

		if (timeSinceLastFire >= fireInterval) {
			GameObject cBall = (GameObject)Instantiate (cannonBall, transform.position, transform.rotation);
			cBall.rigidbody.AddForce (transform.up * cannonVelocity, ForceMode.Impulse);

			timeSinceLastFire = 0;
		}
	}
}
