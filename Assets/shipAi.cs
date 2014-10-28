﻿using UnityEngine;
using System.Collections;

public class shipAi : MonoBehaviour {

	GameObject currentMissionTarget;
	GameObject currentTarget;
		
	Quaternion currentIntendedRotation;
	
	public float maxSpeed;
	float speed = 0;
	public float acceleration;
	
	public float turnSpeed;
	
	public float collisionDetectModifier;
	public float collisionCheckInterval;
	public float swerveCooldown;

	float timeSinceSwerve;	
	float timeSinceCollisionCheck= 0;

	Vector3 bankingVector =Vector3.zero;
	public float bankLimit = 10;


	public enum mission {Attack, Defend, Harvest, None};
	public mission currentMission = mission.None;

	public enum state {Launching, Seeking, Swerving, Landing, Circling, None};
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



		//If no mission or no crew, exit
		if (currentMission == mission.None || PrimaryStation.GetComponent<stationAi>().Status != stationAi.StationStatus.Manned  ) {
			return;
		}

		//If has mission and crew and is stil ldock ed to parent, launch
		if (currentMission != mission.None && PrimaryStation.GetComponent<stationAi> ().Status == stationAi.StationStatus.Manned && transform.parent != null && currentState != state.Launching) {
			Launch ();
				}

		if (speed < maxSpeed){
			speed += acceleration;			
		}
		
		timeSinceCollisionCheck += Time.deltaTime;
		if (timeSinceCollisionCheck > collisionCheckInterval) {
			checkForPendingCollision ();
			timeSinceCollisionCheck =0 ;
		}

		checkForSteering ();
		
		//reduce speed during turn?
		transform.Translate(Vector3.forward * Time.deltaTime * speed);		


//		switch (currentState) {
//		case state.Circling:
//				if (Vector3.Distance(transform.position, currentTarget.transform.position) < 30) {
//				//transform.RotateAround(currentTarget.transform.position, Vector3.up, Time.deltaTime * 
//			}
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
		currentIntendedRotation = Quaternion.LookRotation(currentTarget.transform.position - transform.position);

		if ( currentIntendedRotation != transform.rotation) {
			transform.rotation = Quaternion.Slerp (transform.rotation, currentIntendedRotation, turnSpeed * Time.deltaTime);
		}

	}

	void checkForThreats() {
		//call regularly from update
		//look for enemies within x distance

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
		//currentState = state.Seeking;
		
	}

	void circle(GameObject g) {
		currentTarget = g;
		currentState = state.Circling;
	}

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
				if (currentState = state.Launching) {
					LaunchCompleted();
				}
			break;
		case "LandingWayPoint":
			if (currentState = state.Landing) {
				BeginFinalLanding();
			}
			break;
			}
		}

	void Launch() {
		Debug.Log ("launch");

		transform.parent = null;

		currentTarget = GameObject.FindGameObjectWithTag ("LaunchWayPoint");
		currentState = state.Launching;
	}

	void Land() {
		currentTarget = GameObject.FindGameObjectWithTag ("LandingWayPoint");
		currentState = state.Landing;
	}

	void BeginFinalLanding() {

	}

	void LaunchCompleted() {
		Debug.Log ("launch completed!");

		currentTarget = currentMissionTarget;
		currentState = state.Seeking;
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
