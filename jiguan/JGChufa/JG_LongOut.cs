using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_LongOut : JG_ChufaBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Header("龙！")]
    public GameObject Long;


    [Header("终点 位置！！！")]
    public Transform EndPos;


    bool IsStart = false;

    void TheStart()
    {
        if (!IsStart) return;
        if (!Long.activeSelf)
        {
            Long.SetActive(true);
        }

        if (Long)
        {
            if(Long.transform.position.x>= EndPos.position.x)
            {
                Long.SetActive(false);
                RemoveSelf();
            }
        }

    }


    protected override void Chufa()
    {
        if (HitObj.transform.position.x <= this.transform.position.x)
        {
            IsStart = true;
        }
        else
        {
            RemoveSelf();
        }


        
        

        print("g怪组over！");
        //RemoveSelf();
    }

    // Update is called once per frame
    void Update()
    {
        TheStart();
    }
}
