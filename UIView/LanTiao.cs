using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanTiao : MonoBehaviour
{
    public Image xueBg;
    public Image xue1;
    public Image xue2;

    float _cLan = 1000;
    float _maxLan = 1000;
    public float _maxW = 10;
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

    // Use this for initialization
    void Start()
    {
        GetGameObj();
        SetXueTiao2();
        //_w2 = _w;
        GetXueNum(0);
        ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
    }

    private void OnDestroy()
    {
        ObjectEventDispatcher.dispatcher.removeEventListener(EventTypeName.CHANEG_LIVE, this.LiveChange);
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
        _maxLan = maxLive;
        _w = w;
        _w2 = _w;
        _h = h;
        SetXueTiao2();
    }

    RoleDate roleDate;
    //初始化 血条的 长度之类的
    void GetGameObj()
    {
        if (gameObj != null)
        {
            roleDate = gameObj.GetComponent<RoleDate>();
            _maxLan = roleDate.maxLan;
            roleDate.lan = roleDate.lan > roleDate.maxLan ? roleDate.maxLan : roleDate.lan;
            _cLan = roleDate.lan;
            GetXueNum(0);
            //isChage = true;
        }
    }

    void LiveChange(UEvent e)
    {
        GetGameObj();
    }

    void SetXueTiao2()
    {
        WhBg(xueBg);
        Wh(xue1, _w, _h);
        Wh(xue2, _w2, _h);
    }


    //参数 >= 0的时候 直接跳到结果 可以做为开场预设  +血和预设
    public void GetXueNum(float nums)
    {
        //_cLan = roleDate.live;
        _cLan = _cLan + nums > _maxLan ? _maxLan : _cLan + nums;
        if (_cLan < 0) _cLan = 0;
        _w = _cLan / _maxLan * _maxW;
        if (_w2 != _w) isChage = true;
        if (nums >= 0) _w2 = _w;
        Wh(xue1, _w, _h);
    }

    float lastXue = 0;
    void XueChange()
    {
        _cLan = roleDate.lan;
        if (_cLan == lastXue) return;
        if (_cLan < 0) _cLan = 0;
        _w = _cLan / _maxLan * _maxW;
        if (_w2 != _w) isChage = true;
        if (_w > _w2) _w2 = _w;
        Wh(xue1, _w, _h);
        lastXue = _cLan;
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
        if (!gameObj)
        {
            gameObj = GlobalTools.FindObjByName("player");
            GetGameObj();
            SetXueTiao2();
            //_w2 = _w;
            GetXueNum(0);
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
