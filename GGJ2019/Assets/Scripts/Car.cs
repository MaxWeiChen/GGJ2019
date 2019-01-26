using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Car : MonoBehaviour {
    public float Speed = 40.0f;
    public bool moveing = false;
    private Rigidbody carRigidbody;
    public CinemachineDollyCart dollyCart;
    object lockObject = new object();

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
            CarManager.instance.Recovery(this);
           
        }
        if (!moveing)
        {
            Ray ray = new Ray(transform.position + transform.up + (transform.forward * 3), transform.forward *5);
            RaycastHit hitInfo;
            Debug.DrawRay(transform.position + transform.up + (transform.forward * 3), transform.forward * 5, Color.green);
            if (Physics.Raycast(ray, out hitInfo,10))
            {
                if (hitInfo.collider.tag == "People" || hitInfo.collider.tag == "Car")
                {
                    dollyCart.m_Speed = 10 - (10 * Vector3.Distance(transform.position, hitInfo.point) / 12f);
                    moveing = true;
                    Invoke("ContinueMove", 1);
                }
                else
                {
                    print(hitInfo.collider.name);
                    Vector3 force = transform.forward * Speed * Time.deltaTime;
                    carRigidbody.AddForce(force, ForceMode.Force);
                   
                }
            }
            else
            { 
                Vector3 force = transform.forward * Speed * Time.deltaTime;
                carRigidbody.AddForce(force, ForceMode.Force);
                Fade(10);
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
            CarManager.instance.Recovery(this);
        }
    }
    IEnumerator Fade(float DSpeed)
    {
        lock (lockObject)
        {

            float initSpeed = dollyCart.m_Speed;
            if (initSpeed != DSpeed)
            {
                if (initSpeed < DSpeed)
                {
                    while (initSpeed < DSpeed)
                    {
                        initSpeed += Time.deltaTime;
                        yield return new WaitForFixedUpdate();
                    }
                }
                else
                {
                    while (initSpeed > DSpeed)
                    {
                        initSpeed -= Time.deltaTime;
                        yield return new WaitForFixedUpdate();
                    }
                }
                yield break;
            }
        }

    }


}
