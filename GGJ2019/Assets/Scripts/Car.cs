using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Car : MonoBehaviour {
    public float Speed = 10.0f;
    public float StartSpeed = 5;
    public float a = 2;
    public bool moveing = false;
    public CinemachineDollyCart dollyCart;
    float Length = 5;
    int layerMask = 1 << 8;
    public float cd = 0;
    public Transform RayStartPoint;
    [Header("車停滯幾秒後回收")]
    public float seconds =  5 ;
    // Use this for initialization
    void Start () {
        Speed = 20;
        Length = 5;
        a = Random.Range(1.0f, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if(dollyCart.m_Speed < 1)
        {
            cd += Time.deltaTime;
            if (cd >= seconds)
            {
                //CarManager.instance.Recovery(this);
                cd = 0;
            }
        }
        Ray ray = new Ray(RayStartPoint.position, transform.forward * Length);
        RaycastHit hitInfo;
        Debug.DrawRay(RayStartPoint.position, transform.forward * Length, Color.green);
        if (Physics.Raycast(ray, out hitInfo,Length, layerMask))
        {
            dollyCart.m_Speed = Speed * (hitInfo.distance / Length) ;
        }
        else {
            //float RandomSpeed = Random.Range(-(Speed / 2), Speed + (Speed/2));
            dollyCart.m_Speed = StartSpeed; //RandomSpeed+Speed;  
            if (StartSpeed < Speed)
            {
                StartSpeed += a;
            }
        }
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Block")
        {
            CarManager.instance.Recovery(this);
        }
    }
    
    //IEnumerator Fade(float DSpeed)
    //{
    //    lock (lockObject)
    //    {

    //        float initSpeed = dollyCart.m_Speed;
    //        if (initSpeed != DSpeed)
    //        {
    //            if (initSpeed < DSpeed)
    //            {
    //                while (initSpeed < DSpeed)
    //                {
    //                    initSpeed += Time.deltaTime;
    //                    yield return new WaitForFixedUpdate();
    //                }
    //            }
    //            else
    //            {
    //                while (initSpeed > DSpeed)
    //                {
    //                    initSpeed -= Time.deltaTime;
    //                    yield return new WaitForFixedUpdate();
    //                }
    //            }
    //            yield break;
    //        }
    //    }

    //}


}
