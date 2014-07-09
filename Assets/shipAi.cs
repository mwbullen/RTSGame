using UnityEngine;
using System.Collections;

public class shipAi : MonoBehaviour {

	GameObject currentTarget;
	Vector3 currentTargetPosition;

	public float maxSpeed;
	float speed = 1;
	public float acceleration;

	public float collisionDetectDistance = 20;
	// Use this for initialization

	public Material avoidMaterial;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//if (currentTargetPosition != null) {

		checkForPendingCollision ();

			transform.LookAt (currentTargetPosition);


			if (speed < maxSpeed){
				speed += acceleration;
				
			}
			
			transform.Translate(Vector3.forward * Time.deltaTime * speed);		
		
		//}
				
		

		if (currentTarget != null) {

	}
			//transform.rotation.lo
		/*
		  var rotation = Quaternion.LookRotation(target.position - transform.position);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    */


	}


	void setTarget(GameObject g) {
		currentTarget = g;
		currentTargetPosition = g.transform.position;
			
	}

	void checkForPendingCollision() {
		RaycastHit r;

		Debug.Log ("Check");

		if (Physics.Raycast(new Ray(transform.position, transform.forward), out r, collisionDetectDistance)) {
			//transform.Rotate(
			Debug.Log ("Collision detected");

			transform.renderer.material = avoidMaterial;

			float y = r.collider.bounds.size.y;

			currentTargetPosition = r.collider.bounds.max;

		}
	}
}
