using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExpoler : MonoBehaviour {

	public Transform Camera;
	public float speed;
	public float RayCastLengh;
	public LayerMask mask;
	public float hitDis;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		

		

	}

	void FixedUpdate()
	{
		Vector3 fwd = Camera.TransformDirection(Vector3.forward);
		RaycastHit hit;


		if (Input.GetMouseButton (1)) {



			if (Physics.Raycast (Camera.position, fwd, out hit, RayCastLengh, mask)) {
			
				hitDis = Vector3.Distance (Camera.position, hit.point);
				transform.position += Camera.forward.normalized * Time.deltaTime * speed * (hitDis / RayCastLengh);

			} else {
		
				transform.position += Camera.forward.normalized * Time.deltaTime * speed;
		
			}

		}




		Debug.DrawRay(Camera.position,fwd,Color.green,RayCastLengh);
	}
}
