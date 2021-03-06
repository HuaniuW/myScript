﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XSDiban : MonoBehaviour {
    //消失的地板
    //震动声音
    public AudioSource zdSound;
    //粒子效果
    public ParticleSystem _yanmu;
    //隐藏地板
    public GameObject hideFloor;

    public GameObject suishi;


    // Use this for initialization
    void Start () {


        if (suishi != null)
        {
            suishi.SetActive(false);
        }

    }



    public bool IsChangeKuai = false;
    public BoxCollider2D Kuai;

    public void BeStart()
    {
        HideFloor();
        GetShock();
        PlayYanmu();
        PlaySound();
        PlaySanluo();
        JishiStop();
        //亮灯
        LiangDeng();

        GetRecord();
    }


    public bool IsGetRecordSelf = false;
    void GetRecord()
    {
        if (IsGetRecordSelf)
        {
            print("  记录自己！！！！！！！！！！     "+ this.transform.name);
            GlobalDateControl.SetMsgInCurrentGKDateAndSetInZGKDate(this.transform.name);
        }
    }


    void ChangeCameraKuai()
    {
        if (IsChangeKuai)
        {
            GameObject cm = GlobalTools.FindObjByName("MainCamera");
            //cm.GetComponent<CameraController>().GetBounds(Kuai, true);
            cm.GetComponent<CameraController>().SetNewPosition(new Vector3(cm.transform.position.x, cm.transform.position.y, -15));
        }
    }

    public GameObject deng;
    void LiangDeng()
    {
        if (deng) deng.SetActive(true);
    }


    TheTimer theTime = new TheTimer();
    void JishiStop()
    {
        theTime.GetStopByTime(3);
    }

    void BeStop()
    {
        StopYanmu();
        StopSound();
        ChangeCameraKuai();
        Destroy(this.gameObject);
    }

    //其他触发
    public bool IsOtherCF = true;


    //门怪事件触发
    public bool IsMenGuaiChufa = false;


    //播放震动 
    //播放震动声音
    void GetShock()
    {
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-3"), this);     
    }
    //播放震动灰尘
    void PlayYanmu()
    {
        if (_yanmu) _yanmu.Play();
    }

    void StopYanmu()
    {
        if (_yanmu) _yanmu.Stop();
    }

   
    //播放震动声音
    void PlaySound()
    {
        if (zdSound) zdSound.Play();
    }


    void StopSound()
    {
        if (zdSound) zdSound.Stop();
    }

    public bool IsHideFloor = false;
    //隐藏地板
    void HideFloor()
    {
        if (hideFloor) {
            hideFloor.SetActive(false);
            IsHideFloor = true;
        }
        
    }

    //播放震动动画 地板碎片散落
    void PlaySanluo()
    {
        if (suishi != null)
        {
            suishi.SetActive(true);
        }
    }



    // Update is called once per frame
    void Update () {
        if (theTime.isStart&&theTime.IsPauseTimeOver())
        {
            BeStop();
        }
	}



    void OnTriggerEnter2D(Collider2D Coll)
    {
        if (!IsOtherCF) return;
        if (Coll.tag == "Player")
        {
            //print("chufa !!!!!!!");
            BeStart();
        }
    }

    void OnTriggerExit2D(Collider2D Coll)
    {
        //print("Trigger - B");
        if (Coll.tag == "Player")
        {

        }
    }
    void OnTriggerStay2D(Collider2D Coll)
    {
        //print("Trigger - C");

    }
}
