using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TX_LeiLight : MonoBehaviour
{
    float leiLiangTimes = 0.2f;
    bool IsLeishan = false;
    float leishanTimes = 0;
    float qidongTimes = 0;

    Vector2 YSPos;

    // Start is called before the first frame update
    void Start()
    {
        YSPos = this.gameObject.transform.position;
    }


    AudioSource Leisheng;

    //开始雷闪
    public void GetLeishan(AudioSource leisheng)
    {
        Leisheng = leisheng;
        leishanTimes = 0.0001f;
        if (leishanTimes == 0) return;
        if (GlobalTools.GetRandomNum() >= 60) return;
        float _x = YSPos.x+GlobalTools.GetRandomDistanceNums(1);
        float _y = YSPos.y;
        this.gameObject.transform.position = new Vector2(_x,_y);
        qidongTimes = 0.5f + GlobalTools.GetRandomDistanceNums(0.8f);
    }


   
    void Light1Shan()
    {
        if (qidongTimes == 0) return;
        leishanTimes += Time.deltaTime;

        if (leishanTimes >= qidongTimes && leishanTimes < qidongTimes + leiLiangTimes)
        {
            if (!IsLeishan) IsLeishan = true;
        }

        if (!IsLeishan) return;
        float liangdu = 1 + GlobalTools.GetRandomDistanceNums(1f);
        GetComponent<Light2D>().intensity = liangdu;

        if (leishanTimes > qidongTimes + leiLiangTimes)
        {
            GetComponent<Light2D>().intensity = 0;
            IsLeishan = false;
            qidongTimes = 0;
            leishanTimes = 0;
            
            DaleiTimes = 0.3f + GlobalTools.GetRandomDistanceNums(0.8f);
            IsDalei = true;
        }
    }


    float leishengTimes = 0;
    float DaleiTimes = 0;
    bool IsDalei = false;
    void LeiSheng()
    {
        if (!IsDalei) return;
        leishengTimes += Time.deltaTime;
        if(leishengTimes>= DaleiTimes)
        {
            Leisheng.Play();
            IsDalei = false;
            leishengTimes = 0;
            DaleiTimes = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        Light1Shan();
        LeiSheng();
    }
}
