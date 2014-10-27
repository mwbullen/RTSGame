using UnityEngine;
using System.Collections;

public class scoutAi : MonoBehaviour {

	Vector3 startPosition;

	public NavMeshAgent navMeshAgent;

	GameObject navPlane;

	public float crusingAltitude;
	public float thrustInterval;

	public float vertThrustForce;

	float timeSinceThrust;

	Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;

		navMeshAgent = GetComponent<NavMeshAgent>();

		navPlane = GameObject.FindGameObjectWithTag ("Environment");
		Debug.Log (navPlane);
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceThrust += Time.deltaTime;
	}

	void FixedUpdate() {
		//Debug.Log (transform.position.y);
		if (transform.position.y < crusingAltitude && timeSinceThrust >= thrustInterval) {
			thrust ();
			timeSinceThrust = 0;
		}

		if (targetPosition != transform.position && timeSinceThrust >= thrustInterval) {
			rigidbody.AddForce (Vector3.forward * vertThrustForce, ForceMode.Force);
			Vector3.RotateTowards (transform.position, targetPosition, 360, 0);
				}

		Debug.Log (targetPosition);
	}

	void returnHome() {
		//navMeshAgent.Stop ();

		//navMeshAgent.SetDestination (startPosition);
		//navMeshAgent.baseOffset = .5f;

		moveTo (startPosition);

	}


	void setTarget(GameObject g) {

		moveTo (g.transform.position);

		}

	void moveTo(Vector3 position) {
		//transform.position = new Vector3 (transform.position.x, navPlane.transform.position.y, transform.position.z);

		/*
		navMeshAgent.Stop ();
		navMeshAgent.baseOffset = Random.Range (.5f, 20f);
		
		navMeshAgent.SetDestination (g.transform.position);
		*/
	
		targetPosition = position;


	}

	void thrust() {
		rigidbody.AddForce (Vector3.up * vertThrustForce, ForceMode.Impulse);
		Debug.Log ("thrust");
		}

	void Launch() {
		//remove parent/child relationship to dock
		}
}
