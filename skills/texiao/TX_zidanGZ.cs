﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TX_zidanGZ : TX_zidan
{

    float testNums = 0;
    [Header("跟踪时间")]
    public float GenZongTime = 1;

    public float CunzaiSJ = 4;

    void GenZong()
    {
        if (!_player) return;
        //print("??? ------------   testN    " + testN);
        if (testNums < GenZongTime)
        {
            testNums += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByPostion(_player.transform.position, this.transform.position, speeds);

        }

        if(testNums >= CunzaiSJ)
        {
            RemoveSelf();
            ResetAll();
        }

    }

    public bool IsGZ = true;

    private void Update()
    {
        YanchiHit();
        if (!IsGZ) return;
        
        GenZong();
    }


    public override void ResetAll()
    {
        base.ResetAll();
        testNums = 0;
        testN = 0;
    }
}



