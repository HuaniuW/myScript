using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Daojishi : MonoBehaviour
{
    [Header("倒计时 txt")]
    public Text Txt_Daojishi;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        IsChushi = false;
        Txt_Daojishi.gameObject.SetActive(false);
        //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DAOJISHI, "z2-0.8"), this);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.DAOJISHI, this.DaojishiStart);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.STOP_DAOJISHI, this.StopDJS);
    }

    private void StopDJS(UEvent evt)
    {
        //throw new NotImplementedException();
        IsStartDJS = false;
    }

    int TimesNums = 0;

    private void DaojishiStart(UEvent e)
    {
        Txt_Daojishi.gameObject.SetActive(true);
        TimesNums = int.Parse(e.eventParams.ToString());
        IsStartDJS = true;
        //throw new NotImplementedException();
    }

    bool IsStartDJS = false;
    float jishiNums = 0;

    float fen = 0;
    float miao = 0;
    string _miao = "00";
    bool IsChushi = false;
    void Jishi()
    {
        if (!IsStartDJS) return;

        if (!IsChushi)
        {
            IsChushi = true;
            jishiNums = 0;
            if (TimesNums > 0) TimesNums--;
            fen = TimesNums / 60 >> 0;
            miao = TimesNums % 60;
            _miao = miao < 10 ? "0" + miao : miao.ToString();
        }



        jishiNums += Time.deltaTime;
        float smiao = Mathf.Ceil(100 - jishiNums * 100);
        //print("-----------------jishiNums    "+ jishiNums+"   ????  "+smiao);
        
        string _smiao = smiao < 10 ? "0" + smiao : smiao.ToString();
        //print("  _smiao  "+ _smiao+ "  smiao?    "+ smiao);
        if (smiao <= 0) _smiao = "00";
        if (jishiNums >= 1)
        {
            jishiNums = 0;
            if(TimesNums>0) TimesNums--;
            fen = TimesNums / 60 >> 0;
            miao = TimesNums % 60;
            _miao = miao < 10 ? "0" + miao : miao.ToString();
        }
        string timeStr = "0" + fen + ":" + _miao + ":" + _smiao;
        Txt_Daojishi.text = timeStr;
        if (TimesNums == 0)
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_OVER, null), this);
        }
    }

    private void OnDestroy()
    {
        print(" 倒计时 被移除 ");
        IsStartDJS = false;
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.DAOJISHI, this.DaojishiStart);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.STOP_DAOJISHI, this.StopDJS);
    }

    // Update is called once per frame
    void Update()
    {
        Jishi();
    }
}
