using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {
    public AudioClip BGM;
    private AudioSource AS;
    public static bool timeout;
    private GameObject delete;
    private GameObject exit;
    private GameObject BG;
    public Text Time_text;
    public Text AMA_text;
    public Text Score_text;
    int temCount;

    private int AMAcount;
    float time;
    
    // Use this for initialization
    void Start () {
        AS = this.GetComponent<AudioSource>();
        // ScoreBoardDataControl.instance = null;
        time = 100f;
        AMAcount = 0;
        timeout = false;
        AS.clip = BGM;



        if (AS.clip != null)
        {
            AS.Play();
            time = BGM.length;
        }
        StartCoroutine(MusicEnd(time));

        //Time_text = GameObject.Find("Text_text").GetComponent<Text>();
        //AMA_text = GameObject.Find("AMA_text").GetComponent<Text>();
        delete = GameObject.Find("DeleteButton");
        delete.SetActive(false);
        exit = GameObject.Find("ExitButton");
        exit.SetActive(false);
        BG = GameObject.Find("BackGround");
        BG.SetActive(false);

        temCount = 0;

    }
	
	// Update is called once per frame
	void Update () {
        if (!timeout) {
            time = time - Time.deltaTime;
            Time_text.text = "Time : " + (float)((int)(time * 10)) / 10;
            AMA_text.text = "Capture : " + AMAcount;
        }
        else
        {
            Time_text.text = "Time : 0";

            if (temCount < AMAcount)
            {
                Score_text.text = "Bonus: $" + temCount * 100;
                temCount = temCount + 1;
            }
            else if (temCount == AMAcount)
            {
                Score_text.text = "Bonus: $" + temCount * 100;
                temCount = temCount + 1;
                StartCoroutine(Result(0.5f));


            }


        }
        //測試用
        if (Input.GetKey(KeyCode.A))
        {
            AddAMA();
        }
		
	}

    IEnumerator MusicEnd(float second)
    {
        yield return new WaitForSeconds(second);
        timeout = true;
        delete.SetActive(true);
        exit.SetActive(true);
        BG.SetActive(true);

        

    }
    IEnumerator Result(float second)
    {
        yield return new WaitForSeconds(second);
        ScoreBoardDataControl.instance.NewScore(AMAcount * 100);
    }

    public void AddAMA()
    {
        if (!timeout)
        {
            AMAcount = AMAcount + 1;
        }
    }
    void Exit()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);

    }

}
