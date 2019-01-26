using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour {
    public AudioClip BGM;
    public AudioClip GetCoin;
    public AudioClip GetResult;
    private AudioSource AS;
    public static bool timeout;
    public static bool complete;
    //private GameObject delete;
    private GameObject replay;
    private GameObject exit;
    private GameObject BG;
    public Text Time_text;
    public Text AMA_text;
    public Text Score_text;
    int temCount;
    int Bonus;
    int MaxPeople = createAMA.instance.people.Length;



    private int AMAcount;
    float time;
    bool SpeedUp;
    
    // Use this for initialization
    void Start () {
        AS = this.GetComponent<AudioSource>();
        // ScoreBoardDataControl.instance = null;
        time = 60f;
        AMAcount = 0;
        timeout = false;
        complete = false;
        AS.clip = BGM;



        if (AS.clip != null)
        {
            AS.Play();
            time = BGM.length;
        }
        StartCoroutine(NormalMusic(time*3/4));

        //delete = GameObject.Find("DeleteButton");
        //delete.SetActive(false);
        exit = GameObject.Find("ExitButton");
        exit.SetActive(false);
        replay = GameObject.Find("ReplayButton");
        replay.SetActive(false);
        BG = GameObject.Find("BackGround");
        BG.SetActive(false);

        temCount = 0;
        SpeedUp = false;
        Bonus = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (!timeout) {
            time = time - Time.deltaTime;
            Time_text.text = "Time : " + (float)((int)((time-4.7f) * 10)) / 10;
            AMA_text.text = "Capture : " + AMAcount;
            if (complete)
            {
                Bonus = Bonus + (int)(Time.deltaTime*200);
                Score_text.text = "Bonus: $" + Bonus;
            }
        }
        else
        {
            Time_text.text = "Time : 0";


            if (temCount < AMAcount*5)
            {
                Bonus = Bonus + 20;
                Score_text.text = "Bonus: $" + (Bonus);
                temCount = temCount + 1;
                AMA_text.text = "Capture : " + (AMAcount-temCount/5);
            }
            else if (temCount == AMAcount*5)
            {
                Bonus = Bonus + 20;
                Score_text.text = "Bonus: $" + (Bonus );
                temCount = temCount + 1;
                AMA_text.text = "Capture : " + (AMAcount - temCount/5);
                StartCoroutine(Result(0.5f));
                AS.Stop();


            }


        }
        if (SpeedUp)
        {
            AS.pitch = 1.6f - 2.4f*(time / BGM.length);
        }

		
	}

    IEnumerator NormalMusic(float second)
    {
        yield return new WaitForSeconds(second);
        SpeedUp = true;

        StartCoroutine(MusicEnd(second/3 - 4.7f));

    }
    IEnumerator MusicEnd(float second)
    {
        yield return new WaitForSeconds(second);
        timeout = true;
        //delete.SetActive(true);
        exit.SetActive(true);
        replay.SetActive(true);
        BG.SetActive(true);
        AS.clip = GetResult;
        AS.pitch = 1;
        AS.loop = true;
        AS.Play();
        Time.timeScale = 1;
    }
    IEnumerator Result(float second)
    {
        yield return new WaitForSeconds(second);
        ScoreBoardDataControl.instance.NewScore(Bonus);
        
    }

    public void AddAMA()
    {
        if (!timeout)
        {
            AMAcount = AMAcount + 1;
            AS.PlayOneShot(GetCoin, 3f);
            if(AMAcount >= MaxPeople)
            {
                complete = true;
                Time.timeScale = 4;
                BG.SetActive(true);
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);

    }

}
