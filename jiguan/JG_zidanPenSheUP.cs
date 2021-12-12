using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JG_zidanPenSheUP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetInit();
    }

    [Header("子弹喷射点")]
    public Transform ZiDanPenShePos;

    [Header("喷射时间间隔")]
    public float PenSheShiJianJianGe = 2;

    [Header("子弹喷射速度")]
    public float PenSheSpeedY = 30;



    Vector2 speedV2 = new Vector2(0,0);
    void GetInit()
    {
        PenSheShiJianJianGe = 1f + GlobalTools.GetRandomDistanceNums(2);
        //PenSheSpeedY = 30 + GlobalTools.GetRandomDistanceNums(PenSheSpeedY);
        speedV2 = GetSpeedV2();
    }



    Vector2 GetSpeedV2()
    {
        float __x = GlobalTools.GetRandomDistanceNums(1);
        __x = GlobalTools.GetRandomNum() > 50 ? __x : -__x;
        float __y = 30 + GlobalTools.GetRandomDistanceNums(PenSheSpeedY);
        return new Vector2(__x,__y);
    }

    bool IsDuoFa = false;
    void duofa()
    {
        int nums = 1 + GlobalTools.GetRandomNum(3);
        for(int i = 0; i < nums; i++)
        {
            SetZiDanSpeed(GetSpeedV2());
        }
    }


    float PenSheNums = 0;
    void ZiDannPenShe()
    {
        PenSheNums += Time.deltaTime;
        if(PenSheNums>= PenSheShiJianJianGe)
        {
            PenSheNums = 0;
            if (GlobalTools.GetRandomNum() > 30)
            {
                duofa();
            }
            else
            {
                SetZiDanSpeed(speedV2);
            }
            
        }
    }



    void SetZiDanSpeed(Vector2 speedV2)
    {
        //GameObject _zidan = GlobalTools.GetGameObjectByName("JG_zidanUp");
        GameObject zidan = Resources.Load("JG_zidanUp") as GameObject;
        GameObject zidanUp = ObjectPools.GetInstance().SwpanObject2(zidan);
        //zidanUp.GetComponent<TX_zidan>().IsCanHit = false;
        zidanUp.transform.position = ZiDanPenShePos.position;
        //zidanUp.GetComponent<TX_zidan>().IsCanHit = true;
        zidanUp.GetComponent<TX_zidan>().SetV2Speed(speedV2);
        zidanUp.GetComponent<TX_zidan>().GetSpeedV2();
    }



    // Update is called once per frame
    void Update()
    {
        ZiDannPenShe();
    }
}
