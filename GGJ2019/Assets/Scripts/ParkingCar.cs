using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ParkingCar : Car {
    
    public CinemachinePath NextPath;
    public CinemachinePath originPath;
   
    float Length= 5;
    public bool changePathalready = false;
   
	// Update is called once per frame
	void Update () {
        
        if (!moveing && !changePathalready)
        {
            Ray ray = new Ray(transform.position + transform.up + (transform.forward * 3), transform.forward * Length);
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position + transform.up + (transform.forward * 3), transform.forward * Length, Color.green);
            if (Physics.Raycast(ray, out hitInfo, 10))
            {
                print("hit");
                if (hitInfo.collider.tag == "People" || hitInfo.collider.tag == "Car")
                {
                    print("People");
                    dollyCart.m_Speed = 10 * hitInfo.distance / Length;
                    
                   
                }
                else
                {
                   
                }
            }
            else
            {
            }
        }

    }
   
    //IEnumerator Fade(float DSpeed)
    //{
    //    lock (lockObject)
    //    {
    //        float initSpeed = dollyCart.m_Speed;
    //        if (initSpeed < DSpeed)
    //        {
    //            while (initSpeed < DSpeed)
    //            {
    //                initSpeed += Time.deltaTime;
    //                dollyCart.m_Speed = initSpeed;
    //                yield return new WaitForFixedUpdate();
    //            }
    //        }
    //        else
    //        {
    //            while (initSpeed > DSpeed)
    //            {
    //                initSpeed -= Time.deltaTime;
    //                dollyCart.m_Speed = initSpeed;
    //                yield return new WaitForFixedUpdate();
    //            }
    //            if (DSpeed == 0)
    //                dollyCart.m_Speed = 0;
    //        }
    //        yield break;
    //    }
       
    //}
    
   
}
