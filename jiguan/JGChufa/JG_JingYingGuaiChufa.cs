using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_JingYingGuaiChufa : JG_ChufaBase
{
    [Header("触发的 精英怪")]
    public GameObject JingYingGuai;

    void Start()
    {
        if (!JingYingGuai.GetComponent<AIBase>()) RemoveSelf();
        //print("精英怪触发 行动 机关");
    }


    protected override void Chufa()
    {
        base.Chufa();
        if (JingYingGuai.GetComponent<AIBase>())
        {
            JingYingGuai.GetComponent<AIBase>().IsCanAtk = true;
        }
    }
}
