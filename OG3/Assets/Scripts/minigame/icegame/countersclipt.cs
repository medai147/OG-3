using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class countersclipt : MonoBehaviour
{
    private int ice;
    private int score;
    private int finishcount;
    //private static int nowcoin;
    //private static int count;
    public Text scoretext;
    public Text icetext;
    public Text finishscoretext;
    public Text getcointext;
    public Text cointext;
    public Text startcointext;
    [SerializeField] GameObject finishpanel;
    public int clearcount;

    public AudioClip ice_bgm;
    public AudioClip coinget;
    public AudioClip nocoin;

    AudioSource[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        sounds[0].clip = ice_bgm;
        sounds[0].Play();

        //count++;
        //PlayerPrefs.SetInt("COUNTERCOUNT", count);
        //PlayerPrefs.Save();
        ice =  Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //nowcoin = PlayerPrefs.GetInt("NOWCOIN");
        startcointext.text = "所持金:" + GameManager.instance.coin;
        scoretext.text =score.ToString();
        if (finishpanel.activeSelf == true)
        {
            finishcount++;
            sounds[0].clip = ice_bgm;
            sounds[0].Play();
        } else
        {
            finishcount = 0;
            sounds[1].Stop();
        }
        if(finishcount == 1)
        {
            if(score < 20) {
                sounds[1].clip = nocoin;
            } else
            {
                sounds[1].clip = coinget;
            }
            sounds[1].Play();

            finishscore();
        }

       // Debug.Log("bbb");
       if(ice == 1)
        {
            icetext.text = "ストロベリー";
        } else if (ice == 2)
        {
            icetext.text = "抹茶";
        } else if(ice == 3)
        {
            icetext.text = "チョコレート";
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(ice == 1 && other.name == "pinkImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        }
        else if(ice == 2 && other.name == "greenImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        } else if(ice == 3 && other.name == "brownImagemove")
        {
            score += 1;
            ice = Random.Range(1, 4);
        }else
        {
            score -= 1;
            ice = Random.Range(1, 4);
        }
    }

    private void finishscore()
    {
        clearcount++;
        finishscoretext.text = score.ToString();
        if (score > 30 && finishpanel.activeSelf == true)
        {
            getcointext.text = "30コインゲット！";
            GameManager.instance.coin += 30;
            score = 0;

        }
        else if (score > 25 && finishpanel.activeSelf == true)
        {
            getcointext.text = "25コインゲット！";
            GameManager.instance.coin += 25;
            score = 0;
        }
        else if (score > 20 && finishpanel.activeSelf == true)
        {
            getcointext.text = "20コインゲット！";
            GameManager.instance.coin += 20;
            score = 0;
        }
        else if (score >= 0 && finishpanel.activeSelf == true)
        {
            getcointext.text = "給料無し！";
            score = 0;
        }
        cointext.text = "所持金:" + GameManager.instance.coin;
        //PlayerPrefs.SetInt("NOWCOIN", nowcoin);
        //PlayerPrefs.Save();
    }
}
