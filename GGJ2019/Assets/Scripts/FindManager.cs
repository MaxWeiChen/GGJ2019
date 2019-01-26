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
    // Use this for initialization
    void Start () {
        arrow = Instantiate(target);
        arrow.gameObject.SetActive(false);
        
        arrow.transform.SetParent(canvas.transform);
        cam = Camera.main;
        fixedVector2 = new Vector3(fixedX, fixedY, placeDistance);
    }
    // Update is called once per frame
    void FixedUpdate () {
        timer += Time.deltaTime;
        Vector3 screenPos = cam.WorldToScreenPoint(GrandMaCube.transform.position);
        if (!showArrowTarget)
        {
            
        }
        else
        {
            
            //GrandMaCube.transform.position;
            Debug.Log("target is " + screenPos);
            arrow.GetComponent<RectTransform>().position = screenPos + fixedVector2;
            // arrow.rectTransform.DOShakePosition(10, new Vector3(0, 1, 0), 10,0);

            

        }
        if (timer >= alertEventRate&& !showArrowTarget)
        {
            
            arrow.gameObject.SetActive(true);
            //開啟提示
           // arrow.transform.DOMoveY(2+ screenPos.y, .5f).SetLoops(-1, LoopType.Yoyo);
            showArrowTarget = true;
            timer = 0;
            
        }

    }
    

    }
