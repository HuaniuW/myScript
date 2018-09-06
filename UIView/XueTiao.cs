using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XueTiao : MonoBehaviour {
    public Image xueBg;
    public Image xue1;
    public Image xue2;

    float _cLive = 1000;
    float _maxLive=1000;
    public float _maxW = 10;
    public float _w=10;
    float _w2 = 10;
    public float _h=10;
    public GameObject gameObj;

    // Use this for initialization
    void Start () {
        GetGameObj();
        SetXueTiao2();
        //_w2 = _w;
        GetXueNum(0);
    }

    RoleDate roleDate;
    void GetGameObj()
    {
        if (gameObj != null)
        {
            roleDate = gameObj.GetComponent<RoleDate>();
            _maxLive = roleDate.maxLive;
            _cLive = roleDate.live;
        }
    }

    public void SetXueTiao(float w, float maxLive, float h = 10)
    {
        _maxLive = maxLive;
        _w = w;
        _w2 = _w;
        _h = h;
        SetXueTiao2();
    }

    void SetXueTiao2()
    {
        WhBg(xueBg);
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
    void Xue2W()
    {
        if (_w2 > _w)
        {
            _w2 += (_w - _w2) * 0.1f;
            if (_w2 - _w < -5)
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
        //print(_cLive);
        //if(gameObj.transform.localScale.x >0)
        //{
        //    this.transform.localScale = new Vector3(1, 1, 1);
        //}
        //else 
        //{
        //    this.transform.localScale = new Vector3(-1, 1, 1);
        //}
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
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _w + 4);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _h + 4);
    }
}
