using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyXueTiao : MonoBehaviour
{
    public Image XueTiaoDi;
    public Image xueBg;
    //[Header("诅咒血条背景")]
    //public Image zzXueBg;
    public Image xue1;
    public Image xue2;

    public Image MaskImg;



    //[Header("诅咒血条阻挡")]
    //public Image zzTiao;


    float _cLive = 1000;
    float _maxLive = 1000;
    [Header("血条最大值的宽度")]
    public float _maxW = 10;
    float BaseMaxW = 0; //基础最大宽度
    float BaseDiW = 0;
    /// <summary>
    /// 长条的第一层显示
    /// </summary>
    public float _w = 10;
    /// <summary>
    /// 长条的第二条显示 缓动跟随_w
    /// </summary>
    float _w2 = 10;
    public float _h = 10;
    public GameObject gameObj;

    public bool isCanAddMaxLiveNum = true;

    public static int n = 1;
    // Use this for initialization
    void Start()
    {
        //print("被调用次数   "+n);
        n++;
        //初始化 血条显示
        LiveBarInit();
        //获取到 标的角色 信息  这里要注意boss的怎么获取
        GetGameObj();
        //初始化 数据显示
        GetXueNum(0);
    }

    [Header("血条显示误差 默认为50")]
    public float XueTiaoXianshiWucha = 50;
    void LiveBarInit()
    {
        float xueTiaoDiW = XueTiaoDi.GetComponent<RectTransform>().rect.width;
        _maxW = xueTiaoDiW - XueTiaoXianshiWucha;
        _w = _w2 = _maxW;
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        WhBg(xueBg);
        //if (zzTiao) WhBg(zzTiao);
        //if (zzXueBg) Wh(zzXueBg, _maxW * 0.3f, _h);
        //Wh(zzXueBg, _maxW, _h);
        Wh(xue1, _maxW, _h);
        Wh(xue2, _maxW, _h);
        //是否包含诅咒条 有的话先初始为不显示
        //if (zzTiao) GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 0);
    }

    //角色那边怎么做 如果加生命最大值 
    //基础长度 和判断  血条是否能被拉伸

    /// <summary>
    /// 增加最大生命上限
    /// </summary>
    /// <param name="AddLivesNum">增加的生命上限值</param>
    public void AddMaxLiveBar(float AddLivesNum)
    {

        float OldMaxLive = _maxLive;
        print("OldMaxLive " + OldMaxLive);
        _maxLive += AddLivesNum;
        print("_maxLive " + _maxLive);
        if (!isCanAddMaxLiveNum) return;
        float NewMaxW = 0;
        NewMaxW = _maxW * _maxLive / OldMaxLive;
        _maxW = NewMaxW;
        Wh(XueTiaoDi, _maxW + 50, XueTiaoDi.GetComponent<RectTransform>().rect.height);
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        WhBg(xueBg);
    }


    private void OnDestroy()
    {
    }
    


    //是否激活
    public void Show(bool isAddOnStage)
    {
        this.gameObject.SetActive(isAddOnStage);
    }

    public void GetTargetObj(GameObject obj)
    {
        print("GetTargetObj    " + obj);
        if (obj)
        {
            gameObj = obj;
            GetGameObj();
        }
    }

    //备用 主动控制血条长宽高的方法
    public void SetXueTiao(float w, float maxLive, float h = 10)
    {
        _maxLive = maxLive;
        _w = w;
        _w2 = _w;
        _h = h;
        WhBg(xueBg);
        Wh(xue1, _w, _h);
        Wh(xue2, _w2, _h);
    }

    RoleDate roleDate;
    //初始化 血条的 长度  数据匹配到链接的角色
    public void GetGameObj()
    {
        if (gameObj != null)
        {
            roleDate = gameObj.GetComponent<RoleDate>();
            _maxLive = roleDate.maxLive;
            roleDate.live = roleDate.live > roleDate.maxLive ? roleDate.maxLive : roleDate.live;
            _cLive = roleDate.live;
            GetXueNum(0);
        }
    }

  


    //参数 >= 0的时候 直接跳到结果 可以做为开场预设  +血和预设
    public void GetXueNum(float nums)
    {
        //print("血条长度控制！！");
        _cLive = _cLive + nums > _maxLive ? _maxLive : _cLive + nums;
        if (_cLive < 0) _cLive = 0;
        _w = _cLive / _maxLive * _maxW;
        if (_w2 != _w) isChage = true;
        if (nums >= 0) _w2 = _w;
        Wh(xue1, _w, _h);
    }

    [Header("是否需要 die后 隐藏掉 血条")]
    public bool IsDieNeedHideBar = true;


    float testNums = 1000;

    float lastXue = 0;
    // 血效果根据 标的角色数据变化
    void XueChange()
    {
        _cLive = roleDate.live;
        if (_cLive == lastXue) return;
        if (_cLive <= 0) {
            _cLive = 0;
            if (IsDieNeedHideBar)
            {
                Show(false);
            }
        }
        
        _w = _cLive / _maxLive * _maxW;
        if (_w2 != _w) isChage = true;
        if (_w > _w2) _w2 = _w;
        if (_w < 0) _w = 0;
        if (_w > _maxW) _w = _maxW;
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
        else
        {
            _w2 = _w;
        }

        Wh(xue2, _w2, _h);

        if (_w2 == _w)
        {
            isChage = false;
        }
    }



    // Update is called once per frame
    void Update()
    {
        //Test();
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            GetGameObj();
            return;
        }
        XueChange();
        if (isChage) Xue2W();
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


