using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool[] getimage = new bool[6];

    public int storyindex;

    public int coin;
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
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("COIN", coin);
        PlayerPrefs.Save();
    }
}
