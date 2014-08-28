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


	public float angleAdjust;

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
				//SetInclinetoTarget();
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
			cBall.rigidbody.AddForce (barrel.transform.forward * cannonVelocity, ForceMode.VelocityChange);

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

//
//	float getAngletoTarget() {
////		double x = -(source.x - target.x);
////		double y = (source.y - target.y);
////		double v = 1440; //m/s
////		double g = 25; //m/s
////
//
////		double sqrt = (v*v*v*v) - (g*(g*(x*x) + 2*y*(v*v)));
////		sqrt = Mathf.Sqrt(sqrt);
////		angleInRadians = Math.Atan(((v*v) + sqrt)/(g*x));
////		//Then to convert the radians into a vector that works with XNA, perform the following conversion:
////			
////			Vector2 angleVector = new Vector2(-(float)Math.Cos(angleInRadians), (float)Math.Sin(angleInRadians));
//	} 	

	float getInclinetoTarget() {
//		RaycastHit r;
//
//		if (Physics.Raycast (new Ray (transform.position, transform.up), out r)) {
//
//			}

		//float rangeToTarget = Vector3.Distance (transform.position, currentTarget.transform.position);

		//maximum range:
		//R = (v^2 * sin (2 *angle))/ G

		//angle to hit target at range R:
		//Angle = .5 * sin^-1 (gR / v^2) 

		Vector3 relativePos = currentTarget.transform.position - barrel.transform.position;
		//float rangeToTarget = relativePos

		Debug.Log (relativePos);
		//Debug.Log((9.8f * rangeToTarget) / cannonVelocity * cannonVelocity);

		//float firingAngle = .5f * Mathf.Asin ((9.8f * rangeToTarget) / cannonVelocity * cannonVelocity);

		//Debug.Log (firingAngle);

		//(1/2)*asin(9.81* self.Distance from / self.Power ^2)

		//to hit targets that can be above or below:
		//angle = arctan( (v^2 +- sqrt(v^4- g(gx^2 + 2yv^2) ) / gx  )

		Vector3 adjustedTargetPosition = new Vector3 (currentTarget.transform.position.x, barrel.transform.position.y, currentTarget.transform.position.z);

		float g = Physics.gravity.y;
		//float x = Mathf.Abs (relativePos.x);

		float x = Vector3.Distance (currentTarget.transform.position, transform.position);
		//float x = Vector3.Distance (adjustedTargetPosition, barrel.transform.position);

		float y = relativePos.y;

		//Debug.Log (relativePos.ToString());

		float v2 = Mathf.Pow (cannonVelocity, 2);
		float v4 = Mathf.Pow (cannonVelocity, 4);

		float x2 = Mathf.Pow (x, 2);


		//Debug.Log (Mathf.Sqrt(v4 - (g * ((g * x2)) + (2f * y * v2) )));

		Debug.Log ("Input:  " + x + "," + y);

		float firingAngle = Mathf.Atan(( v2 - Mathf.Sqrt(v4 - (g * ((g * x2) + (2f * y * v2))))) / (g * x) );

		firingAngle = firingAngle * Mathf.Rad2Deg;

		//Debug.Log (firingAngle);


		//return -1 * firingAngle;

		float v = cannonVelocity;
		
		float sqrt = (v*v*v*v) - (g*(g*(x*x) + 2*y*(v*v)));
		sqrt = Mathf.Sqrt(sqrt);

		float angleInRadians = Mathf.Atan(((v*v) - sqrt)/(g*x));


		//Debug.Log (angleInRadians * Mathf.Rad2Deg);
		return angleInRadians * Mathf.Rad2Deg * -1;

		//float angleInRadians = Mathf.Atan2(((v*v) -sqrt), (g*x));

		//return angleInRadians * Mathf.Rad2Deg * -1;

		//barrel.transform.eulerAngles = new Vector3 (firingAngle, barrel.transform.eulerAngles.y, barrel.transform.eulerAngles.z);

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

		//Quaternion targetRotation = Quaternion.LookRotation (relativePos);
		//targetRotation = Quaternion.Euler (getInclinetoTarget(), targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);


		//barrel.transform.localRotation = Quaternion.Euler (getInclinetoTarget (), barrel.transform.localRotation.y, barrel.transform.localRotation.z);

		//barrel.transform.localRotation = Quaternion.AngleAxis (getInclinetoTarget (), barrel.transform.right);

		float incline = getInclinetoTarget ();

		Debug.Log (incline);

		Quaternion q = Quaternion.Euler (incline, barrel.transform.localRotation.y, barrel.transform.localRotation.z);
		Debug.Log (q.ToString ());

		barrel.transform.localRotation = q;

		//"flat" aim is actually x = 90 deg

		float aimDelta = Vector3.Angle (cannonBase.transform.forward, relativePos);
		//Debug.Log (aimDelta);
			if (angletoTarget <= maxHTurnAngle) {
				//	cannonBase.transform.rotation = Quaternion.Slerp (cannonBase.transform.rotation, Quaternion.LookRotation (relativePos), rotateSpeed * Time.deltaTime);

				//cannonBase.transform.rotation = Quaternion.Slerp (cannonBase.transform.rotation, Quaternion.LookRotation (relativePos), rotateSpeed * Time.deltaTime);
					} 
//		}
		//if (aimDelta < accuracyThreshhold) {
			Fire();
		//}
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
		//SetInclinetoTarget ();
	}


	void StepLeft() {
		//barrel.transform.RotateAround (cannonBase.transform.position, transform.up, -3f);
	}
}



