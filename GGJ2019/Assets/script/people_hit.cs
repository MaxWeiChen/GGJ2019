using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people_hit : MonoBehaviour {
	public createAMA CA;
	public int num ;
	private bool jump = false ;
	private bool befound = false ;

	// Use this for initialization
	void Start () {
		//StartCoroutine(startwait ()) ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void hit(){
		if (!jump && CA.getAns() == num) {
            //GameObject ama = GameObject.Find("Main Camera");
            GameObject.Find("Main Camera").GetComponent<GameCtrl>().AddAMA();
			jump = true ;
			befound = true ;


			Rigidbody rig = gameObject.GetComponent<Rigidbody> ();
			rig.AddForce(Vector3.up * 200.0f);
			if(!CA.checkEnd()){
				StartCoroutine(newPeople ()) ;
			}
		}
	}

	IEnumerator newPeople(){ 
		yield return new WaitForSeconds(1.0f); // 等待x秒
		CA.setAns() ;
		jump = false ;
	}

	public bool getBefound(){
		return befound;
	}
}
