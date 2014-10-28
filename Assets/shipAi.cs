using UnityEngine;
using System.Collections;

public class shipAi : MonoBehaviour {

	GameObject currentMissionTarget;
	GameObject currentTarget;
		
	Quaternion currentIntendedRotation;
	
	public float maxSpeed;
	public float speed = 0;
	public float acceleration;
	
	public float turnSpeed;

	public float circlingDistance = 30f;

	public float distancetoTarget;

	public float collisionDetectModifier;
	public float collisionCheckInterval;
	public float swerveCooldown;

	float timeSinceSwerve;	
	float timeSinceCollisionCheck= 0;

	Vector3 bankingVector =Vector3.zero;
	public float bankLimit = 10;

	public GameObject HomeShip;

	public enum mission {Attack, Defend, Harvest, None};
	public mission currentMission = mission.None;

	public enum state {Launching,Landing, Seeking, MovingtoTargetDistance, Swerving,  Circling, None};

	public bool RequestingLanding = false;

	public state currentState = state.None;
	//state previousState;

	public GameObject landingZone;

	public GameObject PrimaryStation;

	public Material avoidMaterial;
	
	void Start () {
		
	}

	//guns should fire as coroutine - seperate script


	void Update () {
//		if (currentState == state.Stopped) {
//			return;
//		}

		switch (currentMission) {
			case mission.None:
			//If no mission or no crew, exit
			if (currentMission == mission.None || PrimaryStation.GetComponent<stationAi>().Status != stationAi.StationStatus.Manned  ) {
				return;
			}
			break;

		case mission.Defend:

			break;
		}

		//If has mission and crew and is stil ldock ed to parent, launch
		if (currentMission != mission.None && PrimaryStation.GetComponent<stationAi> ().Status == stationAi.StationStatus.Manned && transform.parent != null && currentState != state.Launching) {
			Launch ();
		}


		if (currentState == state.Landing && landingZone != null) {
			BeginFinalLanding();
			}

		if (speed < maxSpeed){
			speed += acceleration;			
		}
		
		timeSinceCollisionCheck += Time.deltaTime;
		if (timeSinceCollisionCheck > collisionCheckInterval) {
	//		checkForPendingCollision ();

			if (currentState == state.Circling) {
				currentState = state.MovingtoTargetDistance;
			}
			timeSinceCollisionCheck =0 ;

		}

		checkForSteering ();
		
		//reduce speed during turn?
		transform.Translate(Vector3.forward * Time.deltaTime * speed);		

		if (currentTarget != null) {
			distancetoTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
				}

//		switch (currentState) {
//		case state.Circling:
//			 break;
//
//		case state.Seeking:
//				//transform.LookAt (currentTargetPosition);
//					//currentTargetRotation = currentTarget.transform.position - transform.position;
//			currentIntendedRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);
//			//currentTargetRotation = Quaternion.SetFromToRotation(transform.position, currentTargetPosition);
//				//currentTargetRotation = transform.rotation;
//			break;
//		case state.Swerving :
//			//Debug.Log (transform.rotation + "," +  currentTargetRotation);
//			//if no update to target rotation (i.e. collision has been avoided)
//			//if (transform.rotation == currentTargetRotation) {
//			if (Quaternion.Angle(transform.rotation, currentIntendedRotation) < 5) {
//				Debug.Log("turn done");
//
//
//				timeSinceSwerve += Time.deltaTime;
//				bankingVector = Vector3.zero;
//
//				//Keep travelling same line for a time
//				if (timeSinceSwerve >= swerveCooldown) {
//					Debug.Log ("swerve done");
//
//					//currentState=state.Attacking;
//					timeSinceSwerve = 0;
//
//					//Avoidance completed, resume last state
//					//currentState = previousState;
//				}
//			} 
//				//transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTargetRotation, turnSpeed * Time.deltaTime);
//				//transform.Rotate(
//				break;
//		}	
//		
//		if (currentTarget != null) {
//			
//		}			
		
	}	

	void checkForSteering() {
		if (currentTarget != null) {

			switch (currentState) {
			case state.Circling:
				//transform.RotateAround(currentTarget.transform.position, Vector3.up, Time.deltaTime * speed);
//				float radius = Vector3.Distance(transform.position, currentTarget.transform.position);// <= circlingDistance;
//
//				float circumference = radius * Mathf.PI;
//
//				float distanceTravelled = speed * Time.deltaTime;
//
//				float portionofCircleTravelled = distanceTravelled /circumference  ;
//
//				float degreestoSteer = portionofCircleTravelled * 360;

				//Vector3 radiusToTarget = transform.position - currentTarget.transform.position;
				Vector3 circleCenter = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);

				Quaternion lookAngle = Quaternion.LookRotation (transform.position - circleCenter);
				currentIntendedRotation = lookAngle * Quaternion.Euler(0,95,0);

				//return;
				break;
			case state.MovingtoTargetDistance:
				float distance = Vector3.Distance(transform.position, currentTarget.transform.position);// <= circlingDistance;

				if (Mathf.Abs ( distance - circlingDistance) < 5) {
					currentState = state.Circling;
					return;
				} else {
					//if distance less than circling distance, move away from target
					if (distance < circlingDistance) {
						currentIntendedRotation = Quaternion.Inverse( Quaternion.LookRotation (currentTarget.transform.position - transform.position));
					} //else move towards target
					else {
						currentIntendedRotation = Quaternion.LookRotation (currentTarget.transform.position - transform.position);
					}
				}

				break;
			case state.Seeking: 
			case state.Launching:
				currentIntendedRotation = Quaternion.LookRotation (currentTarget.transform.position - transform.position);

				break;
			//default:
				//currentIntendedRotation = Quaternion.LookRotation (currentTarget.transform.position - transform.position);			

			//break;

			}

			if (currentIntendedRotation != transform.rotation) {
				transform.rotation = Quaternion.Slerp (transform.rotation, currentIntendedRotation, turnSpeed * Time.deltaTime);
			}

		}
	}

	void checkForThreats() {
		//call regularly from update
		//look for enemies within x distance

		}

	state getDefaultState() {
		switch (currentMission) {
		case mission.Attack:
			return state.Seeking;					
			//break;
		case mission.Defend:
			return state.MovingtoTargetDistance;
			//break;
		case mission.Harvest:
			return state.Seeking;	
			//break;
		
			}

		return state.None;
		}

	void getNextState () {
		/*

		If low health, retreat;
		

		switch (mission) {

			if Attack:
				seek target
			if Defend:
				move towards target
				if target within circle range:
					Circle target
			If Harvest

			}
 		*/
	}

	void attack(GameObject g) {
		currentMissionTarget = g;
		//currentTargetPosition = g.transform.position;
		currentMission = mission.Attack;
		currentState = state.Seeking;
		
	}

//	void circle(GameObject g) {
//		currentTarget = g;
//		currentState = state.Circling;
//	}

//	void Defend(GameObject g) {
//
//		}

	int getRandomSign() {
		//return 0 or 1
		if (Random.Range (0, 2) == 0) 
		{
			return 1;
		} else {
			return -1;
				}
	}

	void HitWaypoint (string waypointTag) {

		switch (waypointTag) {
		case "LaunchWayPoint":
				if (currentState == state.Launching) {
					LaunchCompleted();
				}
			break;
		case "LandingWayPoint":
			if (currentState == state.Landing) {
				BeginFinalLanding();
			}
			break;
			}
		}

	void Launch() {
		Debug.Log ("launch");

		transform.parent = null;
		landingZone = null;

		currentTarget = GameObject.FindGameObjectWithTag ("LaunchWayPoint");
		currentState = state.Launching;
	}

	void ReturnandRequestLanding() {
		currentMission = mission.Defend;
		currentState = state.MovingtoTargetDistance;

		currentMissionTarget = HomeShip;
		//currentTarget = HomeShip;

	//	RequestingLanding = true;
		}

	void Land() {
		currentTarget = GameObject.FindGameObjectWithTag ("LandingWayPoint");
		currentState = state.Landing;
	}

	void BeginFinalLanding() {
		currentTarget = landingZone;
	}

	void LaunchCompleted() {
		Debug.Log ("launch completed!");

		currentTarget = currentMissionTarget;
	//	currentState = state.Seeking;
		currentState = getDefaultState ();

		}

	void checkForPendingCollision() {
		RaycastHit r;
		
		//Debug.Log ("Check");
		
		if (Physics.Raycast(new Ray(transform.position, transform.forward), out r, speed * collisionDetectModifier)) {
			//transform.Rotate

			//Need special case  if collider is ground
			Debug.Log ("Collision detected");


			if (bankingVector == Vector3.zero) {
				//bankingVector = new Vector3(Random.Range(-bankLimit, bankLimit), Random.Range(-bankLimit, bankLimit), 0);
				bankingVector = new Vector3(bankLimit * getRandomSign(), bankLimit * getRandomSign(), 0);

			}


			if (r.collider.gameObject.tag == "Ground") {
				//bankingVector.x = Mathf.Abs(bankingVector.x);	
				Debug.Log("Ground!");

				//currentTargetRotation.eulerAngles = new Vector3(bankLimit, transform.rotation.eulerAngles.y + bankingVector.y, transform.rotation.z +  bankingVector.x);
				currentIntendedRotation.eulerAngles = Vector3.up;
			} else {
				currentIntendedRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + bankingVector.x, transform.rotation.eulerAngles.y + bankingVector.y, 0);
			}

			//currentTargetRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.eulerAngles.y - 10, transform.rotation.z);

			//currentTargetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x + bankingVector.x, transform.rotation.eulerAngles.y + bankingVector.y, transform.rotation.z +  bankingVector.x);

			//currentIntendedRotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x + bankingVector.x, transform.rotation.eulerAngles.y + bankingVector.y, transform.rotation.z);

			if (currentState != state.Swerving) {
				//previousState = currentState;
				currentState = state.Swerving;
			}


			//currentTargetRotation = Quaternion.AngleAxis(transform.rotation.y + 10, Vector3.up);
			//currentTargetRotation.eulerAngles.y += 10;

			//currentTargetPosition.x += 100
				; 
		//	transform.renderer.material = avoidMaterial;
			
			//float y = r.collider.bounds.size.y;
			//currentTargetPosition = r.collider.bounds.max;
			
		}
	}
}
