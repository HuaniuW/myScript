using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Nengliangtiao : UI_lantiao
{
    // Start is called before the first frame update
    void Start()
    {
        LiveBarInit();
        //获取到 标的角色 信息  这里要注意boss的怎么获取
        GetGameObj();
        //初始化 数据显示
        GetXueNum(0);
        NengliangNums = 1000;
        //HideSelf();
    }


    public override void GetGameObj(RoleDate _roleDate = null)
    {
        if (gameObj != null)
        {
            //print("gameObj-------------->    " + gameObj);
            //roleDate = gameObj.GetComponent<RoleDate>();
            //print("roleDate------------->   "+ roleDate);
            _maxLive = 1000;
            roleDate.lan = roleDate.lan > roleDate.lan ? roleDate.maxLan : roleDate.lan;
            _cLive = roleDate.lan;
            GetXueNum(0);
            //isChage = true;
            //SetLanText(roleDate.maxLan, roleDate.lan);
            //print("蓝 "+roleDate.maxLan+"  clan "+roleDate.lan);
        }
    }

    float NengliangNums = 1000;

    float jishi = 0.2f;
    float jishiNums = 0;
    bool StartNengliang = false;

    public float GetNengliang()
    {
        return NengliangNums;
    }


    float NengliangXiaohaoNums = 0.08f;

    public void XiaohaoNengliang()
    {
        //print("  >>????????  " + NengliangNums);
        NengliangXiaohaoNums = 0.08f;
        StartNengliang = true;
        
    }

    public void XiaohaoNengliangNengliangPao()
    {
        //print("  >>????????  " + NengliangNums);
        NengliangXiaohaoNums = 1f;
        StartNengliang = true;

    }


    public void StopXiaohaoNengliang()
    {
        StartNengliang = false;
    }

    void NengliangXiaohao()
    {
        if (!StartNengliang) {
            jishiNums = 0;
            return;
        }
        jishiNums += Time.deltaTime;
        if (jishiNums >= jishi)
        {
            NengliangNums-= NengliangXiaohaoNums;
        }

        if (NengliangNums <= 0)
        {
            NengliangNums = 0;
            return;
        }
    }

    void Update()
    {
        if (StartNengliang) {
            NengliangXiaohao();
            XueChange();
        }
        
    }

    protected override void XueChange()
    {
        //print("NengliangNums    "+ NengliangNums);
        _cLive = NengliangNums;
        //if (gameObj.tag != "Player" && _cLive <= 0) this.gameObject.SetActive(false);
        if (_cLive == lastXue) return;
        if (_cLive < 0) _cLive = 0;
        _w = _cLive / _maxLive * _maxW;
        if (_w2 != _w) isChage = true;
        if (_w > _w2) _w2 = _w;
        if (_w < 0) _w = 0;
        if (_w > _maxW) _w = _maxW;
        Wh(xue1, _w, _h);
        lastXue = _cLive;
    }

    //参数 >= 0的时候 直接跳到结果 可以做为开场预设  +血和预设
    public override void GetXueNum(float nums)
    {
        //_cLive = roleDate.live;
        _cLive = _cLive + nums > _maxLive ? _maxLive : _cLive + nums;
        if (_cLive < 0) _cLive = 0;
        _w = _cLive / _maxLive * _maxW;
        if (_w2 != _w) isChage = true;
        if (nums >= 0) _w2 = _w;
        Wh(xue1, _w, _h);
    }

    protected override void LiveBarInit()
    {
        float xueTiaoDiW = XueTiaoDi.GetComponent<RectTransform>().rect.width;
        _maxW = xueTiaoDiW - XueTiaoXianshiWucha;
        _w = _w2 = _maxW;
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        WhBg(xueBg);
        if (zzTiao) WhBg(zzTiao);
        //if (zzXueBg) Wh(zzXueBg, _maxW * 0.3f, _h);
        //Wh(zzXueBg, _maxW, _h);
        Wh(xue1, _maxW, _h);
        //Wh(xue2, _maxW, _h);
        //是否包含诅咒条 有的话先初始为不显示
        //if (zzTiao) GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 0);
    }
}
