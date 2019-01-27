using UnityEngine;
using System.Collections;
using System;  //Serializable
using System.Runtime.Serialization.Formatters.Binary; //BinaryFormatter
using System.IO;  //FileStream

public class ScoreBoardDataControl : MonoBehaviour
{
    public static ScoreBoardDataControl instance;

    //定義在下方
    private ScoreData data;

    //要顯示的名次數量     
    private const int Places = 3;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            //載入排行榜資料
            LoadData();
            instance = this;
        }
        else if (instance != this)
        {
            //只能有一個ScoreBoardDataControl
            Destroy(gameObject);
        }
    }

    void LoadData()
    {
        //如果檔案存在(表示不是第一次開啟遊戲)
        if (File.Exists(Application.persistentDataPath + "/scoreInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/scoreInfo.dat", FileMode.Open);

            //把裝置中的二進位檔案反序列化，存入data變數中
            data = (ScoreData)bf.Deserialize(file);
            file.Close();
        }
        else
        {     //如果檔案不存在(第一次開啟遊戲)
            InitData(); //初始化資料
            SaveData(); //儲入裝置
        }
    }

    void InitData()
    {
        //初始化data、data的陣列
        data = new ScoreData();
        data.scores = new int[Places];

        for (int i = 0; i < Places; i++)
        {
            data.scores[i] = 0;
        }
    }

    void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();

        //新增檔案
        FileStream file = File.Create(Application.persistentDataPath + "/scoreInfo.dat");

        //序列化，存入裝置
        bf.Serialize(file, data);
        file.Close();
    }

    public int NewScore(int score)
    {
        //判斷此分數能排在第幾名
        int place = Places - 1;

        while (place >= 0 && score > data.scores[place])
        {
            place--;
        }

        place++;

        //無法進入排行榜
        if (place >= Places)
            return -1;

        //把此分數之後的排名向後挪一名
        for (int i = Places - 2; i >= place; i--)
        {
            data.scores[i + 1] = data.scores[i];
        }

        //更新名次
        data.scores[place] = score;

        //存入裝置
        SaveData();
        return place;
    }

    //取得place名次的分數
    public int GetScore(int place)
    {
        return data.scores[place];
    }
    public void DeleteDat()
    {
        File.Delete(Application.persistentDataPath + "/scoreInfo.dat");
        InitData(); //初始化資料
        SaveData(); //儲入裝置
    }
}

[Serializable]
class ScoreData
{
    public int[] scores; //儲存的分數
}