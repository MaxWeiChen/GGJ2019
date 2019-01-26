using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreboardScoreText : MonoBehaviour
{
    //第幾名 (第一名:0，第二名:1，第三名:2)
    public int place;
    void Start()
    {

    }
    void Update()
    {
        if (GameCtrl.timeout == true) {
            //由ScoreBoardDataControl依照名次取得分數
            int score = ScoreBoardDataControl.instance.GetScore(place);


            if (score != 0)
            {
                GetComponent<Text>().text = "No."+ (place + 1) +"  $" + score.ToString();
            }
            else
            {
                GetComponent<Text>().text = "No." + (place + 1) + "  －－－－";
            }
        }
    }
}