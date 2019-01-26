using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ParkingCar : Car {
    private Rigidbody parkingcarRigidbody;
    public CinemachinePath NextPath;
    public CinemachinePath originPath;
    public bool changePathalready = false;
    void Start () {
      
        parkingcarRigidbody = GetComponent<Rigidbody>();
        parkingcarRigidbody.useGravity = false;
        RecyclePosition = 184;
    }
	
	// Update is called once per frame
	void Update () {
        if(dollyCart.m_Position >= RecyclePosition && !changePathalready)
        {
            changePathalready = true;
            dollyCart.m_Speed = -10;
            StartCoroutine(RBack());
        }
        if (!moveing && !changePathalready)
        {
            Ray ray = new Ray(transform.position + transform.up + (transform.forward * 3), transform.forward * 5);
            RaycastHit hitInfo = new RaycastHit() ;
            Debug.DrawRay(transform.position + transform.up + (transform.forward*3), transform.forward * 5, Color.green);
            if (Physics.Raycast(ray, out hitInfo, 10))
            {
                if (hitInfo.collider.tag == "Human" || hitInfo.collider.tag == "Car")
                {
                   
                    dollyCart.m_Speed = 0;
                    moveing = true;
                    Invoke("ContinueMove", 2);

                }
                else
                {
                    print(hitInfo.collider.name);
                    Vector3 force = transform.forward * Speed * Time.deltaTime;
                    parkingcarRigidbody.AddForce(force, ForceMode.Force);
                    dollyCart.m_Speed = 10;
                }
            }
            else
            { 
                Vector3 force = transform.forward * Speed * Time.deltaTime;
                parkingcarRigidbody.AddForce(force, ForceMode.Force);
                dollyCart.m_Speed = 10;
            }
        }

    }
    void ContinueMove()
    {
        dollyCart.m_Speed = 10;
        moveing = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Block")
        {
            CarManager.instance.Recovery(gameObject);
        }
    }
    IEnumerator RBack()
    {
        print("cc");
        dollyCart.m_Speed = 0;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        dollyCart.m_Speed = -10;
        while (dollyCart.m_Position > 174)
        {
            yield return new WaitForSeconds(.3f);
        }
        
        dollyCart.m_Path = NextPath;
        dollyCart.m_Position = 0;
        dollyCart.m_Speed = 10;
        while (dollyCart.m_Position < 275)
        {
            Ray ray = new Ray(transform.position, transform.forward * 30);
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == "Human" || hitInfo.collider.tag == "Car")
                {
                    print("前面有車");
                    dollyCart.m_Speed = 0;
                    moveing = true;
                    Invoke("ContinueMove", 1);
                }
                else
                {
                    dollyCart.m_Speed = 10;
                }
            }
            yield return new WaitForFixedUpdate();
        }
        dollyCart.m_Path = originPath;
        dollyCart.m_Position = 0;
        dollyCart.m_Speed = 10;
        changePathalready = false;
        yield break;
    }


}
