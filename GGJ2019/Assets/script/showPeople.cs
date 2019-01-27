using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showPeople : MonoBehaviour {
	public GameObject[] sp ;
	public static showPeople SP;
	void Awake(){
		SP = this;			
	}

	public void showpeo(int num){
		sp[num].SetActive(true) ;
	}
}
