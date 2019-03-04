using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XueTiao : MonoBehaviour {
    public Image xueBg;
    [Header("诅咒血条背景")]
    public Image zzXueBg;
    public Image xue1;
    public Image xue2;



    [Header("诅咒血条阻挡")]
    public Image zzTiao;


    float _cLive = 1000;
    float _maxLive = 1000;
    public float _maxW = 10;
    /// <summary>
    /// 长条的第一层显示
    /// </summary>
    public float _w=10;
    /// <summary>
    /// 长条的第二条显示 缓动跟随_w
    /// </summary>
    float _w2 = 10;
    public float _h=10;
    public GameObject gameObj;

    public static int n = 1;
    // Use this for initialization
    void Start () {
        //print("被调用次数   "+n);
        n++;
        //print(" zzTiao  "+ zzTiao);
        //print(" zzXueBg  " + zzXueBg);
        if(zzTiao) GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 0);
        GetGameObj();
        //print("--------------------->1");
        SetXueTiao2();
        
        //_w2 = _w;
        GetXueNum(0);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ZUZHOU, this.IsHasZZ);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ZUZHOU, this.IsHasZZ);
    }

    void IsHasZZ(UEvent e) {
        if (zzTiao == null) return;
        if ((bool)e.eventParams)
        {
            GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 1);
        }
        else
        {
            GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 0);
        }
    }
    

    //是否激活
    public void Show(bool isAddOnStage)
    {
        this.gameObject.SetActive(isAddOnStage);
    }

    public void GetTargetObj(GameObject obj)
    {
        if (obj) gameObj = obj;
    }

    //备用 主动控制血条长宽高的方法
    public void SetXueTiao(float w, float maxLive, float h = 10)
    {
        _maxLive = maxLive;
        _w = w;
        _w2 = _w;
        _h = h;
        //print("--------------------->3");
        SetXueTiao2();
    }

    RoleDate roleDate;
    //初始化 血条的 长度之类的
    void GetGameObj()
    {
        if (gameObj != null)
        {
            roleDate = gameObj.GetComponent<RoleDate>();
            _maxLive = roleDate.maxLive;
            roleDate.live = roleDate.live > roleDate.maxLive ? roleDate.maxLive : roleDate.live;
            _cLive = roleDate.live;
            GetXueNum(0);
            //isChage = true;
        }
    }

    void LiveChange(UEvent e)
    {
        //print("xue change!");
        GetGameObj();
    }

    void SetXueTiao2()
    {
        WhBg(xueBg);
        if(zzTiao) WhBg(zzTiao);
        if(zzXueBg) Wh(zzXueBg,_w*0.3f,_h);
        Wh(xue1,_w,_h);
        Wh(xue2,_w2,_h);
    }


    //参数 >= 0的时候 直接跳到结果 可以做为开场预设  +血和预设
    public void GetXueNum(float nums)
    {
        //_cLive = roleDate.live;
        _cLive = _cLive+nums > _maxLive?_maxLive: _cLive + nums;
        if (_cLive < 0) _cLive = 0;
         _w  = _cLive / _maxLive*_maxW;
        if (_w2 != _w) isChage = true;
        if (nums >= 0) _w2 = _w;
        Wh(xue1, _w, _h);
    }

    float lastXue = 0;
    void XueChange() {
        _cLive = roleDate.live;
        if (_cLive == lastXue) return;
        if (_cLive < 0) _cLive = 0;
        _w = _cLive / _maxLive * _maxW;
        if (_w2 != _w) isChage = true;
        if (_w > _w2) _w2 = _w;
        Wh(xue1, _w, _h);
        lastXue = _cLive;
    }


    bool isChage = false;
    //缓动跟随效果
    void Xue2W()
    {
        if (_w2 > _w)
        {
            _w2 += (_w - _w2) * 0.04f;
            if (_w2 - _w < -2)
            {
                _w2 = _w;
            }
        }
        else {
            _w2 = _w;
        }
        
        Wh(xue2, _w2, _h);

        if(_w2 == _w)
        {
            isChage = false;
        }
    }



    // Update is called once per frame
    void Update () {
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            GetGameObj();
            //print("--------------------->2");
            //SetXueTiao2();
            //_w2 = _w;
            //GetXueNum(0);
            return;
        }
        XueChange();
        if (isChage)Xue2W();
    }






    void Wh(Image obj, float w, float h = 10)
    {
        var rt = obj.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }

    void WhBg(Image obj)
    {
        var rt = obj.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxW + 4);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _h + 4);
    }
}
