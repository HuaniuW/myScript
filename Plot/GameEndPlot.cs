using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameEndPlot : MonoBehaviour
{
    //最后boss 打败火龙后的 剧情

    [Header("打败 龙后 全局 光照")]
    public Light2D GlobalLight;



    float jishi = 0;
    float endJishi = 7;

    void End()
    {
        jishi += Time.deltaTime;

        if (jishi <= endJishi)
        {
            return;
        }

        if (PlayerUI&& PlayerUI.activeSelf)
        {
            Globals.isInPlot = true;
            //PlayerUI.SetActive(false);
            PlayerUI.GetComponent<CanvasGroup>().alpha = 0;
        }

       

        if (GlobalLight.GetComponent<Light2D>().intensity>=300)
        {
            print("游戏通关！！！！！");
            //进入另一个 场景
            PlayerUI.GetComponent<CanvasGroup>().alpha = 1;
            PlayerUI.GetComponent<PlayerUI>().HideUIs();
            Men.SetActive(true);
            Globals.isInPlot = false;
            return;
        }


        GlobalLight.GetComponent<Light2D>().intensity += (302 - GlobalLight.GetComponent<Light2D>().intensity) * 0.01f;
    }

    [Header("最后场景的门")]
    public GameObject Men;

    GameObject PlayerUI;



    // Start is called before the first frame update
    void Start()
    {
        PlayerUI = GlobalTools.FindObjByName(GlobalTag.PLAYERUI);
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.BOSS_IS_DIE, this.name), this);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.BOSS_IS_DIE,this.IsAllOver);
    }

    private void IsAllOver(UEvent evt)
    {
        //throw new NotImplementedException();
        print("bossdie");
        IsEnd = true;
        if (evt.eventParams.ToString() == "B_huolong")
        {

        }
    }

    bool IsEnd = false;

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.BOSS_IS_DIE, this.IsAllOver);
    }

    // Update is called once per frame
    void Update()
    {
        IsLongHasDie();
        if (IsEnd)End();
    }

    public GameObject Long;
    void IsLongHasDie()
    {
        if (!IsEnd)
        {
            if(Long == null)
            {
                IsEnd = true;
            }
        }
    }

}
