using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames.BasicApi;

public class GoogleGameManager : MonoBehaviour {
    public GameObject debugmsgview;
    public Text msg;
    public string leaderboardID = "CgkIzf6CquASEAIQAQ";//排行榜
    public string missionID = "CgkIzf6CquASEAIQAw";//成就
    static PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
    // Use this for initialization
    public static bool logbo = false;
    void Login()
    {
        Social.localUser.Authenticate((bool success) => {
            logbo = success;
            debugmsgview.SetActive(true);
            msg.text = "登入失敗";
        });
    }
    void Start() {

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success => {
            if (success)
            {
                Debug.Log("Authentication successful");
                string userInfo = "Username: " + Social.localUser.userName +
                    "\nUser ID: " + Social.localUser.id +
                    "\nIsUnderage: " + Social.localUser.underage;
                Debug.Log(userInfo);
                debugmsgview.SetActive(true);
                msg.text = userInfo;
            }
            else
                Debug.Log("Authentication failed");
        });
        Login();
        logbo = true;
  
        //  PlayGamesPlatform.Instance.IncrementAchievement( //多漸進
        //"CgkIzf6CquASEAIQAw", 1, (bool success) => {
        //    // handle success or failure
        //    if (!success)
        //    {
        //        debugmsgview.SetActive(true);
        //        msg.text = "成就讀取失敗";
        //    }
        //  });
        //Social.ReportScore(100, "CgkIzf6CquASEAIQAQ", (bool success) => {
        //    // handle success or failure
        //    if (!success)
        //    {
        //        debugmsgview.SetActive(true);
        //        msg.text = "分數讀取失敗";
        //    }
        //});
        //Social.ShowLeaderboardUI();
    }
    public void OpenRankList()
    {
        print("顯示排行榜");
        // 顯示排行榜
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Login();
            Debug.Log("Cannot show leaderboard: not authenticated");
        }

    }
    void ReportScore(long score, string leaderboardID)
    {
        Debug.Log("Reporting score " + score + " on leaderboard " + leaderboardID);
        Social.ReportScore(score, leaderboardID, success => {
            Debug.Log(success ? "Reported score successfully" : "Failed to report score");
        });
    }
    public void CloseMsgView()
    {
        debugmsgview.SetActive(false);
    }
	// Update is called once per frame
	void Update () {
        
    }
}
