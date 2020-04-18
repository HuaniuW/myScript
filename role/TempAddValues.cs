using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAddValues : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }




    //----------------------------------------------------临时提高硬直------------------------

    bool IsAddTempYZ = false;
    float AddYingzhiNums = 0;
    float TempYZTimes = 0;
    float TempYZJiShi = 0;
    public virtual void TempAddYZ(float AddYZNum, float TempTime = 1)
    {
        AddYingzhiNums = AddYZNum;
        TempYZTimes = TempTime;
        IsAddTempYZ = true;
        TempYZJiShi = 0;
        GetComponent<RoleDate>().addYZ(AddYingzhiNums);
    }

    protected virtual void AddTempYZ()
    {
        if (IsAddTempYZ)
        {
            TempYZJiShi += Time.deltaTime;
            if (TempYZJiShi >= TempYZTimes)
            {
                if (IsAddTempYZ)
                {
                    GetComponent<RoleDate>().hfYZ(AddYingzhiNums);
                    IsAddTempYZ = false;
                    TempYZJiShi = 0;
                    AddYingzhiNums = 0;
                    TempYZTimes = 0;
                }
                
            }
        }

    }



    //----------------------------------------------------悬空------------------------


    //是否悬空
    bool IsTempXK = false;
    //悬空时间
    float _XTTimes = 0;
    //原始重力值
    float _YSZhonglI = 0;
    //临时悬空计时
    float TempXKJiShi = 0;
    //临时悬空
    public virtual void TempXuanKong(float XTTimes)
    {
        _XTTimes = XTTimes;
        TempXKJiShi = 0;
        _YSZhonglI = GetComponent<Rigidbody2D>().gravityScale;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        IsTempXK = true;
    }

    void XuanKongTemp()
    {

        if (GetComponent<RoleDate>().isBeHiting || GetComponent<RoleDate>().isDie)
        {
            //print("悬停的时候 被攻击");
            XTReSet();
            return;
        }

        if (IsTempXK)
        {
            TempXKJiShi+= Time.deltaTime;
            if(TempXKJiShi>= _XTTimes)
            {
                XTReSet();
            }
        }
    }

    void XTReSet()
    {
        if (IsTempXK)
        {
            IsTempXK = false;
            GetComponent<Rigidbody2D>().gravityScale = _YSZhonglI;
            _YSZhonglI = 0;
            TempXKJiShi = 0;
            _XTTimes = 0;
        }
        
    }



    //----------------------------------------------------临时提高减伤比例------------------------
    bool IsTempJianShangbili = false;
    float _jsBl = 0;
    //计时持续时间
    float _JSCXTimes = 0;
    //计时器
    float _TempJSJSQ = 0;
    //记录原来的 减伤比例  用来时间结束 还原用
    float _jlYSbl = 0;

    public void TempJianShangBL(float bl,float JSCXTimes)
    {
        _jsBl = bl;
        _JSCXTimes = JSCXTimes;
        _jlYSbl = GetComponent<RoleDate>().shanghaijianmianLv;
        GetComponent<RoleDate>().AddNewSHJMBL(_jsBl);
        _TempJSJSQ = 0;
        IsTempJianShangbili = true;
    }


    void TempJianShang()
    {
        if (IsTempJianShangbili)
        {
            _TempJSJSQ+= Time.deltaTime;
            if(_TempJSJSQ> _JSCXTimes)
            {
                TempJSReSet();
            }
        }
    }

    void TempJSReSet()
    {
        if (IsTempJianShangbili)
        {
            IsTempJianShangbili = false;
            _jsBl = 0;
            _JSCXTimes = 0;
            _TempJSJSQ = 0;
            GetComponent<RoleDate>().shanghaijianmianLv = _jlYSbl;
            _jlYSbl = 0;
        }
    }




    // Update is called once per frame
    void Update()
    {
        AddTempYZ();
        XuanKongTemp();
        TempJianShang();
    }
}
