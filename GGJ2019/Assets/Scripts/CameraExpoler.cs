using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraExpoler : MonoBehaviour {

	public Transform Camera;
	public float m_Speed;
	public float RayCastLengh;
	public LayerMask mask;
	public float hitDis;
	public CinemachineDollyCart Dolly;
	public float position;
	public float OutSide;
	public Rect rect ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		rect = new Rect (new Vector2(Screen.width/2,Screen.height/2),new Vector2(Screen.width-OutSide,Screen.height-OutSide));

		if (rect.Contains(Input.mousePosition))
			Debug.Log("Inside");
	
	



			Dolly.m_Position = position;
	

	}





	public Texture myTexture;



}
