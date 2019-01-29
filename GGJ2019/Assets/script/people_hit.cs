using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class people_hit : MonoBehaviour {
	public createAMA CA;
	//public showPeople SP ;
	public int num ;
	private bool jump = false ;
	private bool befound = false ;
    public FindManager findManager;
    public bool find = false;
	// Use this for initialization
	void Start () {
        //StartCoroutine(startwait ()) ;
        if (!findManager)
        {
            findManager =Camera.main.GetComponent<FindManager>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!findManager)
        {
            findManager = Camera.main.GetComponent<FindManager>();
        }
    }
	void hit(){
		if (!jump && CA.getAns() == num) {
            print("打");
            GameObject.Find("Main Camera").GetComponent<GameCtrl>().AddAMA();
			jump = true ;
			befound = true ;
			showPeople.SP.showpeo(num);
			Rigidbody rig = gameObject.GetComponent<Rigidbody> ();
			rig.AddForce(Vector3.up * 200.0f);
            findManager.RecycleImage(); //圖片隱藏 重新計算秒數
            CA.setAns();
            findManager.GrandMaCube = CA.getAnsObject();//生成新AMA放入腳本中對應
            Invoke("CloseJump", 1);
           
         
        }
	}

	void CloseJump(){
        jump = false;
    }

	public bool getBefound(){
		return befound;
	}
}
