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
    public float _Speed = 5;
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
               
                arrow.GetComponent<RectTransform>().position = screenPos + fixedVector2;
                //outLineTarget.SetActive(false);
                Vector2 dir = (outLineTarget.GetComponent<RectTransform>().anchoredPosition - arrow.GetComponent<RectTransform>().anchoredPosition).normalized;
                outLineTarget.GetComponent<RectTransform>().up = dir;
                float distance = Vector3.Distance(outLineTarget.GetComponent<RectTransform>().anchoredPosition, arrow.GetComponent<RectTransform>().anchoredPosition);
                if(distance>5)
                    outLineTarget.GetComponent<RectTransform>().anchoredPosition -= dir* _Speed * Time.deltaTime * distance;
                if ((Mathf.Abs(arrow.GetComponent<RectTransform>().anchoredPosition.x) > (canvas.GetComponent<RectTransform>().sizeDelta.x / 2.0f))
                    || Mathf.Abs(arrow.GetComponent<RectTransform>().anchoredPosition.y) > (canvas.GetComponent<RectTransform>().sizeDelta.y / 2.0f))
                {
                    outLineTarget.SetActive(true);
                   
                    outLineTarget.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                    outLineTarget.GetComponent<RectTransform>().up = dir;


                }
                
               


            }
            if (timer >= alertEventRate && !showArrowTarget)
            {
                outLineTarget.SetActive(true);
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
