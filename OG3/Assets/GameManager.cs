using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int[] getimage = new int[14];

    public int[] getcontent = new int[6];


    public int coin;

    //GameManager内変数
    string[] key = new string[6];



    public string screenshotpath = "";



    public bool attention = false;
    internal Dictionary<string, int> affectionPoints;


    public string previousSceneName; // 一つ前のシーン名を記録





    //いらない
    public int storyindex;
    public int storynum = 0;
    public string heroinename = "";
    public GameStateManager gameStateManager; // ゲームの状態を管理
    public List<string> logtext = new List<string>();
    //遷移前のシーンがどこか(0:スタート,1:メイン,2:セーブ)
    public int beforescene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        coin = PlayerPrefs.GetInt("COIN", 0);
        indexload(getimage,"key");
        //getimage[1] = PlayerPrefs.GetInt("key1", 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //ゲームが閉じる
    void OnApplicationQuit()
    {
        string coinkey = "COIN";
        PlayerPrefs.SetInt(coinkey, coin);
        PlayerPrefs.Save();

        indexsave(getimage,"key");
        //PlayerPrefs.SetInt("key0", getimage[0]);
        PlayerPrefs.Save();
    }

    //配列セーブ
    void indexsave(int[]save,string pass)
    {
        key = new string[save.Length];
        for(int i = 0; i < save.Length;i++)
        {
            key[i] = pass + i.ToString();
            PlayerPrefs.SetInt(key[i], save[i]);
            PlayerPrefs.Save();
        }
    }

    //配列ロード
    void indexload(int[]load, string pass)
    {
        for(int i = 0; i < load.Length;i++)
        {
            key[i] = pass + i.ToString();
            getimage[i] = PlayerPrefs.GetInt(key[i], 0);
        }
    }
}
