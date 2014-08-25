using UnityEngine;
using System.Collections;

public class crewManAi : MonoBehaviour {

	public enum Team
		{
			None, Mechanic, Pilot, Soldier, Gunner
		}

	public enum Status
		{
			AssignedStation, Free
		}


	public Status currentStatus = Status.Free;

	public Team currentTeam = Team.None;

	public GameObject currentTarget;

	public Material Mechanic_Material;
	public Material Pilot_Material;
	public Material Soldier_Material;
	public Material Gunner_Material;

	public float MechanicLevel = 0;
	public float PilotLevel = 0;
	public float GunnerLevel = 0;
	public float SoldierLevel= 0;


	public GameObject body;

	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = gameObject.GetComponent<NavMeshAgent>();

		switch (currentTeam) {		
		case Team.Mechanic:
			body.renderer.material = Mechanic_Material;
			break;

		case Team.Pilot:
			body.renderer.material = Pilot_Material;
			break;

		case Team.Gunner:
			body.renderer.material = Gunner_Material;
			break;

		case Team.Soldier:
			body.renderer.material = Soldier_Material;
			break;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTarget != null && navAgent.remainingDistance == 0) {
			//currentTarget.SendMessage("Arrived");
		}
	}

	void setTarget(GameObject g) {
		currentTarget = g;
		navAgent.SetDestination (g.transform.position);
		currentStatus = Status.AssignedStation;
		}
}
