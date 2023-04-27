using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationfinishScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animation_finished()
    {
        Story_new.instance.nextflag = true;
        Story_new.instance.animationfinishedflag = true;
        Destroy(this.gameObject);
    }

    public void fade_next()
    {
        Story_new.instance.nextflag = true;
        Instantiate((GameObject)Resources.Load("Animations/fadeopenImage"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, GameObject.Find("GameManager").transform);
        Destroy(this.gameObject);
    }
}
