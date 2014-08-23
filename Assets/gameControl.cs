using UnityEngine;
using System.Collections;

public class gameControl : MonoBehaviour {

	public GameObject Scout;

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

		GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
//		GameObject g = GameObject.FindGameObjectWithTag ("Enemy");
			
		/*if (Input.GetKey (KeyCode.H)) {

				foreach(GameObject scout in scouts) {
					scout.SendMessage("returnHome");

				}	
			}
*/
		if (Input.GetKey (KeyCode.E)) {
			Debug.Log ("E");

			GameObject target = getHitTarget();

			Debug.Log (target.tag);
			if (target != null) {
			foreach(GameObject scout in scouts) {
				scout.SendMessage("seek", target);				
			
				}				
			}

		}


		if (Input.GetKey (KeyCode.H)) {
			foreach(GameObject scout in scouts) {
				//scout.SendMessage("seek", target);				
				
			}
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

		if (Input.GetKey (KeyCode.C)) {
			//GameObject g = GameObject.FindGameObjectWithTag ("Enemy");
			//GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
			
			foreach(GameObject scout in scouts) {
			//	scout.SendMessage("circle", g);				
			}	


		}
	}
}