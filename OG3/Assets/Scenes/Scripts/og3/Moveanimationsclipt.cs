using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveanimationsclipt : MonoBehaviour
{
    [SerializeField] float smallspeed;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta += new Vector2(0, -smallspeed) * Time.deltaTime;
        if(rectTransform.sizeDelta.y < 0)
        {
            Destroy(gameObject);
        }
    }
}
