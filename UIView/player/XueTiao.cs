using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XueTiao : MonoBehaviour {
    public Image XueTiaoDi;
    public Image xueBg;
    [Header("诅咒血条背景")]
    public Image zzXueBg;
    public Image xue1;
    public Image xue2;

    public Image MaskImg;



    [Header("诅咒血条阻挡")]
    public Image zzTiao;


    protected float _cLive = 1000;
    protected float _maxLive = 1000;
    [Header("血条最大值的宽度")]
    public float _maxW = 10;
    protected float BaseMaxW = 0; //基础最大宽度
    protected float BaseDiW = 0;
    /// <summary>
    /// 长条的第一层显示
    /// </summary>
    public float _w=10;
    /// <summary>
    /// 长条的第二条显示 缓动跟随_w
    /// </summary>
    protected float _w2 = 10;
    public float _h=10;
    public GameObject gameObj;

    public bool isCanAddMaxLiveNum = true;

    public static int n = 1;
    // Use this for initialization
    void Start () {
        //print("被调用次数   血条-----****************************************************************************************************************************   " + n);
        n++;
        //初始化 血条显示
        LiveBarInit();
        //获取到 标的角色 信息  这里要注意boss的怎么获取
        GetGameObj();
        //初始化 数据显示
        GetXueNum(0);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GET_ZUZHOU, this.IsHasZZ);

        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.XUETIAO_ZHIJIEGENSUI, this.GenSuiXuetiaoW2ToPos);
        //AddMaxLiveBar(200);
    }

    void OnEable()
    {
        
    }

    [Header("血条显示误差 默认为50")]
    public float XueTiaoXianshiWucha = 50;
    protected virtual void LiveBarInit()
    {
        float xueTiaoDiW = XueTiaoDi.GetComponent<RectTransform>().rect.width;
        _maxW = xueTiaoDiW - XueTiaoXianshiWucha;
        _w = _w2 = _maxW;
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        WhBg(xueBg);
        if (zzTiao) WhBg(zzTiao);
        if (zzXueBg) Wh(zzXueBg, _maxW * 0.3f, _h);
        Wh(zzXueBg, _maxW, _h);
        Wh(xue1, _maxW, _h);
        Wh(xue2, _maxW, _h);
        //是否包含诅咒条 有的话先初始为不显示
        if (zzTiao) GlobalTools.CanvasGroupAlpha(zzTiao.GetComponent<CanvasGroup>(), 0);
    }

    //角色那边怎么做 如果加生命最大值 
    //基础长度 和判断  血条是否能被拉伸

    float OldMaxLive = 0;
    float NewMaxW = 0;
    float OldMaxW = 0;

    /// <summary>
    /// 增加最大生命上限
    /// </summary>
    /// <param name="AddLivesNum">增加的生命上限值</param>
    public virtual void AddMaxLiveBar(float AddLivesNum)
    {

        OldMaxLive = _maxLive;
        OldMaxW = _maxW;
        _maxLive += AddLivesNum;
        if (!isCanAddMaxLiveNum) return;
        
        NewMaxW = OldMaxW * _maxLive / OldMaxLive;
        _maxW = NewMaxW;
        Wh(XueTiaoDi, _maxW+50, XueTiaoDi.GetComponent<RectTransform>().rect.height);
        Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
        if (zzTiao) WhBg(zzTiao);
        if (zzXueBg) Wh(zzXueBg, _maxW * 0.3f, _h);
        WhBg(xueBg);
    }


    void OnDestroy()
    {
        //print("血条 我被销毁了？？");
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
        //是否有诅咒条
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.GET_ZUZHOU, this.IsHasZZ);
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.XUETIAO_ZHIJIEGENSUI, this.GenSuiXuetiaoW2ToPos);
    }


    public void showUIByAlpha(float alphaNum)
    {
        GlobalTools.CanvasGroupAlpha(GetComponent<CanvasGroup>(), alphaNum);
    }



    protected void IsHasZZ(UEvent e) {
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
        //print("GetTargetObj    "+obj);
        if (obj) {
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
        //print("--------------------->3");
        WhBg(xueBg);
        if (zzTiao) WhBg(zzTiao);
        if (zzXueBg) Wh(zzXueBg, _w * 0.3f, _h);
        Wh(xue1, _w, _h);
        Wh(xue2, _w2, _h);
    }

    protected RoleDate roleDate;
    //初始化 血条的 长度  数据匹配到链接的角色
    public virtual void GetGameObj(RoleDate _roleDate = null)
    {
        if (gameObj != null)
        {
            //gameObj = GlobalTools.FindObjByName("player");
            //print("   gameObjName "+gameObj.name);
           
            if (_roleDate != null){
                roleDate = _roleDate;
                //print(" @@ _roleDate  "+ _roleDate.live);
            }
            else
            {
                roleDate = gameObj.GetComponent<RoleDate>();
            }
            
            //print("roleDate------------->   "+ roleDate);
            _maxLive = roleDate.maxLive;
            print(gameObj.name+"  当前xuel "+ roleDate.live+"   max "+roleDate.maxLive);
            //print("111111111  当前 血量是多少   " + _cLive + "   传进来的 当前血量roleDate.live  " + roleDate.live);
            //_cLive = _cLive > roleDate.maxLive ? roleDate.maxLive : _cLive;
            _cLive = roleDate.live;
            //print("最大血上线xue change!  roleDate.maxLive    " + roleDate.maxLive + "  _maxLive " + _maxLive + "   传进来数据  " + e.eventParams + "    ---clive " + _cLive + "    ----roledateLive " + roleDate.live);
            print("  当前 血量是多少   "+_cLive+ "   传进来的 当前血量roleDate.live  "+ roleDate.live);
            //_cLive = roleDate.live;
            roleDate.live = _cLive;
            print("roledetelive   "+roleDate.live);
            GetXueNum(0);
            //isChage = true;
        }
    }


    //void OnGUI()
    //{
    //    if (Globals.isDebug)
    //    {
    //        if(roleDate!=null) GUI.TextArea(new Rect(0, 160, 450, 40), "liveBar   : " + "  _maxLive  " + _maxLive + "  IsInJijiaGK?   " + Globals.IsInJijiaGK+"   roledateMax   "+ roleDate.maxLive+ " _maxW "+ _maxW+ " NewMaxW  "+ NewMaxW+ "  OldMaxLive   " + OldMaxLive+ " _maxLive  " + _maxLive);
    //    }
    //}

    protected virtual void LiveChange(UEvent e)
    {
        
        if (gameObj &&gameObj.tag != GlobalTag.Player) return;
        if (gameObj == null)
        {
            gameObj = GlobalTools.FindObjByName("player");
            //print("  gameObj  "+gameObj.name);
            //GetGameObj();
            if (gameObj == null)
            {
                gameObj = GlobalTools.FindObjByName("player_jijia");
            }
        }
        
       

        roleDate = gameObj.GetComponent<RoleDate>();
        //print("XT  gameObj>  " + gameObj);

        bool isHZChange = false;
        float NewMaxLive = _maxLive;

        if (gameObj != null)
        {
            print("  name " + gameObj.name);
            print("最大血上线xue change!  roleDate.maxLive    " + roleDate.maxLive + "  _maxLive " + _maxLive + "   传进来数据  " + e.eventParams + "    ---_clive " + _cLive + "    ----roledateLive " + roleDate.live);
            //_cLive = int.Parse(e.eventParams.ToString());
            print("  传进来的 clive  "+ e.eventParams.ToString());
            string liveStr = e.eventParams.ToString();
            _cLive = roleDate.live;
            if (liveStr.Split('_').Length >= 2)
            {
                NewMaxLive = float.Parse(liveStr.Split('_')[0]);
                isHZChange = true;
            }
        }


        roleDate.live = _cLive;
        print("2222222roledetelive   " + roleDate.live);
        //**********下面 两行交换了 顺序  上次 也是这里 出了其他问题 但是 这次换回来了 注意一下**********  如果先记录 就会导致 带徽章时候 当前血量变回1000的 初始值
        GlobalSetDate.instance.GetScreenChangeDate();
        if (isHZChange) GlobalSetDate.instance.ScreenChangeDateRecord();
        
        if (gameObj == null) return;
        if (gameObj.tag == GlobalTag.Player && roleDate)
        {
            AddMaxLiveBar(NewMaxLive - _maxLive);
            //print(" ???@roledate   "+roleDate.live);
            GetGameObj(roleDate);
        }

        //print("333333333333333roledetelive   " + roleDate.live);

    }


    //参数 >= 0的时候 直接跳到结果 可以做为开场预设  +血和预设
    public virtual void GetXueNum(float nums)
    {
        //_cLive = roleDate.live;
        _cLive = _cLive+nums > _maxLive?_maxLive: _cLive + nums;
        if (_cLive < 0) _cLive = 0;
         _w  = _cLive / _maxLive*_maxW;
        if (_w2 != _w) isChage = true;
        if (nums >= 0) _w2 = _w;
        Wh(xue1, _w, _h);
    }





    //protected float testNums = 1000;

    protected float lastXue = 0;
    // 血效果根据 标的角色数据变化
    protected virtual void XueChange() {
        //print("hiiiii     "+_maxLive+ "   roleDate.maxLive   "+roleDate.maxLive);
        //if (_maxLive != roleDate.maxLive)
        //{
        //    print(gameObj.tag);
        //    if (gameObj.tag == "Player" && roleDate) AddMaxLiveBar(roleDate.maxLive-_maxLive);
        //}
        
        //_cLive = testNums;
        _cLive = roleDate.live;
        //print(gameObj.name+" roleDate.live    "+ roleDate.live);
        if (gameObj.tag!=GlobalTag.Player&& _cLive <= 0) this.gameObject.SetActive(false);
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


    protected bool isChage = false;
    //缓动跟随效果
    protected void Xue2W()
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

    public bool IsPlayerXuetiao = false;
    //场景切换 跟随的 血条直接到位置
    public void GenSuiXuetiaoW2ToPos(UEvent e)
    {
        
        if (!IsPlayerXuetiao) return;
        //print("血条 直接 跟随！！！"+_w2+"   w "+_w+"  血量 "+ roleDate.live);
        XueChange();
        _w2 = _w;
        Wh(xue2, _w2, _h);
    }



    // Update is called once per frame
    void Update () {
        //Test();
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            if (gameObj == null)
            {
                gameObj = GlobalTools.FindObjByName("player_jijia");
                AddMaxLiveBar(2000 - _maxLive);
            }
            GetGameObj();
            return;
        }
        XueChange();
        if (isChage)Xue2W();
    }

    //void Test()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {    //首先判断是否点击了鼠标左键
    //        print("游戏开始！");
    //        testNums -= 100;
    //        //_maxW = 450;
    //        //Wh(MaskImg, _maxW, MaskImg.GetComponent<RectTransform>().rect.height);
    //        //Wh(XueTiaoDi, 500, XueTiaoDi.GetComponent<RectTransform>().rect.height);
            
    //        //GetXueNum(0);
    //    }else if (Input.GetMouseButtonDown(1))
    //    {
    //        testNums += 100;
    //        if (testNums > _maxLive) testNums = _maxLive;
    //    }
    //}




    protected void Wh(Image obj, float w, float h = 10)
    {
        if (obj == null) return;
        var rt = obj.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
    }

    protected void WhBg(Image obj)
    {
        if (obj == null) return;
        var rt = obj.GetComponent<RectTransform>();
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxW + 4);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _h + 4);
    }
}


