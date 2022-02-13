using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_lantiao : XueTiao
{
    public Text LanText;

    // Start is called before the first frame update
    void Start()
    {
        LiveBarInit();
        //获取到 标的角色 信息  这里要注意boss的怎么获取
        GetGameObj();
        //初始化 数据显示
        GetXueNum(0);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANEG_LAN, this.LanChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
    }
    [Header("**** 数值条")]
    public GameObject Bar;

    public virtual void HideSelf()
    {
        Bar.gameObject.SetActive(false);

    }

    public virtual void ShowSelf()
    {
        Bar.gameObject.SetActive(true);
    }



    void ChangeHun(UEvent e)
    {
        //hun_num_text.text = e.eventParams.ToString();
        if (roleDate == null) return;
        SetLanText(roleDate.maxLan, roleDate.lan);
        if (GlobalTools.FindObjByName("player"))
        {
            GlobalSetDate.instance.CurrentUserDate.curLan = GlobalTools.FindObjByName("player").GetComponent<RoleDate>().Lan.ToString();
        }
        else
        {
            GlobalSetDate.instance.CurrentUserDate.curLan = GlobalTools.FindObjByName("player_jijia").GetComponent<RoleDate>().Lan.ToString();
        }
        

        //GetComponent<UIShake>().GetShake();
    }

    void OnDestroy()
    {
        //print("血条 我被销毁了？？");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
        //是否有诅咒条
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ZUZHOU, this.IsHasZZ);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANGE_HUN, this.ChangeHun);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            GetGameObj();
            return;
        }
        XueChange();
    }

    protected override void XueChange()
    {
        _cLive = roleDate.lan;
        if (gameObj.tag != "Player" && _cLive <= 0) this.gameObject.SetActive(false);
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

    protected void LanChange(UEvent e)
    {
        //return;
        if (gameObj == null)
        {
            gameObj = GlobalTools.FindObjByName("player");
            if (gameObj==null)
            {
                gameObj = GlobalTools.FindObjByName("player_jijia");
            }
            roleDate = gameObj.GetComponent<RoleDate>();
            //print("roleDate%%%%    "+ roleDate.live);
            //GetGameObj();
        }
        //print("XT  gameObj>  " + gameObj);

        //print("roleDate%%%%    " + roleDate.live);
        //print("蓝量Change change!  roleDate.maxLive    " + roleDate.maxLan + "  _maxLive " + _maxLive + "   传进来数据  " + e.eventParams);
        //GlobalSetDate.instance.ScreenChangeDateRecord();
        GlobalSetDate.instance.GetScreenChangeDate();
        //print("roleDate%%%%    " + roleDate.live);
        if (gameObj == null) return;
        if (gameObj.tag == "Player" && roleDate)
        {
            AddMaxLiveBar((float)e.eventParams - _maxLive);
            GetGameObj();
        }
    }

    public override void AddMaxLiveBar(float AddLivesNum)
    {
        if (XueTiaoDi == null) return;
        float OldMaxLive = _maxLive;
        _maxLive += AddLivesNum;
        if (!isCanAddMaxLiveNum) return;
        float NewMaxW = 0;
        NewMaxW = _maxW * _maxLive / OldMaxLive;
        _maxW = NewMaxW;
        //print(" XueTiaoDi  "+ XueTiaoDi);
        Wh(XueTiaoDi, _maxW + 50, XueTiaoDi.GetComponent<RectTransform>().rect.height);
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        //if (zzTiao) WhBg(zzTiao);
        //if (zzXueBg) Wh(zzXueBg, _maxW * 0.3f, _h);
        WhBg(xueBg);
    }

    public override void GetGameObj(RoleDate _roleDate = null)
    {
        if (gameObj != null)
        {
            //print("gameObj-------------->    " + gameObj);
            roleDate = gameObj.GetComponent<RoleDate>();
            //print("roleDate------------->   "+ roleDate);
            _maxLive = roleDate.maxLan;
            roleDate.lan = roleDate.lan > roleDate.lan ? roleDate.maxLan : roleDate.lan;
            _cLive = roleDate.lan;
            GetXueNum(0);
            //isChage = true;
            SetLanText(roleDate.maxLan, roleDate.lan);
            //print("蓝 "+roleDate.maxLan+"  clan "+roleDate.lan);
        }
    }

    public string LanStr = "100/100";


    protected void SetLanText(float MaxLan,float cLan)
    {
        if (LanText == null) return;
        LanText.text = cLan + "/" + MaxLan;
        LanStr = LanText.text;
        //print("蓝 "+LanText.text);
    }

}
