using UnityEngine;
using System.Collections;

public class stationAi : MonoBehaviour {
	public crewManAi.Team preferredTeam;

	//status
	public enum StationStatus {Manned, CrewEnRoute, RequestingCrew, Idle};

	public StationStatus Status = StationStatus.Idle;

	private GameObject assignedCrewman;

	//public GameObject activeCrewman;

	//public crewManAi.Status stationType;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Status == StationStatus.RequestingCrew) {
//			RequestCrewman();				
//		}
	}

	void AssignCrewman(GameObject g) {
		assignedCrewman = g;
		g.SendMessage ("setTarget", gameObject);
		Status = StationStatus.CrewEnRoute;
	}
//	void RequestCrewman() {
//		
//		assignedCrewman = FindAvailableCrewman();
//		
//		if (assignedCrewman != null) {
//			Debug.Log ("Found crewman!");
//			assignedCrewman.SendMessage("setTarget", gameObject);
//			Status = StationStatus.Manned;
//		}
//	}
//
	void FindCrewman() {
		if (assignedCrewman == null) {
			Status = StationStatus.RequestingCrew;
		}
	}

	void CrewManArrived () {
		Status = StationStatus.Manned;
	}

}
