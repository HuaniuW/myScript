﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class XuePingBox : CanTouchBox
{

    public Text XP_numText;
    public Image xueping;
    int XP_num = 0;
    // Use this for initialization
    void Start () {
        if (GlobalSetDate.instance.CurrentUserDate != null) XP_num = GlobalSetDate.instance.CurrentUserDate.xp_nums;
        InitNums();
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_XP, this.GetXP);
    }



    //当鼠标按下时调用 接口对应  IPointerDownHandler
    override public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = imgReduceScale;
        UseXuePing();
    }

    //当鼠标抬起时调用  对应接口  IPointerUpHandler
    override public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().localScale = imgNormalScale;
    }

    public int GetXPNums() {
        return XP_num;
    }

    void InitNums()
    {
        if(XP_num == 0)
        {
            GlobalTools.CanvasGroupAlpha(xueping.GetComponent<CanvasGroup>(), 0);
            XP_numText.gameObject.SetActive(false);
        }
        else
        {
            GlobalTools.CanvasGroupAlpha(xueping.GetComponent<CanvasGroup>(), 1);
            XP_numText.gameObject.SetActive(true);
            if (XP_numText != null) XP_numText.text = XP_num.ToString();
        }
        
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_XP, this.GetXP);
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(Globals.USE_XP))
        {
            UseXuePing();
        }
    }

    void UseXuePing()
    {
        if (Globals.isInPlot) return;
        if (GlobalSetDate.instance.IsChangeScreening) return;
        //FindNearestQR("up");
        if (XP_num > 0)
        {
            XP_num -= 1;
            InitNums();
            if (XP_numText != null) XP_numText.text = XP_num.ToString();
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.JIAXUE), this);
        }
    }

    void GetXP(UEvent e)
    {
        int nums = (int)e.eventParams;
        if (nums == 1)
        {
            XP_num += nums;
            XP_num = XP_num >= 5 ? 5 : XP_num;
        } else if (nums>1) {
            XP_num = nums;
        }else if (nums == -1)
        {
            XP_num += nums;
        }
        
        InitNums();
    }

  
}
