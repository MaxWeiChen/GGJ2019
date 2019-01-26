using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAMA : MonoBehaviour {
	public GameObject[] people ; 
	private int AMA_index = 0 ; 
	private bool jump = false ;
	private bool once = false ;

	// Use this for initialization
	void Start () {
		AMA_index = UnityEngine.Random.Range( 0, 3) ;
		people [AMA_index].SetActive (true) ;
		//setPosion ();
		print("start") ;
	}
	
	// Update is called once per frame
	void Update () {
		if(jump){
			//transform.position = transform.position + new Vector3(0, 0.05f, 0);
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.049f, transform.position.z);
			//Mathf.Clamp(transform.position.y + 0.5f, 0.5f, 1.0f)
			//jump = false;
			//gameObject.SetActive (false) ;
			print (transform.position.y);
			if (transform.position.y >= 1.0f) {
				jump = false;
				StartCoroutine(waittime ()) ;
			}
		}
	}



	void setType(){
		people [AMA_index].SetActive (false) ;
		AMA_index = UnityEngine.Random.Range( 0, 3) ;
		people [AMA_index].SetActive (true) ;
		print ("change");
	}

	void setPosion(){

	}

	void OnMouseDown(){
		if (!jump) {
			jump = true ;
			//once = true ;
		}
	}
		
	IEnumerator waittime(){ 
		yield return new WaitForSeconds(0.2f); // 等待x秒
		setType() ;
	}

}
