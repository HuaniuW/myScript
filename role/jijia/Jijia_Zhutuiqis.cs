using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jijia_Zhutuiqis : MonoBehaviour
{
    [Header("助推器 1234****")]
    public GameObject Zhutuiqi1;
    public GameObject Zhutuiqi2;
    public GameObject Zhutuiqi3;
    public GameObject Zhutuiqi4;


    // Start is called before the first frame update
    void Start()
    {
        //PaoZhutuiqi(Zhutuiqi1);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZHUTUIQI_TUOLI, this.ZhutuiqiTuoli);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.JIJIA_BAOZHA, this.JijiaBaozha);
    }

    private void JijiaBaozha(UEvent evt)
    {
        //throw new NotImplementedException();
        GetDieBaozhaKuai();
    }

    bool IsHasPaoqiZhutuiqi = false;
    private void ZhutuiqiTuoli(UEvent evt)
    {
        //隐藏 助推器
        HideZhutuiqi();

        //throw new NotImplementedException();
        PaoZhutuiqi(Zhutuiqi1);
        PaoZhutuiqi(Zhutuiqi2);
        PaoZhutuiqi(Zhutuiqi3);
        PaoZhutuiqi(Zhutuiqi4);
        IsHasPaoqiZhutuiqi = true;

    }

    public void HideZhutuiqi()
    {
        GetComponent<GameBody>().GetDB().armature.GetSlot("JYHuojianTong1")._SetDisplayIndex(-1);
        GetComponent<GameBody>().GetDB().armature.GetSlot("JYHuojianTong2")._SetDisplayIndex(-1);
        GetComponent<GameBody>().GetDB().armature.GetSlot("JZHuojian2")._SetDisplayIndex(-1);
        GetComponent<GameBody>().GetDB().armature.GetSlot("JZHuojian1")._SetDisplayIndex(-1);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.ZHUTUIQI_TUOLI, this.ZhutuiqiTuoli);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.JIJIA_BAOZHA, this.JijiaBaozha);
    }


    void PaoZhutuiqi(GameObject zhutuiqi)
    {
        zhutuiqi.SetActive(true);
        zhutuiqi.GetComponent<Rigidbody2D>().gravityScale = 1;
        //初速度 这里要获取 玩家速度
        float __x = GetComponent<Rigidbody2D>().velocity.x - 30- GlobalTools.GetRandomDistanceNums(180);
        float __y = 60+GlobalTools.GetRandomDistanceNums(400);
        zhutuiqi.GetComponent<Rigidbody2D>().AddForce(new Vector2(__x, __y));
        //角速度
        zhutuiqi.GetComponent<JijiaZhutuiqi>().SetJiaoSDSpeed();

    }

    public GameObject Jiankui;


    public GameObject Chibang;
    public GameObject ChibangWai;
    
    public GameObject Shoubi;
    
    public GameObject Maichongpao;



    public ParticleSystem BaozhaHuoyan;




    //机甲die 爆炸 碎片
    public void GetDieBaozhaKuai(float vx = 0)
    {
        print("  --------------------->vx "+vx);
        BaozhaHuoyan.gameObject.SetActive(true);
        if (!IsHasPaoqiZhutuiqi)
        {
            GetComponent<GameBody>().GetDB().armature.GetSlot("JYHuojianTong1")._SetDisplayIndex(-1);
            GetComponent<GameBody>().GetDB().armature.GetSlot("JYHuojianTong2")._SetDisplayIndex(-1);
            GetComponent<GameBody>().GetDB().armature.GetSlot("JZHuojian2")._SetDisplayIndex(-1);
            GetComponent<GameBody>().GetDB().armature.GetSlot("JZHuojian1")._SetDisplayIndex(-1);
            Zhutuiqi1.SetActive(true);
            Zhutuiqi2.SetActive(true);
            Zhutuiqi3.SetActive(true);
            Zhutuiqi4.SetActive(true);
            Zhutuiqi1.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);
            Zhutuiqi2.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);
            Zhutuiqi3.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);
            Zhutuiqi4.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);
        }
       

        GetComponent<GameBody>().GetDB().armature.GetSlot("JZChibang2")._SetDisplayIndex(-1);
        Chibang.SetActive(true);
        Chibang.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);

        GetComponent<GameBody>().GetDB().armature.GetSlot("JYChibang")._SetDisplayIndex(-1);
        ChibangWai.SetActive(true);
        ChibangWai.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);

        GetComponent<GameBody>().GetDB().armature.GetSlot("jyouXiaobi")._SetDisplayIndex(-1);
        GetComponent<GameBody>().GetDB().armature.GetSlot("jyouShou")._SetDisplayIndex(-1);
        Shoubi.SetActive(true);
        Shoubi.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);

        GetComponent<GameBody>().GetDB().armature.GetSlot("JYPao")._SetDisplayIndex(-1);
        Maichongpao.SetActive(true);
        Maichongpao.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);

        if (GlobalTools.GetRandomNum() > 50)
        {
            GetComponent<GameBody>().GetDB().armature.GetSlot("jYJiankui")._SetDisplayIndex(-1);
            Jiankui.SetActive(true);
            Jiankui.GetComponent<JijiaZhutuiqi>().BaozhaSuipian(vx);
        }


    }






    // Update is called once per frame
   
}
