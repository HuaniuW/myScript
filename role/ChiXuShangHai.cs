using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiXuShangHai : MonoBehaviour
{
    // Start is called before the first frame update
    RoleDate _roledate;
    GameBody _gameBody;


    void Start()
    {
        _roledate = GetComponent<RoleDate>();
        _gameBody = GetComponent<GameBody>();
    }

    // Update is called once per frame
    void Update()
    {
        InDuShanghai();
    }


    public void ReSetAll()
    {
        DuOver();
    }

    //毒
    const string DU = "du";
    //火
    const string HUO = "huo";

    bool IsDu = false;
    float _DuShanghai = 0;
    float _DuShanghaiTimes = 0;
    float _DuJishi = 0;
    public void InDu(float ShangHai, float CXTimes)
    {
        _DuShanghai = ShangHai;
        _DuShanghaiTimes = CXTimes;
        //是否叠加  叠加 还是覆盖
        ChiXuDuTX();
        IsDu = true;

        //显示一次 中毒的 伤害特效
        DuTXDaShow();
    }

    void DuOver()
    {
        _DuShanghai = 0;
        _DuShanghaiTimes = 0;
        _DuJishi = 0;
        IsDu = false;
        if(_ChixuDUTXObj) _ChixuDUTXObj.GetComponent<ParticleSystem>().Stop();
        if(_DUTXObj) _DUTXObj.GetComponent<ParticleSystem>().Stop();
        //_gameBody.GetDB().animation.timeScale = 1;
    }

    //**** 毒效果 根据 怪物大小 怎么处理？？
    //****  毒抗性 属性
    string ChixuDuTXName = "TX_CXDuXiaoGuo";
    GameObject _ChixuDUTXObj;
    ParticleSystem.ShapeModule A;
    //显示 持续 毒特效
    void ChiXuDuTX()
    {
        if (!_ChixuDUTXObj)
        {
            _ChixuDUTXObj = GlobalTools.GetGameObjectByName(ChixuDuTXName);
            _ChixuDUTXObj.transform.position = this.transform.position;
            _ChixuDUTXObj.transform.parent = this.transform;
            //_ChixuDUTXObj.transform.position = Vector2.zero;
            //A = _ChixuDUTXObj.GetComponent<ParticleSystem>().shape;
            //A.angle = 10;
            //_ChixuDUTXObj.transform.localScale = new Vector3(3, 3, 3);
        }
        _ChixuDUTXObj.GetComponent<ParticleSystem>().Play();
    }


    string DuTXName = "TX_DuXiaoGuo";
    GameObject _DUTXObj;
    //每次 扣血 显示的 毒特效 及 其他效果
    void DuTX()
    {
        if (!_DUTXObj)
        {
            _DUTXObj = GlobalTools.GetGameObjectByName(DuTXName);
            _DUTXObj.transform.position = this.transform.position;
            _DUTXObj.transform.parent = this.transform;
            //_DUTXObj.GetComponent<ParticleSystem>().shape.
        }
        if (_DUTXObj) {
            _DUTXObj.GetComponent<ParticleSystem>().Play();
        }
        float dushanghai = _DuShanghai * (1 - _roledate.KangDuShanghaijilv);
        if (dushanghai < 0) dushanghai = 0;
        _roledate.live -= dushanghai;

        if (_roledate.live ==0)
        {
            DuTXDaShow();
        }
        //其他 什么效果 比如 减速  声音-可以放在特效里   被攻击动作 等
    }


    void DuTXDaShow()
    {
        GameObject duTX = GlobalTools.GetGameObjectByName(DuTXName);
        duTX.transform.position = this.transform.position;
        duTX.transform.parent = this.transform;

        duTX.transform.localScale = new Vector3(2, 2, 2);
        duTX.GetComponent<ParticleSystem>().Play();
    }

    

    void InDuShanghai()
    {
        if (!IsDu) return;
        //_gameBody.GetPlayerRigidbody2D().velocity *= 0.9f;
        //_gameBody.GetDB().animation.timeScale = 0.9f;
        _DuJishi += Time.deltaTime;
        if (_DuJishi >= 1)
        {
            _DuJishi = 0;
            _DuShanghaiTimes--;
            DuTX();
            if (_DuShanghaiTimes <= 0)
            {
                DuOver();
            }
        }

        //if (_ChixuDUTXObj)
        //{
        //    if (_ChixuDUTXObj.transform.position != Vector3.zero)
        //    {
        //        _ChixuDUTXObj.transform.parent = this.transform;
        //        _ChixuDUTXObj.transform.position = Vector3.zero;
        //    }
        //}

    }



}
