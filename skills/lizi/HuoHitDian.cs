using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuoHitDian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool IsHasDianbao = false;

    [Header("产生电爆 间隔 时间")]
    public float CanDianBaoJiangeTime = 0;


    float CanDianbaoJishi = 0;
    void TheCanFireJishi()
    {
        if (CanDianBaoJiangeTime == 0 || !IsHasDianbao) return;
        CanDianbaoJishi += Time.deltaTime;
        if (CanDianbaoJishi >= CanDianBaoJiangeTime)
        {
            CanDianbaoJishi = 0;
            IsHasDianbao = false;
        }
    }

    void OnTriggerEnter2D(Collider2D Coll)
    {
        //print(" pengdao:  "+Coll.name+"  毒遇到火  碰撞    " + Coll.tag);
        if (Coll.tag == GlobalTag.DIAN)
        {
            print("------------ 电爆");
            ShowDianbao();
        }
    }

    private void ShowDianbao()
    {
        if (!IsHasDianbao)
        {
            IsHasDianbao = true;
            CreateDianbao();
        }
    }

    private void OnEnable()
    {
        IsHasDianbao = false;
        CanDianbaoJishi = 0;
    }


    string DianbaoName = "TX_Dianbao";

    GameObject CreateDianbao(Transform pos = null)
    {
        GameObject huoyan = GlobalTools.GetGameObjectInObjPoolByName(DianbaoName);
        //GameObject huoyan = GlobalTools.GetGameObjectByName(Huoyan);
        huoyan.transform.parent = this.transform.parent;

        huoyan.name = DianbaoName;
        huoyan.gameObject.SetActive(true);

        if (pos)
        {
            float fanwei = 2;
            float ___x = pos.position.x - fanwei + GlobalTools.GetRandomDistanceNums(fanwei);
            float ___y = pos.position.y - fanwei + GlobalTools.GetRandomDistanceNums(fanwei);
            Vector2 newPos = new Vector2(___x, ___y);
            huoyan.transform.position = newPos;
        }
        else
        {
            huoyan.transform.position = this.transform.position;
        }

        //****** 2021-11-08 注意 直接在这生成粒子 调用粒子系统-直接生成 直接生成 直接生成  不要return出去给变量 在外面调用return生成后 有概率不被摄像机渲染 注意 粒子系统 都直接生成     靠 *****************

        //print(huoyan.GetComponent<ParticleSystem>().IsAlive()+"   ?stop?  "+huoyan.GetComponent<ParticleSystem>().isStopped+"   --粒子数 "+ huoyan.GetComponent<ParticleSystem>().particleCount);
        //if (huoyan.GetComponent<ParticleSystem>().particleCount == 0)
        //{
        //    huoyan.gameObject.SetActive(false);
        //    huoyan = GlobalTools.GetGameObjectInObjPoolByName(Huoyan);
        //    huoyan.transform.parent = this.transform.parent;
        //    if (pos)
        //    {
        //        huoyan.transform.position = pos.position;
        //    }
        //    else
        //    {
        //        huoyan.transform.position = this.transform.position;
        //    }

        //    huoyan.name = Huoyan;
        //    huoyan.gameObject.SetActive(true);
        //}
        huoyan.GetComponent<ParticleSystem>().Simulate(0.0f);
        huoyan.GetComponent<ParticleSystem>().Play();
        return huoyan;
    }


    // Update is called once per frame
    void Update()
    {
        TheCanFireJishi();
    }
}
