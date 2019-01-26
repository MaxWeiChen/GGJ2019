using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAMA : MonoBehaviour {
	public GameObject[] people ; 
	private int AMA_Ans ; 
	private GameObject cam ;



	void Start () {
		AMA_Ans = UnityEngine.Random.Range( 0, 3) ;
		cam = gameObject.transform.GetChild(AMA_Ans).gameObject.transform.GetChild(1).gameObject ;
		cam.SetActive(true);
		//people [AMA_index].SetActive (true) ;

		//print("start") ;
	}
	

	void Update () {

	}

	public void setAns(){
		cam.SetActive(false);
		int temp = UnityEngine.Random.Range (0, 3);
		while(AMA_Ans == temp){
			temp = UnityEngine.Random.Range (0, 3);
		}
		AMA_Ans = temp;
		cam = gameObject.transform.GetChild(AMA_Ans).gameObject.transform.GetChild(1).gameObject ;
		cam.SetActive(true);
		//print (cam.name);

	}
	//transform.position = new Vector3 (transform.position.x + 3 , transform.position.y , transform.position.z);
	/*
	void setType(){
		people [AMA_Ans].SetActive (false) ;
		int temp = UnityEngine.Random.Range (0, 3);
		while(AMA_Ans == temp){
			temp = UnityEngine.Random.Range (0, 3);
		}
		AMA_Ans = temp;
		people [AMA_Ans].SetActive (true) ;
		print ("change");
	}*/
	/*	
	public void check(int num){
		if (!jump && AMA_Ans == num) {
			jump = true ;
			print (gameObject.name);
			Rigidbody rig = gameObject.GetComponent<Rigidbody> ();
			rig.AddForce(Vector3.up * 200.0f);
			StartCoroutine(waittime ()) ;
		}
	}
	*/
	public int getAns(){
		return  AMA_Ans ;
	}

}
