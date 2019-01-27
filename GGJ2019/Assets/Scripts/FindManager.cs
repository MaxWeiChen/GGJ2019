using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FindManager : MonoBehaviour {
    [Header("當前阿嬤ID")]
    public int correctID;
    [Header("提示出現秒數")]
    public float alertEventRate = 10.0f;
    [Header("累計秒數")]
    public float timer = 0;
    public GameObject GrandMaCube;
    Camera cam;
    public Canvas canvas;
    public float placeDistance = 0;
    [Header("箭頭誤差X")]
    public float fixedX  = 0 ;
    [Header("箭頭誤差Y")]
    public float fixedY = 20;
    Vector3 fixedVector2;
    [Header("箭頭圖片")]
    public GameObject target;
    GameObject arrow;
    public bool showArrowTarget = false;
    public float during = 4.0f;

    public GameObject outLineTarget; 
    // Use this for initialization
    void Start () {
        outLineTarget = Instantiate(target);
        outLineTarget.gameObject.SetActive(false);
        outLineTarget.transform.SetParent(canvas.transform);
        arrow = Instantiate(target);
        arrow.gameObject.SetActive(false);
        
        arrow.transform.SetParent(canvas.transform);
        cam = Camera.main;
        fixedVector2 = new Vector3(fixedX, fixedY, placeDistance);

        GrandMaCube = createAMA.instance.getAnsObject();
    }
    // Update is called once per frame
    void FixedUpdate () {
        if (GrandMaCube != null)
        {
            //createAMA.instance.gameObject.transform.GetChild (AMA_Ans).gameObject;
            timer += Time.deltaTime;
            Vector3 screenPos = cam.WorldToScreenPoint(GrandMaCube.transform.position);
           
            if (!showArrowTarget)
            {

            }
            else
            {

                //GrandMaCube.transform.position;
                // Debug.Log("target is " + screenPos);
                arrow.GetComponent<RectTransform>().position = screenPos + fixedVector2;
                ////print(canvas.GetComponent<RectTransform>().sizeDelta);
                //if (Mathf.Abs(screenPos.x) < (canvas.GetComponent<RectTransform>().sizeDelta.x / 2.0f) && Mathf.Abs(screenPos.y)< (canvas.GetComponent<RectTransform>().sizeDelta.y / 2.0f))
                //{
                //    Debug.Log(" CANVAS width :" + canvas.GetComponent<RectTransform>().sizeDelta.x + ", " + " CANVAS height :" + canvas.GetComponent<RectTransform>().sizeDelta.y);
                //    arrow.GetComponent<RectTransform>().position = screenPos + fixedVector2;
                //    outLineTarget.SetActive(false);
                //}
                //else
                //{
                //    float angle = Mathf.Atan2(screenPos.x, screenPos.y) * Mathf.Rad2Deg;
                //    // Debug.Log("角度" + angle);
                //    if (screenPos.x < 0)
                //    {
                //        Debug.Log("左");
                //    }
                //    else if (screenPos.x > 0)
                //    {
                //        Debug.Log("又");
                //    }
                //    if (screenPos.y < 0)
                //    {
                //        Debug.Log("下");
                //    }
                //    else if (screenPos.y > 0)
                //    {
                //        Debug.Log("上");

                //    }                    //超出邊界

                //    outLineTarget.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, angle);
                //    outLineTarget.GetComponent<RectTransform>().position = new Vector3(400,250); //screenPos + fixedVector2 - new Vector3((canvas.GetComponent<RectTransform>().sizeDelta.x / 2.0f), (canvas.GetComponent<RectTransform>().sizeDelta.y / 2.0f));
                //    outLineTarget.SetActive(true);
                //    arrow.SetActive(false);

                //}

                // arrow.rectTransform.DOShakePosition(10, new Vector3(0, 1, 0), 10,0);



            }
            if (timer >= alertEventRate && !showArrowTarget)
            {

                arrow.gameObject.SetActive(true);
                //開啟提示
                // arrow.transform.DOMoveY(2+ screenPos.y, .5f).SetLoops(-1, LoopType.Yoyo);
                showArrowTarget = true;
                timer = 0;

            }
        }
        else
        {
            GrandMaCube = createAMA.instance.getAnsObject();
        }
    }
    public void RecycleImage()
    {
        showArrowTarget = false;
        arrow.gameObject.SetActive(false);
        timer = 0;
    }
    

    }
