using UnityEngine;
using System.Collections;

public class crewManAi : MonoBehaviour {

	private NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
		navAgent = gameObject.GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setTarget(GameObject g) {
		navAgent.SetDestination (g.transform.position);

		}
}
