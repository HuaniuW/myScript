using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDZidans : AI_SkillBase
{
    // eg:   ZD_Zidans_10

    protected override void ACSkillShowOut()
    {
        print(" dongzuo name shijian **********************ac jicheng!!!   " + ACScaleStartTimes);

        if(ZiDanTypeNum == 18)
        {
            print("  进入 连弹！！！！！ ");
            StartLiandan();
            return;
        }

        //这里要实现 JNDate 赋值
        Fire();
    }

    int ZiDanTypeNum = 0;

    protected override void ZDSkillShow(UEvent e)
    {
        if(!_player)_player = GlobalTools.FindObjByName("player");
        ZSName = "Zidans";
        GetZSName = "";

        //"id@ZD_Zidans_10"

        string[] strArr = e.eventParams.ToString().Split('@');
        string id = strArr[0];
        string msg = strArr[1];
        string[] msgArr = msg.Split('_');
        GetZSName = msg.Split('_')[1];
        //print("自动技能 释放 接收 参数 " + e.eventParams.ToString() + "   本技能名字  " + ZSName + " GetZSName   " + GetZSName);

        if(msgArr.Length>2) ZiDanTypeNum = int.Parse(msgArr[2]);

        //print("----->ZiDanTypeNum    "+ ZiDanTypeNum+ "  strArr   "+ msgArr.Length);

        if (id == this.gameObject.GetInstanceID().ToString() && GetZSName == ZSName)
        {
            //print("发动技能  " + ZSName);
            GetStart(null);
        }
    }



    void LiandanReset()
    {
        LiandanJishi = 0;
        LiandanJishu = 0;
        OverDelayTimes = _OverDelayTimes;
        Liandan = false;
    }


    void StartLiandan()
    {
        _OverDelayTimes = OverDelayTimes;
        OverDelayTimes = 6;
        Liandan = true;
    }


    float _OverDelayTimes = 0;
    float LiandanJishi = 0;
    float LiandanJiange = 0.2f;
    int LiandanNums = 10;
    int LiandanJishu = 0; 
    bool Liandan = false;
    void GetStartLianDan()
    {
        if (!Liandan) return;

        if (LiandanJishu >= 10)
        {
            TheSkillOver();
            ReSetAll();
            LiandanReset();
            return;
        }
        
        LiandanJishi += Time.deltaTime;
        if (LiandanJishi >= LiandanJiange)
        {
            print("   liandan sheji!!!!!!!!!!!! " );
            LiandanJishi = 0;
            LiandanJishu++;
            Fire();
        }
    }

    protected override void ChixuSkillStarting()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            ReSetAll();
            LiandanReset();
            return;
        }
        GetStartLianDan();
    }




    public AudioSource FireAudio;

    //public GameObject _player;


    protected bool _isFire = false;
    protected virtual void Fire()
    {
        //print(">>fashezidan!!  发射 什么 子弹 都这里控制");
        if (FireAudio)
        {
            print("  -------------------------------------------------- 子弹发射声音  ");
            FireAudio.Play();
        }

        ZiDanName = "TX_zidan1";
        Vector3 _targetPos = _player.transform.position;


        if (ZiDanTypeNum == 23)
        {
            //高速 火焰单
            ZiDanName = "TX_zidan23";
        }
        else if (ZiDanTypeNum == 20)
        {
            //高速子弹
            ZiDanName = "TX_zidan2";
            //_targetPos = new Vector3(_targetPos.x, _targetPos.y + 2.7f, _targetPos.z);
        }
        else if (ZiDanTypeNum == 2)
        {
            ZiDanName = "TX_zidan1";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y + 2.7f, _targetPos.z);
        }
        else if (ZiDanTypeNum == 10)
        {

            //毒子弹 1发
            ZiDanName = "TX_zidandu1";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y + 2.7f, _targetPos.z);
        }
        else if (ZiDanTypeNum == 11)
        {
            //毒子弹 3发
            ZiDanName = "TX_zidandu1";
            _targetPos = new Vector3(_targetPos.x, _targetPos.y + 2.7f, _targetPos.z);
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
        else if (ZiDanTypeNum == 21)
        {
            //大子弹 单子弹
            ZiDanName = "TX_zidan21";
        }
        else if (ZiDanTypeNum == 22)
        {
            //大子弹 
            ZiDanName = "TX_zidan21";
        }
        else if (ZiDanTypeNum == 28)
        {
            //子弹22 会飘一会 再进攻
            ZiDanName = "TX_zidan22";
        }
        else if (ZiDanTypeNum == 24)
        {
            //3散 子弹
            ZiDanName = "TX_zidan21";
        }
        else if (ZiDanTypeNum == 25)
        {
            //顺序5散 子弹
            ZiDanName = "TX_zidan21";
        }


        GameObject zidan = GetZiDan();
        Vector2 v1 = ZidanFX();
        zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v1, 10);

        if (ZiDanTypeNum == 20|| ZiDanTypeNum == 23)
        {
            //高速子弹
            zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v1, 40);
        }
        else if (ZiDanTypeNum == 2)
        {
            //3子弹
            SanLianSanDan(v1, 1,40);
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
        }
        else if (ZiDanTypeNum == 5)
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

        }
        else if (ZiDanTypeNum == 14)
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
        else if (ZiDanTypeNum == 24)
        {
            //子弹22 会飘一会 再进攻
            SanLianSanDan(v1, 1);
        }
        else if (ZiDanTypeNum == 25)
        {
            //子弹22 会飘一会 再进攻
            //SanLianSanDan(v1, 1);
        }
    }


    protected virtual Vector2 ZidanFX()
    {
        return _player.transform.position - zidanDian1.position;
    }

    public string ZiDanName = "TX_zidan1";
    void SanLianSanDan(Vector2 v1, int nums = 1, float hudu = 20)
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

        if (ZiDanTypeNum == 15 || ZiDanTypeNum == 16)
        {
            //火焰弹 
            zidan.GetComponent<OnLziHit>().SetCanHit();
        }
        zidan.transform.position = zidanDian1.position;
        zidan.GetComponent<TX_zidan>().CloseAutoFire();
        zidan.transform.localScale = this.transform.localScale;
        zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        return zidan;
    }




}
