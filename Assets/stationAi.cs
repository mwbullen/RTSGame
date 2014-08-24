using UnityEngine;
using System.Collections;

public class stationAi : MonoBehaviour {
	public crewManAi.Team preferredTeam;

	//status
	private GameObject assignedCrewman;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	GameObject findCrewman() {

		GameObject returnCrewman = null;


		foreach (GameObject c in GameObject.FindGameObjectsWithTag("Friend")) {
			crewManAi crewAi = c.GetComponent<crewManAi> ();

			//if crewman is preferred team and is available (on low-status duty)
			if (crewAi.currentTeam == preferredTeam && crewAi.currentStatus != crewManAi.Status.AtBattleStation ) {

				if (returnCrewman != null) {
				switch (preferredTeam) {
					case crewManAi.Team.Gunner:
						if (crewAi.GunnerLevel > returnCrewman.GetComponent<crewManAi>().GunnerLevel) {
							returnCrewman = c;							
						}
					}						    
				} else {
					returnCrewman = c;

				}
				returnCrewman = c;
			}
		}
		return returnCrewman;
	}
}
