using UnityEngine;
using System.Collections;

public class gameControl : MonoBehaviour {

	public GameObject Scout;
	public float crewAssignCheckInterval = 2;
	private float timeSinceCrewAssignCheck;

	// Use this for initialization
	void Start () {
		GameObject[] landingPads = GameObject.FindGameObjectsWithTag ("Respawn");

		foreach (GameObject g in landingPads) {
			Instantiate(Scout, g.transform.position, g.transform.rotation);
				}
		Screen.lockCursor = true;
		/*
		GameObject e = GameObject.FindGameObjectWithTag ("Enemy");
		GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
		
		foreach(GameObject scout in scouts) {
			scout.SendMessage("seek", e);
			
		}
		*/

		callBattleStations();

	}

	GameObject getHitTarget ()
	{
		RaycastHit r;
		if (Physics.Raycast (new Ray(Camera.main.transform.position, Camera.main.transform.forward), out r,Mathf.Infinity)) {
			return r.collider.gameObject;
				}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceCrewAssignCheck = timeSinceCrewAssignCheck + Time.deltaTime;

		if (timeSinceCrewAssignCheck >= crewAssignCheckInterval) {
			HandleCrewmanRequests();
				}

		CheckKeyPress ();

		//GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
//		GameObject g = GameObject.FindGameObjectWithTag ("Enemy");
			
		/*if (Input.GetKey (KeyCode.H)) {

				foreach(GameObject scout in scouts) {
					scout.SendMessage("returnHome");

				}	
			}
*/

	}

	void CheckKeyPress() {

		if (Input.GetKey (KeyCode.E)) {
			Debug.Log ("E");



			GameObject target = getHitTarget();
			
			Debug.Log (target.tag);
			if (target != null) {
				GameObject[] scouts = GameObject.FindGameObjectsWithTag("scout");

				foreach(GameObject scout in scouts) {
					scout.SendMessage("seek", target);				
					
				}				
			}
			
		}
		
		
		if (Input.GetKey (KeyCode.H)) {
//			foreach(GameObject scout in scouts) {
//				//scout.SendMessage("seek", target);				
//				
//			}
		}
		
		if (Input.GetKey (KeyCode.T)) {
			GameObject[] c = GameObject.FindGameObjectsWithTag("navSpot");
			
			
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Friend")) {
				//scout.SendMessage("seek", target);				
				g.SendMessage("setTarget", c[0]);
			}
		}
		
		if (Input.GetKey (KeyCode.F)) {
			
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Weapon")) {
				//scout.SendMessage("seek", target);				
				g.SendMessage("Fire");
			}
		}
		
		if (Input.GetKey (KeyCode.Z)) {
			GameObject c = GameObject.FindGameObjectWithTag ("Enemy");
			
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Weapon")) {
				//scout.SendMessage("seek", target);				
				g.SendMessage("SetTarget", c);
			}
		}
		
		
		if (Input.GetKey (KeyCode.B)) {
			callBattleStations();
			
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
						
			foreach(GameObject g in GameObject.FindGameObjectsWithTag("Weapon")) {
				//scout.SendMessage("seek", target);				
				g.SendMessage("StepLeft");
			}
		}
		
		if (Input.GetKey (KeyCode.C)) {
			//GameObject g = GameObject.FindGameObjectWithTag ("Enemy");
			//GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
			
//			foreach(GameObject scout in scouts) {
//				//	scout.SendMessage("circle", g);				
//			}	
			
		}
	}
	void callBattleStations() {
		//crewManAi.Team[] teams = (crewManAi.Team[]) System.Enum.GetValues ( typeof (crewManAi.Team));

//		foreach (crewManAi.Team t in  teams) {
//
//		}
		foreach (GameObject station in GameObject.FindGameObjectsWithTag("navSpot")) {
			//stationAi s = station.GetComponent<stationAi>();
//			if (station.GetComponent<stationAi>().assignedCrewman == null) {
//				//Debug.Log ("emptystation");
				//station.SendMessage("FindCrewman");
			station.SendMessage("FindCrewman");

//			}

		}

	}

	//periodically check stations for crew requests, assign sequentially
	void HandleCrewmanRequests() {
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("navSpot")) {
			stationAi s = g.GetComponent<stationAi>();

			Debug.Log (s);
			if (s.Status == stationAi.StationStatus.RequestingCrew) {
				GameObject crewMan = FindAvailableCrewman(s.preferredTeam);

				if (crewMan != null) {
					g.SendMessage("AssignCrewman", crewMan);

				}
			}
		}
	}

	GameObject FindAvailableCrewman(crewManAi.Team preferredTeam) {
		
		//GameObject selectedCrewman = null;
		
		//find crewman on preferred team and free
		foreach (GameObject c in GameObject.FindGameObjectsWithTag("Friend")) {
			crewManAi crewAi = c.GetComponent<crewManAi> ();
			
			if (crewAi.currentTeam == preferredTeam && crewAi.currentStatus == crewManAi.Status.Free ) {
				Debug.Log (crewAi.currentTeam);
				return c;
			}
		}
		
		//if none above, find crewman on preferred team and not at battlestation
		foreach (GameObject c in GameObject.FindGameObjectsWithTag("Friend")) {
			crewManAi crewAi = c.GetComponent<crewManAi> ();
			
			//if crewman is preferred team and is available (on low-status duty)
			if (crewAi.currentTeam == preferredTeam && crewAi.currentStatus != crewManAi.Status.AssignedStation ) {
				
				return c;
			}
		}
		
		//if none above, find anyone free
		foreach (GameObject c in GameObject.FindGameObjectsWithTag("Friend")) {
			crewManAi crewAi = c.GetComponent<crewManAi> ();
			
			//if crewman is preferred team and is available (on low-status duty)
			if (crewAi.currentStatus != crewManAi.Status.AssignedStation ) {
				
				return c;
			}
		}
		
		//no available crewmen
		Debug.Log ("no crewman found!");
		return null;
	}
}



