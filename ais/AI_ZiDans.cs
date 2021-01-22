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


    RoleDate _roleDate;

    // Start is called before the first frame update
    void Start()
    {
        _roleDate = GetComponent<RoleDate>();
    }

    // Update is called once per frame
    void Update()
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
            if (this.transform.position.x > _player.transform.position.x)
            {
                GetComponent<GameBody>().TurnLeft();
            }
            else
            {
                GetComponent<GameBody>().TurnRight();
            }
        }

        _qishiACJS += Time.deltaTime;

        if (TX_Qishi && TX_Qishi.isStopped)
        {
            TX_Qishi.Play();
        }


        if (_qishiACJS>= QiShiACYCTimes)
        {
            ReSetAll();
            _qishiACJS = 0;
            Fire();
        }
    }

    protected bool _isFire = false;
    protected virtual void Fire()
    {
        print(">>fashezidan!!  发射 什么 子弹 都这里控制");
        if (FireAudio)
        {
            print("  -------------------------------------------------- 子弹发射声音  ");
            FireAudio.Play();
        }






        GameObject zidan = GetZiDan();
        Vector2 v1 = _player.transform.position - ZiDanPos.position;
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

        //连续3层  等。。。。
        

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


    


    GameObject GetZiDan()
    {
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZiDanName) as GameObject);
        zidan.transform.position = ZiDanPos.position;
        zidan.GetComponent<TX_zidan>().CloseAutoFire();
        zidan.transform.localScale = this.transform.localScale;
        zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        return zidan;
    } 



    void CreateZiDan(int nums)
    {
        
    }

    






    float _fireOverJS = 0;
    public float _fireOverTimes = 0.2f;
    protected bool _isFireOver = false;
    protected virtual void FireOver()
    {
        _fireOverJS += Time.deltaTime;
        if (_fireOverJS>= _fireOverTimes)
        {
            ReSetAll();
            print(" 子弹s " + ZiDanTypeNum+"   结束！！！  ");
            _isGetOver = true;
        }
    }



    public void ReSetAll()
    {
        _isQiShiAC = false;
        _isGetOver = false;
        _isFireOver = false;
        _fireOverJS = 0;
        _qishiACJS = 0;
        IsTrunFace = false;
    }


    GameObject _player;
    public void GetStart(GameObject gameObj)
    {
        print(" 子弹s "+ZiDanTypeNum);
        _player = gameObj;
        _isGetOver = false;
        _isQiShiAC = true;
        
    }

    bool _isGetOver = false;
    public bool IsGetOver()
    {
        return _isGetOver;
    }
}
