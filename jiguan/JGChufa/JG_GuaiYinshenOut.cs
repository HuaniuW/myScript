using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_GuaiYinshenOut : JG_ChufaBase
{
    [Header("出现的 怪物")]
    public string OutGuaiName = "G_zhiren";

    [Header("出现前的 特效")]
    public string TX_GuaiOutName = "TX_yinshenYM1";

    [Header("是否显示 出现前的 特效")]
    public bool IsShowTX = true;


    public List<Transform> GuaiOutPosList = new List<Transform>() { };


    protected override void Chufa()
    {
        //GuaiOut();
        ListNums = GuaiOutPosList.Count;
        IsStart = true;
        print("  触发！！！！！！！ ");
        
    }




    private void GuaiOut()
    {
        foreach(Transform pos in GuaiOutPosList)
        {
            ChuGuai(pos);
        }

    }


    void ChuGuai(Transform pos)
    {
        if (IsShowTX)
        {
            GameObject tx = GlobalTools.GetGameObjectInObjPoolByName(TX_GuaiOutName);
            tx.transform.position = pos.position;
        }


        GameObject guai = GlobalTools.GetGameObjectInObjPoolByName(OutGuaiName);
        
        guai.transform.position = pos.position;
        print(" 出现 纸人 怪 ！！！ " + guai.transform.position);
    }

   

    private void Update()
    {
        ShunxuChuGuai();
    }

    int ListNums = 0;
    float jishiNums = 0;
    bool IsChuGuai = false;
    float JiangeTimes = 0.5f;
    int posNum = 0;


    bool IsStart = false;

    private void ShunxuChuGuai()
    {
        //throw new NotImplementedException();
        //print("..........");
        if (!IsStart)
        {
            return;
        }
        //print(" ListNums  "+ ListNums+ "  posNum   "+ posNum);
        if (ListNums == 0 || posNum == ListNums) {
            RemoveSelf();
            return;
        }
        

        if (!IsChuGuai)
        {
            IsChuGuai = true;
            JiangeTimes = GlobalTools.GetRandomDistanceNums(0.5f);
            jishiNums = 0;
        }
        jishiNums += Time.deltaTime;
        if(jishiNums>= JiangeTimes)
        {
            ChuGuai(GuaiOutPosList[posNum]);
            posNum++;
            jishiNums = 0;
            IsChuGuai = false;
        }


    }
}
