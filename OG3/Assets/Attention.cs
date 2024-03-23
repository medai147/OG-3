using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attention : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.attention)
        {
            Destroy(this.gameObject);
        }
    }

    public void onClickd_attention()
    {
        if(!GameManager.instance.attention)
        {
            GameManager.instance.attention = true;
            Destroy(this.gameObject);
        }
        
    }
}
