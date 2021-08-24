using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bgm : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main scene" || SceneManager.GetActiveScene().name == "icegame scene" || SceneManager.GetActiveScene().name == "gacha scene")
        {
            Destroy(this.gameObject);
        }
    }
}
