using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people_hit : MonoBehaviour {
	public createAMA CA;
	public int num ;
	private bool jump = false ;

	// Use this for initialization
	void Start () {
		//StartCoroutine(startwait ()) ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void hit(){
		if (!jump && CA.getAns() == num) {
			jump = true ;
			print (gameObject.name);
			Rigidbody rig = gameObject.GetComponent<Rigidbody> ();
			rig.AddForce(Vector3.up * 200.0f);
			StartCoroutine(newPeople ()) ;
		}
	}
	IEnumerator newPeople(){ 
		yield return new WaitForSeconds(1.0f); // 等待x秒
		CA.setAns() ;
		jump = false ;
	}

}
