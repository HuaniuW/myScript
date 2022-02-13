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
        //FuDaiDianXiaoguos();
        FDMaBi();
        FDDianMaBi();

      
        if (IsHuo)
        {
            if (IsInWater)
            {
                //print("   跳到水里了！！！！！！  ");
                HuoOver();
            }
        }
    }


    public void ReSetAll()
    {
        DuOver();
        HuoOver();
        DianOver();
    }

    //****************************************************火伤害***************************

    public bool IsHuo = false;
    float _HuoShanghai = 0;
    float _HuoShanghaiTimes = 0;
    float _HuoJishi = 0;

    //当前火的最大持续时间
    float _CMaxHuoChixuShijian = 0;

    public void InHuo(float ShangHai, float CXTimes)
    {
        //先比较 抗毒的 几率
        if (GlobalTools.GetRandomDistanceNums(100) < _roledate.KangHuoJilv) return;

        _HuoShanghai = ShangHai;
        print(" --火伤害 🔥  " + _HuoShanghai);
        print("进入火伤害  持续时间 " + CXTimes);
        HuoTX();
        if (_HuoShanghaiTimes != 0 && CXTimes <= _CMaxHuoChixuShijian) return;
        _CMaxHuoChixuShijian = CXTimes;
        _HuoShanghaiTimes = CXTimes;
        IsHuo = true;


    }


    [Header("地面图层 包括机关")]
    public LayerMask groundLayer;

    bool IsInWater
    {
        get
        {
            if (_gameBody==null||_gameBody.TopPoint == null) return false;
            Vector2 start = _gameBody.TopPoint.position;
            float __x = start.x;
            Vector2 end = new Vector2(__x, start.y-1);
            Debug.DrawLine(start, end, Color.yellow);
            bool isShentiHitWall = Physics2D.Linecast(start, end, groundLayer);
            return isShentiHitWall;
        }
    }

    //private void OnCollisionEnter2D(Collision2D coll)
    //{
    //    print("  碰到水************************************！！！！ "+coll.gameObject.tag);
    //    if(coll.gameObject.tag == GlobalTag.WATER)
    //    {
    //        print("  碰到水************************************！！！！ ");
    //    }
    //}


    public void HuoOver()
    {
        _HuoShanghai = 0;
        _HuoShanghaiTimes = 0;
        _HuoJishi = 0;
        IsHuo = false;
        _CMaxHuoChixuShijian = 0;
        //if (_ChixuHuoTXObj) _ChixuHuoTXObj.GetComponent<ParticleSystem>().Stop();
        //if (_HuoTXObj) _HuoTXObj.GetComponent<ParticleSystem>().Stop();
        //_gameBody.GetDB().animation.timeScale = 1;

        if (_HuoTXObj)
        {
            _HuoTXObj.GetComponent<ParticleSystem>().Stop();
            ObjectPools.GetInstance().DestoryObject2(_HuoTXObj.gameObject);
        }


        if (ChixuDuodianTXList.Count != 0)
        {
            for (int i = ChixuDuodianTXList.Count - 1; i >= 0; i--)
            {
                if (ChixuDuodianTXList[i].name == HuoTXName)
                {
                    IshasNameTX = true;
                    ChixuDuodianTXList[i].GetComponent<ParticleSystem>().Stop();
                    ObjectPools.GetInstance().DestoryObject2(ChixuDuodianTXList[i].gameObject);
                    //ChixuDuodianTXList.Remove(ChixuDuodianTXList[i]);
                }
            }
            if (IshasNameTX) return;
        }
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


        if (DuodianTXList.Count != 0) {
            DuodianShowChixuXiaoguo(HuoTXName);
        } else
        {
            if (!_HuoTXObj)
            {
                _HuoTXObj = GlobalTools.GetGameObjectInObjPoolByName(HuoTXName);
                _HuoTXObj.transform.position = this.transform.position;
                _HuoTXObj.transform.parent = this.transform;
                //_DUTXObj.GetComponent<ParticleSystem>().shape.
            }
            if (_HuoTXObj)
            {
                _HuoTXObj.gameObject.SetActive(true);
                _HuoTXObj.GetComponent<ParticleSystem>().Play();
            }
        }



        float huoshanghai = _HuoShanghai * (100 - _roledate.KangHuoShanghaijilv)*0.01f;
        if (_roledate.KangHuoJilv < 0) huoshanghai *= Mathf.Abs(_roledate.KangHuoJilv);
        if (huoshanghai < 0) huoshanghai = 0;
        _roledate.live -= huoshanghai;
        //print(" --------------------------- 火焰持续伤害   " + huoshanghai + "   剩余生命   " + _roledate.live);
        if (GlobalTools.GetRandomNum() > 70)
        {
            //if (_audioBeHit && !_audioBeHit.isPlaying) _audioBeHit.Play();
            if (_roleAudio&&_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }


        if (_roledate.live == 0)
        {
            HuoTXDaShow();
            //if (_audioDie && !_audioDie.isPlaying) _audioDie.Play();
            if (_roleAudio&&_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }
        //其他 什么效果 比如 减速  声音-可以放在特效里   被攻击动作 等
    }

    void InHuoShanghai()
    {
        if (!IsHuo) return;
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
    }








    //*********************************************************毒伤害***************************************
    //毒
    //const string DU = "du";
    //火
    //const string HUO = "huo";

    public bool IsDu = false;
    float _DuShanghai = 0;
    float _DuShanghaiTimes = 0;
    float _DuJishi = 0;
    //当前毒的最大持续时间
    float _CMaxDuChixuShijian = 0;
    public void InDu(float ShangHai, float CXTimes)
    {
        //先比较 抗毒的 几率
        if (GlobalTools.GetRandomDistanceNums(100) < _roledate.KangDuJilv) return;

        _DuShanghai = ShangHai;
        _DuShanghaiTimes = CXTimes;
        //是否叠加  叠加 还是覆盖


        DuTX();
        if (_DuShanghaiTimes != 0 && CXTimes <= _CMaxDuChixuShijian)
        {
            return;
        }
        _CMaxDuChixuShijian = CXTimes;
        //ChiXuDuTX();
        IsDu = true;

        //显示一次 中毒的 伤害特效
        //DuTXDaShow();


    }

    void DuOver()
    {
        _DuShanghai = 0;
        _DuShanghaiTimes = 0;
        _DuJishi = 0;
        IsDu = false;
        _CMaxDuChixuShijian = 0;

        if (_DUTXObj)
        {
            _DUTXObj.GetComponent<ParticleSystem>().Stop();
            ObjectPools.GetInstance().DestoryObject2(_DUTXObj.gameObject);
        }


        //print("  持续 毒伤害 结束 ！！！！ "+ ChixuDuodianTXList.Count);
        if (ChixuDuodianTXList.Count != 0)
        {
            for (int i = ChixuDuodianTXList.Count - 1; i >= 0; i--)
            {
                if (ChixuDuodianTXList[i].name == DuTXName)
                {
                    IshasNameTX = true;
                    ChixuDuodianTXList[i].GetComponent<ParticleSystem>().Stop();
                    ObjectPools.GetInstance().DestoryObject2(ChixuDuodianTXList[i].gameObject);
                    //ChixuDuodianTXList.Remove(ChixuDuodianTXList[i]);
                }
            }
            if (IshasNameTX) return;
        }
    }




    public List<Transform> DuodianTXList;
    protected List<GameObject> ChixuDuodianTXList = new List<GameObject>() { };
    bool IshasNameTX = false;

    [Header("持续伤害特效 放大 倍数 默认0 不做处理")]
    public float TXBeishu = 0;
    //大型怪 持续伤害效果显示 多点显示
    protected void DuodianShowChixuXiaoguo(string TXName)
    {
        for (int i = 0; i < DuodianTXList.Count; i++)
        {
            //交叉循环了 有点 耗性能
            GameObject tx = GetTXInList(TXName);
            if (tx == null) tx = GlobalTools.GetGameObjectInObjPoolByName(TXName);

            //GameObject tx = GlobalTools.GetGameObjectInObjPoolByName(TXName);

            tx.gameObject.SetActive(true);
            tx.transform.position = DuodianTXList[i].position;
            tx.transform.parent = this.transform;
            if (TXBeishu!=0)
            {
                tx.transform.localScale = new Vector3(TXBeishu, TXBeishu, TXBeishu);
            }
            //print("  shengchengde shihou  "+ tx.name);
            tx.name = TXName;
            //print("  shengchengde shihou >>>>>>>>>>>> " + tx.name);
            tx.GetComponent<ParticleSystem>().Play();
            ChixuDuodianTXList.Add(tx);
        }

    }



    GameObject GetTXInList(string TXName)
    {
        if (ChixuDuodianTXList.Count != 0)
        {
            foreach (GameObject o in ChixuDuodianTXList)
            {
                if (o.name == TXName && !o.activeSelf)
                {
                    return o;
                }
            }
        }
        return null;
    }




    string DuTXName = "TX_DuXiaoGuo";
    GameObject _DUTXObj;
    //每次 扣血 显示的 毒特效 及 其他效果
    void DuTX()
    {
        if (_roledate.isDie) return;

        if (DuodianTXList.Count != 0)
        {
            DuodianShowChixuXiaoguo(DuTXName);
        }
        else
        {
            if (!_DUTXObj)
            {
                _DUTXObj = GlobalTools.GetGameObjectInObjPoolByName(DuTXName);
                _DUTXObj.transform.position = this.transform.position;
                _DUTXObj.transform.parent = this.transform;
                //_DUTXObj.GetComponent<ParticleSystem>().shape.
            }
            if (_DUTXObj)
            {
                _DUTXObj.gameObject.SetActive(true);
                _DUTXObj.GetComponent<ParticleSystem>().Play();
            }
        }


        //float dianshanghai = _DianShanghai * (1 - _roledate.KangDuShanghaijilv);
        //if (dianshanghai < 0) dianshanghai = 0;
        float TheDushanghai = _DuShanghai*(100-_roledate.KangDuShanghaijilv)*0.01f;
        if (_roledate.KangDuJilv<0) TheDushanghai *= Mathf.Abs(_roledate.KangDuJilv);
        _roledate.live -= TheDushanghai;





        //print("  毒 持续伤害   " + dushanghai + "   剩余生命   " + _roledate.live);

        if (GlobalTools.GetRandomNum() > 70)
        {
            //if (_audioBeHit && !_audioBeHit.isPlaying) _audioBeHit.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();


        }


        if (_roledate.live == 0)
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
    }


    //****************************************************电伤害**************************************************************
    public bool IsDian = false;
    float _DianShanghai = 0;
    float _DianMabiChixuTimes = 0;
    float _DianMabiJishi = 0;
    //当前电的 最大麻痹 持续时间
    float _CMaxDianMabiShijian = 0;


    public void InDian(float ShangHai, float CXTimes)
    {
        //先比较 抗电麻痹的 几率
        if (GlobalTools.GetRandomDistanceNums(100) < _roledate.KangDianJilv) return;

        _DianShanghai = ShangHai;
        _DianMabiChixuTimes = CXTimes;
        //是否叠加  叠加 还是覆盖

        
        DianTX();
        if (_DianMabiChixuTimes != 0 && CXTimes <= _CMaxDianMabiShijian)
        {
            return;
        }

        

        //_gameBody.HasBeHit();
        _CMaxDianMabiShijian = CXTimes;
        if (_roledate.KangDianMabiJilv < 0) {
            _CMaxDianMabiShijian = CXTimes * Mathf.Abs(_roledate.KangDianMabiJilv);
            GetComponent<GameBody>().HasBeHit();
            print("弱电  持续时间   "+ _CMaxDianMabiShijian);
        }


        IsDian = true;

        //if (_roledate.KangDianMabiJilv<0||GlobalTools.GetRandomNum() <= 30) {
        //    IsDian = true;
        //}
        
    }


    void FDDianMaBi()
    {
        if (!IsDian) return;
        _DianMabiJishi += Time.deltaTime;
        //print("附带 麻痹 效果 _DianMabiJishi " + _DianMabiJishi+ " _CMaxDianMabiShijian    "+ _CMaxDianMabiShijian);

        _gameBody.GetDB().animation.timeScale = 0.1f;
        _gameBody.GetDB().animation.Stop();
        _gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;
        //GetPlayerRigidbody2D().gravityScale = 0;

        if (_DianMabiJishi >= _CMaxDianMabiShijian || _roledate.isDie)
        {
            IsDian = false;
            print(" 电效果结束！！！！！  ");
            _gameBody.GetDB().animation.Play();
            _gameBody.GetDB().animation.timeScale = 1;
            _DianMabiJishi = 0;
            _CMaxDianMabiShijian = 0;
            //GetPlayerRigidbody2D().gravityScale = 4.5f;
        }
    }

    protected void DianOver()
    {
        _DianShanghai = 0;
        _DianMabiChixuTimes = 0;
        _DianMabiJishi = 0;
        IsDian = false;
        _CMaxDianMabiShijian = 0;
        //if (_ChixuHuoTXObj) _ChixuHuoTXObj.GetComponent<ParticleSystem>().Stop();
        //if (_HuoTXObj) _HuoTXObj.GetComponent<ParticleSystem>().Stop();
        //_gameBody.GetDB().animation.timeScale = 1;

        if (_DianTXObj)
        {
            _DianTXObj.GetComponent<ParticleSystem>().Stop();
            ObjectPools.GetInstance().DestoryObject2(_DianTXObj.gameObject);
        }


        if (ChixuDuodianTXList.Count != 0)
        {
            for (int i = ChixuDuodianTXList.Count - 1; i >= 0; i--)
            {
                if (ChixuDuodianTXList[i].name == DianTXName)
                {
                    IshasNameTX = true;
                    ChixuDuodianTXList[i].GetComponent<ParticleSystem>().Stop();
                    ObjectPools.GetInstance().DestoryObject2(ChixuDuodianTXList[i].gameObject);
                    //ChixuDuodianTXList.Remove(ChixuDuodianTXList[i]);
                }
            }
            if (IshasNameTX) return;
        }
    }




    string DianTXName = "JZTX_dian";
    GameObject _DianTXObj;

    void DianTX() {
        if (_roledate.isDie) return;

        //_gameBody.GetPlayerRigidbody2D().velocity = Vector2.zero;

        if (DuodianTXList.Count != 0)
        {
            DuodianShowChixuXiaoguo(DianTXName);
        }
        else
        {
            if (!_DianTXObj)
            {
                _DianTXObj = GlobalTools.GetGameObjectInObjPoolByName(DianTXName);
                _DianTXObj.transform.position = this.transform.position;
                _DianTXObj.transform.parent = this.transform;
                //_DUTXObj.GetComponent<ParticleSystem>().shape.
            }
            if (_DianTXObj)
            {
                _DianTXObj.gameObject.SetActive(true);
                _DianTXObj.GetComponent<ParticleSystem>().Play();
            }
        }


        float dianshanghai = _DianShanghai;
        if (dianshanghai < 0) dianshanghai = 0;
        print(" 弱  电伤害   "+ dianshanghai+"   抗电几率  "+ _roledate.KangDianMabiJilv);
        if (_roledate.KangDianMabiJilv < 0)
        {
            dianshanghai *= Mathf.Abs(_roledate.KangDianMabiJilv);
            print("弱电伤害！！！！！！！！！"+ dianshanghai);
        }
        _roledate.live -= dianshanghai;

        //print("  毒 持续伤害   " + dushanghai + "   剩余生命   " + _roledate.live);

        if (GlobalTools.GetRandomNum() > 70)
        {
            //if (_audioBeHit && !_audioBeHit.isPlaying) _audioBeHit.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }


        if (_roledate.live == 0)
        {
            //DuTXDaShow();
            //if (_audioDie && !_audioDie.isPlaying) _audioDie.Play();
            if (_roleAudio.BeHit_1 && !_roleAudio.BeHit_1.isPlaying) _roleAudio.BeHit_1.Play();
        }
    }




    //附带效果  比如*电


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
            if (GlobalTools.GetRandomNum() <= _roledate.KangDianMabiJilv) return;



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
