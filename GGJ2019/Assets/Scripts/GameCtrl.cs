using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip GetCoin;
    public AudioClip GetResult;
    private AudioSource AS;
    public static bool timeout;
    public static bool complete;
    public static bool ScoreColorChange;
    bool SpeedUp;
    //private GameObject delete;
    //private GameObject replay;
    //private GameObject exit;
    private GameObject BG;
    private GameObject ScoreBoard;
    public Text Time_text;
    public Text AMA_text;
    public Text Score_text;
    int temCount;
    public static int rank;
    int Bonus;
    int MaxPeople;

    private int AMAcount;
    public float time = 60f;
    public static GameCtrl _instance;
   
    void Addtime()
    {
        time += 5.0f;
    }
    // Use this for initialization
    void Start()
    {
        AS = this.GetComponent<AudioSource>();
        
        AMAcount = 0;
        timeout = false;
        complete = false;
        SpeedUp = false;
        ScoreColorChange = false;
        AS.clip = BGM;

        AS.loop = true;

        if (AS.clip != null)
        {
            AS.Play();
            //time = BGM.length;
        }
        StartCoroutine(NormalMusic(time));

        //delete = GameObject.Find("DeleteButton");
        //delete.SetActive(false);
        /*
        exit = GameObject.Find("ExitButton");
        exit.SetActive(false);
        replay = GameObject.Find("ReplayButton");
        replay.SetActive(false);
        */
        ScoreBoard = GameObject.Find("ScoreBoard");
        ScoreBoard.SetActive(false);
        BG = GameObject.Find("BackGround");
        BG.SetActive(false);

        temCount = 1;
        rank = -1;
        Bonus = 0;
        MaxPeople = createAMA.instance.people.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeout)
        {
            time = time - Time.deltaTime;
            Time_text.text = "Time : " + time.ToString("0.0");
            AMA_text.text = "Capture : " + AMAcount;
            if (complete)
            {
                Bonus = Bonus + (int)(Time.deltaTime * 100);
                Score_text.text = "Bonus: $" + Bonus;
            }
        }
        else
        {
            Time_text.text = "Time : 0";

            if (AMAcount == 0 && temCount == 1)
            {
                temCount = temCount + 1;
                Score_text.text = "Bonus: $" + (Bonus);
                StartCoroutine(Result(0.5f));
                AS.Stop();
            }
            if (temCount < AMAcount * 5)
            {
                Bonus = Bonus + 20;
                Score_text.text = "Bonus: $" + (Bonus);
                temCount = temCount + 1;
                AMA_text.text = "Capture : " + (AMAcount - temCount / 5);
            }
            else if (temCount == AMAcount * 5)
            {
                Bonus = Bonus + 20;
                Score_text.text = "Bonus: $" + (Bonus);
                temCount = temCount + 1;
                AMA_text.text = "Capture : " + (AMAcount - temCount / 5);
                StartCoroutine(Result(0.5f));
                AS.Stop();


            }


        }
       

    }

    IEnumerator NormalMusic(float second)
    {
        while (time > 0)
        {
            if (time < 15)
            {
                AS.pitch = 1f + (time/60.0f);
            }
            
            yield return new WaitForFixedUpdate();
        }

        timeout = true;

        ScoreBoard.SetActive(true);
        BG.SetActive(true);
        AS.clip = GetResult;
        AS.pitch = 1.3f;
        AS.loop = true;
        AS.Play();
        Time.timeScale = 1;

    }
    
    IEnumerator Result(float second)
    {
        yield return new WaitForSeconds(second);
        rank = ScoreBoardDataControl.instance.NewScore(Bonus);
        ScoreColorChange = true;
    }

    public void AddAMA()
    {
        Addtime();
        if (!timeout)
        {
            AMAcount = AMAcount + 1;
            AS.PlayOneShot(GetCoin, 3f);
            if (AMAcount >= MaxPeople)
            {
                complete = true;
                Time.timeScale = 4;
                BG.SetActive(true);
                AS.clip = GetResult;
                AS.pitch = 1.5f;
                AS.loop = true;
                AS.Play();

            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }

}
