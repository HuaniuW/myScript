using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiXuShangHai : MonoBehaviour
{
    // Start is called before the first frame update
    RoleDate _roledate;
    GameBody _gameBody;
  

    RoleAudio _roleAudio;

    void Start()
    {
        _roledate = GetComponent<RoleDate>();
        _gameBody = GetComponent<GameBody>();
        _roleAudio = GetComponent<RoleAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        InDuShanghai();
        InHuoShanghai();
        FuDaiDianXiaoguos();
        FDMaBi();
    }


    public void ReSetAll()
    {
        DuOver();
    }

    //****************************************************火伤害***************************

    bool IsHuo = false;
    float _HuoShanghai = 0;
    float _HuoShanghaiTimes = 0;
    float _HuoJishi = 0;
    public void InHuo(float ShangHai, float CXTimes)
    {
        //先比较 抗毒的 几率
        if (GlobalTools.GetRandomDistanceNums(100) < _roledate.KangDuJilv) return;

        _HuoShanghai = ShangHai;
        print(" --火伤害 🔥  "+ _HuoShanghai);
        _HuoShanghaiTimes = CXTimes;
        //是否叠加  叠加 还是覆盖
        ChiXuHuoTX();
        IsHuo = true;

        //显示一次 中毒的 伤害特效
        HuoTXDaShow();
    }

    void HuoOver()
    {
        _HuoShanghai = 0;
        _HuoShanghaiTimes = 0;
        _HuoJishi = 0;
        IsHuo = false;
        if (_ChixuHuoTXObj) _ChixuHuoTXObj.GetComponent<ParticleSystem>().Stop();
        if (_HuoTXObj) _HuoTXObj.GetComponent<ParticleSystem>().Stop();
        //_gameBody.GetDB().animation.timeScale = 1;
    }

    //**** 毒效果 根据 怪物大小 怎么处理？？
    //****  毒抗性 属性
    string ChixuHuoTXName = "TX_CXHuoXiaoGuo";
    GameObject _ChixuHuoTXObj;
    //ParticleSystem.ShapeModule A;
    //显示 持续 毒特效
    void ChiXuHuoTX()
    {
        if (!_ChixuHuoTXObj)
        {
            _ChixuHuoTXObj = GlobalTools.GetGameObjectByName(ChixuHuoTXName);
            _ChixuHuoTXObj.transform.position = this.transform.position;
            _ChixuHuoTXObj.transform.parent = this.transform;
            //_ChixuDUTXObj.transform.position = Vector2.zero;
            //A = _ChixuDUTXObj.GetComponent<ParticleSystem>().shape;
            //A.angle = 10;
            //_ChixuDUTXObj.transform.localScale = new Vector3(3, 3, 3);
        }
        _ChixuHuoTXObj.GetComponent<ParticleSystem>().Play();
    }


   
    void HuoTXDaShow()
    {
        GameObject HuoTX = GlobalTools.GetGameObjectByName(HuoTXName);
        HuoTX.transform.position = this.transform.position;
        HuoTX.transform.parent = this.transform;

        HuoTX.transform.localScale = new Vector3(2, 2, 2);
        HuoTX.GetComponent<ParticleSystem>().Play();
    }

    string HuoTXName = "TX_HuoXiaoGuo";
    GameObject _HuoTXObj;
    //每次 扣血 显示的 火特效 及 其他效果
    void HuoTX()
    {
        if (_roledate.isDie) return;
        if (!_HuoTXObj)
        {
            _HuoTXObj = GlobalTools.GetGameObjectByName(HuoTXName);
            _HuoTXObj.transform.position = this.transform.position;
            _HuoTXObj.transform.parent = this.transform;
            //_DUTXObj.GetComponent<ParticleSystem>().shape.
        }
        if (_HuoTXObj)
        {
            _HuoTXObj.GetComponent<ParticleSystem>().Play();
        }
        float huoshanghai = _HuoShanghai * (1 - _roledate.KangDuShanghaijilv);
        if (huoshanghai < 0) huoshanghai = 0;
        _roledate.live -= huoshanghai;
        print("  火焰持续伤害   "+ huoshanghai+"   剩余生命   "+ _roledate.live);
        if (GlobalTools.GetRandomNum() > 70)
        {
            //if (_audioBeHit && !_audioBeHit.isPlaying) _audioBeHit.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }


        if (_roledate.live == 0)
        {
            HuoTXDaShow();
            //if (_audioDie && !_audioDie.isPlaying) _audioDie.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }
        //其他 什么效果 比如 减速  声音-可以放在特效里   被攻击动作 等
    }

    void InHuoShanghai()
    {
        if (!IsHuo) return;
        //_gameBody.GetPlayerRigidbody2D().velocity *= 0.9f;
        //_gameBody.GetDB().animation.timeScale = 0.9f;
        _HuoJishi += Time.deltaTime;
        if (_HuoJishi >= 1)
        {
            _HuoJishi = 0;
            _HuoShanghaiTimes--;
            HuoTX();
            if (_HuoShanghaiTimes <= 0)
            {
                HuoOver();
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








    //*********************************************************毒伤害***************************************
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
        //先比较 抗毒的 几率
        if (GlobalTools.GetRandomDistanceNums(100) < _roledate.KangDuJilv) return;

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
        if (_roledate.isDie) return;


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
        if (GlobalTools.GetRandomNum() > 70)
        {
            //if (_audioBeHit && !_audioBeHit.isPlaying) _audioBeHit.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();


        }


        if (_roledate.live ==0)
        {
            DuTXDaShow();
            //if (_audioDie && !_audioDie.isPlaying) _audioDie.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
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








    public void FudaiXiaoguo(string fdStr)
    {
        string fdName = "";
        float cxTime = 0;
        float meimiaoSH = 0;
        int n = fdStr.Split('_').Length;
        fdName = fdStr.Split('_')[0];


        if (GetComponent<RoleDate>().isDie) return;

        if (n == 2)
        {
            cxTime = float.Parse(fdStr.Split('_')[1]);
        }

        if (n == 3)
        {
            //持续时间
            cxTime = float.Parse(fdStr.Split('_')[1]);
            //每秒伤害
            meimiaoSH = float.Parse(fdStr.Split('_')[2]);

        }

        if (fdName == "mabi")
        {
            //print("  mabi!!!!!!!!!!!!!  ");

            //抗麻痹属性
            if (GlobalTools.GetRandomNum() <= _roledate.KangMabiJilv) return;



            IsMabi = true;

            HitTX2("JZTX_dian");


            if (_mbMaxTimes != 0)
            {
                if (_mbMaxTimes - _mbTimes >= cxTime) return;
            }
            _mbMaxTimes = cxTime;

            _mbTimes = 0;

        }
    }

    bool IsMabi = false;

    float _mbTimes = 0;
    float _mbMaxTimes = 0;

    void FDMaBi()
    {
        if (!IsMabi) return;
        _mbTimes += Time.deltaTime;
        //print("附带 麻痹 效果  "+_mbMaxTimes);

        _gameBody.GetDB().animation.timeScale = 0.1f;
        _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        //GetPlayerRigidbody2D().gravityScale = 0;

        if (_mbTimes >= _mbMaxTimes || _roledate.isDie)
        {
            IsMabi = false;
            _gameBody.GetDB().animation.timeScale = 1;
            _mbTimes = 0;
            _mbMaxTimes = 0;
            //GetPlayerRigidbody2D().gravityScale = 4.5f;
        }
    }

    void HitTX2(string txName)
    {
        GameObject hitTx = Resources.Load(txName) as GameObject;
        hitTx = ObjectPools.GetInstance().SwpanObject2(hitTx);
        hitTx.transform.position = _gameBody.transform.position;
        hitTx.transform.parent = _gameBody.transform;
    }


    void FuDaiDianXiaoguos()
    {
        FDMaBi();
    }





}
