using UnityEngine;
using System.Collections;

public class gameControl : MonoBehaviour {

	public GameObject Scout;

	// Use this for initialization
	void Start () {
		GameObject[] landingPads = GameObject.FindGameObjectsWithTag ("Respawn");

		foreach (GameObject g in landingPads) {
			//Instantiate(Scout, g.transform.position, g.transform.rotation);
				}
	}
	
	// Update is called once per frame
	void Update () {
	
			if (Input.GetKey (KeyCode.H)) {
				GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");

				foreach(GameObject scout in scouts) {
					scout.SendMessage("returnHome");

				}	
			}

		if (Input.GetKey (KeyCode.A)) {
			GameObject g = GameObject.FindGameObjectWithTag ("Enemy");
			GameObject[] scouts = GameObject.FindGameObjectsWithTag("Scout");
			
			foreach(GameObject scout in scouts) {
				scout.SendMessage("setTarget", g);
				
			}	
		}
	}
}