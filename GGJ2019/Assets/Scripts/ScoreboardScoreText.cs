using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreboardScoreText : MonoBehaviour
{
    //第幾名 (第一名:0，第二名:1，第三名:2)
    public int place;
    int down;
    void Start()
    {
        down = -1;
    }
    void Update()
    {
        if (GameCtrl.timeout == true)
        {
            //由ScoreBoardDataControl依照名次取得分數
            int score = ScoreBoardDataControl.instance.GetScore(place);

            if (GameCtrl.rank == place && GameCtrl.ScoreColorChange)
            {

                if (GetComponent<Text>().color.b < 0.2f)
                {
                    down = 1;
                }
                if (GetComponent<Text>().color.b > 0.6f)
                {
                    down = -1;
                }
                GetComponent<Text>().color = GetComponent<Text>().color + new Color(0f, 0f, 0.015f * down, 0f);

            }

            if (score != 0)
            {
                GetComponent<Text>().text = "No" + (place + 1) + ":$" + score.ToString();
            }
            else
            {
                GetComponent<Text>().text = "No" + (place + 1) + ":------";
            }

        }
    }
}