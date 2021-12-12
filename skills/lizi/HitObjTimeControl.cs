using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjTimeControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JishiTime();
    }



    private void OnEnable()
    {
        if (GetComponent<BoxCollider2D>())
        {
            print("  控制碰撞块存在 时间 ");
            GetComponent<BoxCollider2D>().enabled = true;
            IsKyjishi = true;
        }
    }


    float jishi = 0;
    float chunzaishijian = 1;
    bool IsKyjishi = false;

    void JishiTime()
    {
        if (!IsKyjishi) return;
        jishi += Time.deltaTime;
        if (jishi >= chunzaishijian)
        {
            jishi = 0;
            IsKyjishi = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }


}
