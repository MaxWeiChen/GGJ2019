using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ParkingCar : Car {
    private Rigidbody parkingcarRigidbody;
    public CinemachinePath NextPath;
    public CinemachinePath originPath;
    object lockObject = new object();

    public bool changePathalready = false;
    void Start () {
      
        parkingcarRigidbody = GetComponent<Rigidbody>();
        parkingcarRigidbody.useGravity = false;
        RecyclePosition = 180;
    }
	
	// Update is called once per frame
	void Update () {
        if(dollyCart.m_Position >= RecyclePosition && !changePathalready)
        {
            print("倒車");
            changePathalready = true;
            StartCoroutine(RBack());
        }
        if (!moveing && !changePathalready)
        {
            Ray ray = new Ray(transform.position + transform.up + (transform.forward * 3), transform.forward * 5);
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position + transform.up + (transform.forward * 3), transform.forward * 5, Color.green);
            if (Physics.Raycast(ray, out hitInfo, 10))
            {
                if (hitInfo.collider.tag == "Human" || hitInfo.collider.tag == "Car")
                {
                    print(Vector3.Distance(transform.position, hitInfo.point));

                    dollyCart.m_Speed = 10 - (10 * Vector3.Distance(transform.position, hitInfo.point) / 12f);
                    moveing = true;
                    Invoke("ContinueMove", 1);
                }
                else
                {
                    print(hitInfo.collider.name);
                    Vector3 force = transform.forward * Speed * Time.deltaTime;
                    parkingcarRigidbody.AddForce(force, ForceMode.Force);
                    Fade(10);
                }
            }
            else
            {
                Vector3 force = transform.forward * Speed * Time.deltaTime;
                parkingcarRigidbody.AddForce(force, ForceMode.Force);
                Fade(10);
            }
        }

    }
    void ContinueMove()
    {
        Fade(10);
        moveing = false;
    }
    
    IEnumerator Fade(float DSpeed)
    {
        lock (lockObject)
        {
            float initSpeed = dollyCart.m_Speed;
            if (initSpeed < DSpeed)
            {
                while (initSpeed < DSpeed)
                {
                    initSpeed += Time.deltaTime;
                    dollyCart.m_Speed = initSpeed;
                    yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                while (initSpeed > DSpeed)
                {
                    initSpeed -= Time.deltaTime;
                    dollyCart.m_Speed = initSpeed;
                    yield return new WaitForFixedUpdate();
                }
                if (DSpeed == 0)
                    dollyCart.m_Speed = 0;
            }
            yield break;
        }
       
    }
    
    IEnumerator RBack()
    {
       
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        dollyCart.m_Speed = -5;


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
                    Fade(0);
                    moveing = true;
                    Invoke("ContinueMove", 1);
                }
                else
                {
                    Fade(10);
                }
            }
            yield return new WaitForFixedUpdate();
        }
        dollyCart.m_Path = originPath;
        dollyCart.m_Position = 0;
        Fade(10);
        changePathalready = false;
        yield break;
    }


}
