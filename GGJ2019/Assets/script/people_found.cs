using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people_found : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
		{
			//Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
			if(hit.collider.tag == "People"){
                
				hit.collider.SendMessage("hit");
			}
			//print ("hi " + hit.collider.name);
		}
	}
}
