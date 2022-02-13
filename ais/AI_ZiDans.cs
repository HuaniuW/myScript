using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZiDans : MonoBehaviour,ISkill
{
    [Header("子弹发射点")]
    public Transform ZiDanPos;

    [Header("准备 发射子弹前 的 特效 ")]
    public ParticleSystem TX_Qishi;

    [Header("起手的 声音")]
    public AudioSource QSAudio;
    [Header("子弹发射 声音")]
    public AudioSource FireAudio;

    [Header("是否需要面对 目标")]
    public bool IsFaceToTarget = false;
    bool IsTrunFace = false;


    protected RoleDate _roleDate;
    protected GameBody _gameBody;

    // Start is called before the first frame update
    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
        _gameBody = GetComponent<GameBody>();
        StartMove();
    }

    protected virtual void StartMove()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            _isGetOver = true;
            return;
        }

        if (_isQiShiAC) QiShiAC();
        if (_isFireOver) FireOver();
    }


    public int ZiDanTypeNum = 1;
    public void SetZiDanType(int typeNums = 1)
    {
        ZiDanTypeNum = typeNums;
    }




    float _qishiACJS = 0;
    public float QiShiACYCTimes = 0.5f;
    protected bool _isQiShiAC = false;
    //起始动作
    protected virtual void QiShiAC()
    {
        if (IsFaceToTarget&&!IsTrunFace)
        {
            IsTrunFace = true;

            _gameBody.SetV0();

            if (this.transform.position.x > _player.transform.position.x)
            {
                _gameBody.TurnLeft();
            }
            else
            {
                _gameBody.TurnRight();
            }
            if (QSAudio) QSAudio.Play();
        }

        _qishiACJS += Time.deltaTime;

        //print("_qishiACJS    "+ _qishiACJS+ "    TX_Qishi isplaying :   " + TX_Qishi.isPlaying);

        if (TX_Qishi&&!TX_Qishi.isPlaying)
        {
            //print(ZiDanName+"    ---------------------------------------------->播放 起始粒子！！！！" + _qishiACJS);
            //_qishiACJS = 0;
            //TX_Qishi.Simulate(0.0f);
            if (QSAudio) QSAudio.Play();
            TX_Qishi.Play();
        }


        if (_qishiACJS>= QiShiACYCTimes)
        {
            _qishiACJS = 0;
            if (QSAudio) QSAudio.Stop();
            ReSetAll();
            Fire();
        }
    }


   
    protected virtual Vector2 ZidanFX()
    {
        return _player.transform.position - ZiDanPos.position;
    }



    protected bool _isFire = false;
    protected virtual void Fire()
    {
        //print(">>fashezidan!!  发射 什么 子弹 都这里控制");
        if (FireAudio)
        {
            //print("  -------------------------------------------------- 子弹发射声音  ");
            FireAudio.Play();
        }

        ZiDanName = "TX_zidan1";
        Vector3 _targetPos = _player.transform.position;


        if (ZiDanTypeNum == 10)
        {

            //毒子弹 1发
            ZiDanName = "TX_zidandu1";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y+2.7f, _targetPos.z);
        }
        else if (ZiDanTypeNum == 11)
        {
            //毒子弹 3发
            ZiDanName = "TX_zidandu1";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y+2.7f, _targetPos.z);
        }
        else if (ZiDanTypeNum == 12)
        {
            //中途会爆炸的 毒雾弹
            ZiDanName = "TX_zidandu2";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y, _targetPos.z);
        }
        else if (ZiDanTypeNum == 14)
        {
            //一般 火子弹
            ZiDanName = "TX_zidan7";
        }
        else if (ZiDanTypeNum == 15)
        {
            //火爆弹
            ZiDanName = "TX_huoyanDan";
        }
        else if (ZiDanTypeNum == 16)
        {
            //火爆弹
            ZiDanName = "TX_huoyanDan";
        }


        GameObject zidan = GetZiDan();
        if (zidan == null) {
            _isFireOver = true;
            return;
        } 
        Vector2 v1 = ZidanFX();
        zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v1, 10);



        if (ZiDanTypeNum == 2)
        {
            //3子弹
            SanLianSanDan(v1, 1);
        }
        else if (ZiDanTypeNum == 3)
        {
            //5子弹
            SanLianSanDan(v1, 2);
        }
        else if (ZiDanTypeNum == 4)
        {
            //11子弹
            SanLianSanDan(v1, 5);
        }else if (ZiDanTypeNum == 5)
        {
            //连续 2层的 3子弹
        }
        else if (ZiDanTypeNum == 10)
        {

            //毒子弹 1发
        }
        else if (ZiDanTypeNum == 11)
        {
            //毒子弹 3发
            SanLianSanDan(v1, 1);
        }
        else if (ZiDanTypeNum == 12)
        {
            //中途会爆炸的 毒雾弹

        }else if (ZiDanTypeNum == 14)
        {
            //龙的 3发子弹
            SanLianSanDan(v1, 1);

        }
        else if (ZiDanTypeNum == 15)
        {
            //火爆弹
            
            //SanLianSanDan(v1, 1);
        }
        else if (ZiDanTypeNum == 16)
        {
            //3发 火爆弹

            SanLianSanDan(v1, 1);
        }


        _isFireOver = true;
    }

    public string ZiDanName = "TX_zidan1";
    void SanLianSanDan(Vector2 v1,int nums = 1,float hudu = 20)
    {
        GameObject zidan;
        float _hudu = hudu;
        for (int i = 0; i < nums; i++)
        {
            zidan = GetZiDan();
            _hudu = hudu * (i + 1) * 3.14f / 180;
            Vector2 v2 = GlobalTools.GetNewV2ByHuDu(_hudu, v1);
            zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v2, 10);
        }

        for (int i = 0; i < nums; i++)
        {
            zidan = GetZiDan();
            _hudu = -hudu * (i + 1) * 3.14f / 180;
            Vector2 v2 = GlobalTools.GetNewV2ByHuDu(_hudu, v1);
            zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v2, 10);
        }

    }


    


    protected GameObject GetZiDan()
    {
        //print("************************************************ZiDanName   " + ZiDanName);
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZiDanName) as GameObject);
        //print("  zidan "+zidan);
        if (zidan == null || ZiDanPos == null) return null;
        if (ZiDanTypeNum == 15|| ZiDanTypeNum == 16)
        {
            //火焰弹 
            zidan.GetComponent<OnLziHit>().SetCanHit();
        }

        

        zidan.transform.position = ZiDanPos.position;
        zidan.GetComponent<TX_zidan>().CloseAutoFire();
        zidan.transform.localScale = this.transform.localScale;
        zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        return zidan;
    } 



    float _fireOverJS = 0;
    public float _fireOverTimes = 0.2f;
    protected bool _isFireOver = false;
    protected virtual void FireOver()
    {
        //print("  进来没  fireOver!!! ");
        _fireOverJS += Time.deltaTime;
        if (_fireOverJS>= _fireOverTimes)
        {
            ReSetAll();
            //print(" 子弹s " + ZiDanTypeNum+"   结束！！！  ");
            _isGetOver = true;
        }
    }



    public void ReSetAll()
    {
        _isQiShiAC = false;
        //_isGetOver = false;
        _isFireOver = false;
        _fireOverJS = 0;
        _qishiACJS = 0;
        IsTrunFace = false;
        //TX_Qishi.Pause();
        if(TX_Qishi) TX_Qishi.Stop();
        if (TX_Qishi) TX_Qishi.gameObject.SetActive(false);
        ResetAllMore();
    }


    protected virtual void ResetAllMore()
    {

    }

    protected GameObject _player;
    public virtual void GetStart(GameObject gameObj)
    {
        //print(" 子弹s "+ZiDanTypeNum);
        _player = gameObj;
        _isGetOver = false;
        _isQiShiAC = true;
        //print("Getstart is TXplayering :  "+TX_Qishi.isPlaying);
        //TX_Qishi.Pause();
        TX_Qishi.gameObject.SetActive(true);
        TX_Qishi.Stop();
         
        //print("2222Getstart is TXplayering :  " + TX_Qishi.isPlaying);
    }

    protected bool _isGetOver = false;
    public bool IsGetOver()
    {
        //print("  zidans Over????   "+_isGetOver);
        return _isGetOver;
    }
}
