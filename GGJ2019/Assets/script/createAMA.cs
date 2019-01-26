using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAMA : MonoBehaviour {
	public static createAMA instance;
	public GameObject[] people ; 
	private int nowPeopleNum = 0 ;
	private int AMA_Ans ; 
	private GameObject cam ;


	void Awake(){
		instance = this;			
	}

	void Start () {
			
		AMA_Ans = UnityEngine.Random.Range( 0, people.Length ) ;
		//print (AMA_Ans);
		cam = gameObject.transform.GetChild(AMA_Ans).gameObject.transform.GetChild(1).gameObject ;
		cam.SetActive(true);
		//people [AMA_index].SetActive (true) ;

		//print("start") ;
	}
	

	void Update () {

	}

	public void setAns(){
		cam.SetActive(false);
		int temp = UnityEngine.Random.Range (0, people.Length);
		//GameObject go = gameObject.transform.GetChild(temp).gameObject.GetComponent<people_hit>().getBefound() ;
		//print ("test" + go.name);
		//people_hit ph = go.GetComponent<people_hit> ();
		while(gameObject.transform.GetChild(temp).gameObject.GetComponent<people_hit>().getBefound()){
			temp = UnityEngine.Random.Range (0, people.Length);
			//go = gameObject.transform.GetChild(temp).gameObject ;
			//ph = go.GetComponent<people_hit> ();
		}
		AMA_Ans = temp;
		//print (AMA_Ans);
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
	public bool checkEnd(){
		if ((++nowPeopleNum) >= people.Length){
			print (nowPeopleNum);
			return true; 
		}else{print (nowPeopleNum);
		
			return false;
		}
	}

}
