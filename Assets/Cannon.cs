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

	public float maxHTurnAngle = 85f;

	enum shipLocation {Port, Starboard};

	//public Vector3 targetRotation;

	//private Vector3 resetPosition;
	//private Quaternion resetRotation;

	public float rotateSpeed = 5f;
	public float accuracyThreshhold = 3;

	// Use this for initialization
	void Start () {
		//resetPosition =  cannonBase.transform.localPosition;

		//resetPosition = transform.forward;

		//Debug.Log (resetPosition.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastFire += Time.deltaTime;


		if (primaryStation.GetComponent<stationAi> ().Status != stationAi.StationStatus.Manned) {
			return;
				}
//		if (currentTarget != null) {
//			RotatetoTarget(currentTarget);
//				
//		}

		if (currentTarget == null) {
			RotatetoDefault();
			GameObject g = FindTarget ();

			if (g != null) {
				SetTarget (g);
			}
		} else {
			if (ObjectisAimable (currentTarget)) {
				RotatetoTarget (currentTarget);	
				//Fire ();
			} else {
				//if current target is out of view, forget it
				currentTarget = null;
			}
		}


//		GameObject g  = FindTarget ();
//		if (g != null) {
//			SetTarget(g);
//				}
//	
//		if (currentTarget != null) {
//			RotatetoTarget(currentTarget);
//				
//		}

	}

	void Fire () {
		//Maximum range for set velocity:

		//R = (v^2 * sin (2 *angle))/ G

		//Angle = .5 * sin^-1 (gR / v^2) 

		//Change acceleration to velocity?

		if (timeSinceLastFire >= fireInterval && primaryStation.GetComponent<stationAi>().Status == stationAi.StationStatus.Manned && currentTarget != null) {
			GameObject cBall = (GameObject)Instantiate (cannonBall, barrel.transform.position, barrel.transform.rotation);
			cBall.rigidbody.AddForce (barrel.transform.up * cannonVelocity, ForceMode.Impulse);

			timeSinceLastFire = 0;


		}
	}

	GameObject FindTarget() {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy")) {
			if (ObjectisAimable(g)) {
				return g;
				}
			}

		return null;
		}

	void SetInclinetoTarget(GameObject g) {
//		RaycastHit r;
//
//		if (Physics.Raycast (new Ray (transform.position, transform.up), out r)) {
//
//			}

		//(1/2)*asin(9.81* self.Distance from / self.Power ^2)


		//float targetDistance = Vector3.Distance (transform.position, g.transform.position);

	//	float angle = .5f * Mathf.Asin (9.81f * targetDistance / Mathf.Pow(cannonVelocity,2));

//		Debug.Log (targetDistance);
//		Debug.Log (angle);
//		//Need to rotate on X axis

//		if (targetDistance > 500) {
//
//		}
	}

	void RotatetoTarget(GameObject g) {
		Vector3 adjustedTargetPosition = new Vector3 (g.transform.position.x, cannonBase.transform.position.y, g.transform.position.z);

		//Vector3 relativePos = adjustedTargetPosition - cannonBase.transform.position;
		Vector3 relativePos = adjustedTargetPosition - transform.position;

//		Quaternion rotation = Quaternion.LookRotation(relativePos);
//		cannonBase.transform.rotation = rotation;
//
		//cannonBase.transform.LookAt (g.transform.position);

		float angletoTarget = Vector3.Angle (transform.forward, relativePos);

		//Debug.Log (angletoTarget + ", " + maxHTurnAngle);

//		if (cannonBase.transform.rotation == Quaternion.LookRotation(relativePos)) {
//			Fire ();
//		} else {
		//Debug.Log (cannonBase.transform.rotation.ToString () + "|| " +  Quaternion.LookRotation (relativePos).ToString ());

		float aimDelta = Vector3.Angle (cannonBase.transform.forward, relativePos);
		Debug.Log (aimDelta);
			if (angletoTarget <= maxHTurnAngle) {
					cannonBase.transform.rotation = Quaternion.Slerp (cannonBase.transform.rotation, Quaternion.LookRotation (relativePos), rotateSpeed * Time.deltaTime);
					} 
//		}
		if (aimDelta < accuracyThreshhold) {
			Fire();
		}
		//Debug.Log (Vector3.Angle (transform.forward, relativePos));
	}

	void RotatetoDefault() {
		cannonBase.transform.rotation = Quaternion.Slerp (cannonBase.transform.rotation, Quaternion.LookRotation(transform.forward), rotateSpeed * Time.deltaTime);
	}

	bool ObjectisAimable(GameObject g) {
		Vector3 adjustedTargetPosition = new Vector3 (g.transform.position.x, cannonBase.transform.position.y, g.transform.position.z);
		
		//Vector3 relativePos = adjustedTargetPosition - cannonBase.transform.position;
		Vector3 relativePos = adjustedTargetPosition - transform.position;
						
		float angletoTarget = Vector3.Angle (transform.forward, relativePos);

		return (angletoTarget <= maxHTurnAngle);
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



