using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeanimationScript : MonoBehaviour
{
    [SerializeField] Animator fadeAnimation; //アニメーション
    // Start is called before the first frame update
    void Start()
    {
        fadeStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeStart()
    {
        fadeAnimation.SetBool("fadeBool", true);
        Debug.Log("fadeStart");
        //fadeAnimation.SetBool("emptyBool", true);
    }

    public void EndEffect()
    {
        Debug.Log("EndEffect!");
        Destroy(this.gameObject);
    }

}
