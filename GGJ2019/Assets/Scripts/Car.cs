using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Car : MonoBehaviour {
    public float Speed = 40.0f;
    public bool moveing = false;
    private Rigidbody carRigidbody;
    public CinemachineDollyCart dollyCart;
    
    public float RecyclePosition = 860;//1014;
    // Use this for initialization
    void Start () {
        carRigidbody = GetComponent<Rigidbody>();
        carRigidbody.useGravity = false;
       
       
    }
	
	// Update is called once per frame
	void Update () {
        if(dollyCart.m_Position >= RecyclePosition)
        {
            CarManager.instance.Recovery(gameObject);
            print("回收");
        }
        if (!moveing)
        {
            Ray ray = new Ray(transform.position + transform.up + (transform.forward * 3), transform.forward *5);
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position + transform.up + (transform.forward * 3), transform.forward * 5, Color.green);
            if (Physics.Raycast(ray, out hitInfo,10))
            {
                if (hitInfo.collider.tag == "Human" || hitInfo.collider.tag == "Car")
                {
                    print(hitInfo.collider.name);
                   
                    dollyCart.m_Speed = 0;
                    moveing = true;
                    Invoke("ContinueMove", 1);
                }
                else
                {
                    print(hitInfo.collider.name);
                    Vector3 force = transform.forward * Speed * Time.deltaTime;
                    carRigidbody.AddForce(force, ForceMode.Force);
                    dollyCart.m_Speed = 10;
                }
            }
            else
            { 
                Vector3 force = transform.forward * Speed * Time.deltaTime;
                carRigidbody.AddForce(force, ForceMode.Force);
                dollyCart.m_Speed = 10;
            }
        }

    }
   void ContinueMove()
    {
        moveing = false;
        dollyCart.m_Speed = 10;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Block")
        {
            CarManager.instance.Recovery(gameObject);
        }
    }

   
}
