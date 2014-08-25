using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	public GameObject cannonBall;

	public float cannonVelocity;

	private float timeSinceLastFire;
	public float fireInterval;

	public GameObject barrel;
	public GameObject cannonBase;

	private GameObject currentTarget;

	public GameObject primaryStation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastFire += Time.deltaTime;
	}

	void Fire () {
		//Maximum range for set velocity:

		//R = (v^2 * sin (2 *angle))/ G

		//Angle = .5 * sin^-1 (gR / v^2) 

		//Change acceleration to velocity?

		if (timeSinceLastFire >= fireInterval && primaryStation.GetComponent<stationAi>().Status == stationAi.StationStatus.Manned) {
			GameObject cBall = (GameObject)Instantiate (cannonBall, barrel.transform.position, barrel.transform.rotation);
			cBall.rigidbody.AddForce (barrel.transform.up * cannonVelocity, ForceMode.Impulse);

			timeSinceLastFire = 0;


		}
	}


	void SetInclinetoTarget(GameObject g) {
//		RaycastHit r;
//
//		if (Physics.Raycast (new Ray (transform.position, transform.up), out r)) {
//
//			}

		//(1/2)*asin(9.81* self.Distance from / self.Power ^2)


		float targetDistance = Vector3.Distance (transform.position, g.transform.position);

		float angle = .5f * Mathf.Asin (9.81f * targetDistance / Mathf.Pow(cannonVelocity,2));

		Debug.Log (targetDistance);
		Debug.Log (angle);
		//Need to rotate on X axis

//		if (targetDistance > 500) {
//
//		}
	}

	void RotatetoTarget(GameObject g) {
//		Vector3 relativePos = g.transform.position - cannonBase.transform.position;
//		Quaternion rotation = Quaternion.LookRotation(relativePos);
//		cannonBase.transform.rotation = rotation;
//
		cannonBase.transform.LookAt (g.transform.position);
	}
	
	
	void SetTarget (GameObject g) {
		currentTarget = g;
		RotatetoTarget (currentTarget);
		SetInclinetoTarget (currentTarget);
	}


	void StepLeft() {
		//barrel.transform.RotateAround (cannonBase.transform.position, transform.up, -3f);
	}
}



