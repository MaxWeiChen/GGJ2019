using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour {
    public float timer = 30;
    public float count = 30;
    public Collider[] colliders;
    public int index = 0;
 	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= count)
        {
            colliders[index].enabled = false;
            //換
            if (index == 0)
            {
                index = 1;
                colliders[index].enabled = true;
            }
            else
            {
                index = 0;
                colliders[index].enabled = true;
            }
            timer = 0;
        }
	}
}
