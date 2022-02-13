using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ZDMove : AI_SkillBase
{
   //技能名字  ZDMoveAwayX  X远离

    protected override void TheStart()
    {
        //开启 特殊 技能释放  这里 就进入持续技能里面  ChixuSkillStarting
        IsSpeAISkill = true;
        IsResetInStand = false;
        this.ZSName = "ZDMoveAwayX";
        if (_player == null) _player = GlobalTools.FindObjByName("player");
    }

    protected override void ACSkillShowOut()
    {

    }

    protected override void ChixuSkillStarting()
    {
        if (_roleDate.isDie || _roleDate.isBeHiting)
        {
            StopTimesJishi = 0;
            TheResetAll();
            ReSetAll();
            TheSkillOver();
        }


        StartMove();
        GetStartLianDan();
        StopAtkInTime();
    }


    void GetRandomZidanType()
    {
        //21-普通子弹 22-3散弹  23-高速子弹  28-5连发
        List<int> t = new List<int>() {21,22,28 ,28};
        ZiDanTypeNum = t[GlobalTools.GetRandomNum(t.Count-1)];
    }

    [Header("技能开始的 声音")]
    public AudioSource SkillBeginAudio;
    protected override void GetTheStart()
    {
        if (_player && _player.GetComponent<RoleDate>().isDie)
        {
            StopTimesJishi = 0;
            TheResetAll();
            ReSetAll();
            TheSkillOver();
        }

        if (SkillBeginAudio) SkillBeginAudio.Play();

         _CurrentPos = this.transform.position;

        GetRandomZidanType();
        //转向 面向玩家
        //TurnToPlayer();

        //判断玩家 与自己的 距离  如果大于最小距离 就不动 发射子弹 否则就 移动

        if (Mathf.Abs(this.transform.position.x - _player.transform.position.x) >= 16)
        {
            //print("  距离大于 移动距离  不动 ");
            //TheResetAll();
            //ReSetAll();
            //TheSkillOver();
            IsStopInTime = true;
            return;
        }

        _TargetPos = GetTargetPos();
        GetMove();
        IsStartMove = true;
        _gameBody.IsJiasu = true;
    }


    //player 不在 最短距离内 不移动 进入stop 子弹
    bool IsStopInTime = false;
    //停留时时间
    float StopTimes = 1;
    float StopTimesJishi = 0;

    void StopAtkInTime()
    {
        if (!IsStopInTime) return;


        ZidanZhandou();
        if (Liandan)
        {
            return;
        }
        

        StopTimesJishi += Time.deltaTime;
        if (StopTimesJishi>= StopTimes)
        {
            StopTimesJishi = 0;
            TheResetAll();
            ReSetAll();
            TheSkillOver();
        }
    }



    bool IsCanFasheZidan = true;
    void ZidanTXStop()
    {
        if (StartAudio) StartAudio.Stop();
        if (StartParticle && StartParticle.gameObject.activeSelf)
        {
            print("特效 显示 stop");
            StartParticle.Stop();
            StartParticle.gameObject.SetActive(false);
        }

        if(TX_GaosuHuoyandanFashe && TX_GaosuHuoyandanFashe.gameObject.activeSelf)
        {
            TX_GaosuHuoyandanFashe.Stop();
            TX_GaosuHuoyandanFashe.gameObject.SetActive(false);
        }
    }

    [Header("高速火焰弹 发射前的 特效")]
    public ParticleSystem TX_GaosuHuoyandanFashe;


    float _FasheYanchiTime = 0;
    //战斗 子弹 怎么发射？
    protected void ZidanZhandou()
    {
        if (!IsCanFasheZidan) return;
        //是否有子弹 发射
        if (!IsHasFashe)
        {

            //高速狙击子弹发射
            if(ZiDanTypeNum == 23)
            {
                //播放 高速子弹播放特效
                //ZidanFasheTimes 延长
                if (TX_GaosuHuoyandanFashe &&!TX_GaosuHuoyandanFashe.gameObject.activeSelf)
                {
                    TX_GaosuHuoyandanFashe.gameObject.SetActive(true);
                    TX_GaosuHuoyandanFashe.Play();
                    _FasheYanchiTime = ZidanFasheTimes+0.4f;
                    //print("  *********************** _FasheYanchiTime  "+ _FasheYanchiTime);
                }
            }
            else
            {
                if (!StartParticle.gameObject.activeSelf)
                {
                    StartParticle.gameObject.SetActive(true);
                    StartParticle.Play();
                    _FasheYanchiTime = ZidanFasheTimes;
                }
            }

           

            ZidanFasheJishi += Time.deltaTime;
            //print("  jishi  "+ ZidanFasheJishi);
            if (ZidanFasheJishi >= _FasheYanchiTime)
            {
                ZidanTXStop();
                IsHasFashe = true;
                ZidanFasheJishi = 0;
                if (ZiDanTypeNum == 18|| ZiDanTypeNum == 28)
                {
                    //连弹***
                    //print("  *********************** 连弹************  ");
                    StartLiandan();
                    return;
                }


                if (SkillOutAudio) SkillOutAudio.Play();
               
                Fire();
            }
        }
    }



    float ZidanFasheJishi = 0;
    //子弹发射延迟时间
    float ZidanFasheTimes = 1;
    bool IsHasFashe = false;


    protected bool IsStartMove = false;
    protected void StartMove()
    {
        if (!IsStartMove) return;

        print("yd1");

        ZidanZhandou();
        if (Liandan)
        {
            print("yd2");
            //MoveToPoint(_TargetPos, 10);
            if (this.transform.position.x>=RightPos.position.x|| this.transform.position.x <= LeftPos.position.x)
            {
                this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            }

            return;
        }
        print("yd3   _TargetPos "+ _TargetPos);

        if (MoveToPoint(_TargetPos,10))
        {
            print("yd4");
            TheResetAll();
            ReSetAll();
            TheSkillOver();
            _gameBody.GetStand();
            print("远离 移动结束！！！！！！！");
        }
        print("yd5");
    }


    bool IsGetChaoXiang = false;
    //朝向玩家
    protected void TurnToPlayer()
    {
        if (!IsGetChaoXiang)
        {
            IsGetChaoXiang = true;
            if (this.transform.position.x > _player.transform.position.x)
            {
                //我在右
                _gameBody.TurnLeft();
            }
            else
            {
                _gameBody.TurnRight();
            }
        }
       
    }


    protected void TheResetAll()
    {
        _gameBody.isAcing = false;
        IsStartMove = false;
        _gameBody.IsJiasu = false;
        IsGetChaoXiang = false;
        IsStopInTime = false;
        StopTimesJishi = 0;

        //子弹发射 重置
        IsHasFashe = false;
        IsCanFasheZidan = true;
        ZidanFasheJishi = 0;

        ZidanTXStop();
        Liandan = false;

    }


    protected bool MoveToPoint(Vector2 TargetPos,float TempSpeed,float MaxSpeedX = 15)
    {
        Vector2 thisV2 = new Vector2(transform.position.x, transform.position.y);
        print("  v2-》  "+this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity);
        Vector2 v2 = (TargetPos - thisV2) * TempSpeed;// GlobalTools.GetVector2ByPostion(point, thisV2, TempSpeed);

        print("直接移动速度 v2 >>>>>>>>>>>>>>>>>>  " + v2);
        if (MaxSpeedX != 0 && Mathf.Abs(v2.x) > MaxSpeedX)
        {
            v2.x = v2.x > 0 ? MaxSpeedX : -MaxSpeedX;
            v2.y *= MaxSpeedX / Mathf.Abs(v2.x);
        }

        _gameBody.GetPlayerRigidbody2D().velocity = v2;
        //_gameBody.IsJiasu = true;
        print("_gameBody.GetPlayerRigidbody2D().velocity>>>>>>>>>>>>>>>>>>  " + _gameBody.GetPlayerRigidbody2D().velocity);
        if (GetComponent<AIAirRunNear>().IsHitWallByFX(v2, 2, thisV2))
        {
            print("   撞墙了！！！！！！！！！！！！！！！！！！  ");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            return true;
        }

        float _jinruDis = (thisV2 - TargetPos).sqrMagnitude;
        print(" _jinruDis  "+ _jinruDis);
        //print("两点间距离 " + _jinruDis+"   ------进入距离的 误差 内  "+ zuijiPosDisWC+ "  inDistance   " + inDistance+"    我的位置  "+thisV2+"    目标点 "+ point);
        //距离小于 误差内 直接结束
        if (_jinruDis < 1.8F)
        {
            print(thisV2 + "_jinruDis  --point   over!!! ");
            this.GetComponent<GameBody>().GetPlayerRigidbody2D().velocity = Vector2.zero;
            return true;
        }


        return false;
    }




    //移动到什么位置 左右 最大点
    [Header("左边最远点")]
    public Transform LeftPos;
    [Header("右边 最远点")]
    public Transform RightPos;

    [Header("X移动距离")]
    public float MoveDistance = 5;

    protected Vector2 _CurrentPos;
    protected Vector2 _TargetPos;
    protected Vector2 GetTargetPos()
    {
        float __x = 0;
        float __y = 0;



        //普通找点
        if (this.transform.position.x > _player.transform.position.x)
        {
            //我在 玩家 右侧
            __x = this.transform.position.x + MoveDistance;
            if(__x> RightPos.position.x)
            {
                //如果 找点 超过了 最右点   
                __x = _player.transform.position.x - MoveDistance * 2;
                _gameBody.TurnRight();
                MoveACName = "run_3";
                IsCanFasheZidan = false;
            }
            else
            {
                MoveACName = "run_6";
                _gameBody.TurnLeft();
            }
        }
        else
        {
            //我在玩家左侧
            __x = this.transform.position.x - MoveDistance;
            if (__x < LeftPos.position.x)
            {
                //如果 找点 超过了 最 左点
                __x = _player.transform.position.x + MoveDistance * 2;
                _gameBody.TurnLeft();
                MoveACName = "run_3";
                IsCanFasheZidan = false;
            }
            else
            {
                MoveACName = "run_6";
                _gameBody.TurnRight();
            }
        }
        __y = RightPos.position.y;
        _TargetPos = new Vector2(__x,__y);
        return _TargetPos;
    }



    //是否需要转向
    protected bool IsNeedZhuanxiang = false;

    [Header("移动动作名字")]
    public string MoveACName = "";


    protected void GetMove()
    {
        _gameBody.isAcing = true;
        _gameBody.GetDB().animation.GotoAndPlayByFrame(MoveACName);
    }

    //怎么写  一个是 固定 点位置移动
    //一个 是 自动 感应位置 来移动  移动固定距离





    //*********************发射子弹*******************************


    protected void ZidanACFashe()
    {
        //print(" dongzuo name shijian **********************ac jicheng!!!   " + ACScaleStartTimes);

        if (ZiDanTypeNum == 18|| ZiDanTypeNum == 28)
        {
            print("  进入 连弹！！！！！ ");
            StartLiandan();
            return;
        }

        //这里要实现 JNDate 赋值
        Fire();
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

        if (LiandanJishu >= 5)
        {
            TheSkillOver();
            ReSetAll();
            LiandanReset();

            TheResetAll();
            //ReSetAll();
            //TheSkillOver();
            return;
        }

        LiandanJishi += Time.deltaTime;
        if (LiandanJishi >= LiandanJiange)
        {
            print("   liandan sheji!!!!!!!!!!!! ");
            LiandanJishi = 0;
            LiandanJishu++;
            Fire();
        }
    }




    void StartLiandan()
    {
        _OverDelayTimes = OverDelayTimes;
        OverDelayTimes = 6;
        Liandan = true;
    }

    void LiandanReset()
    {
        LiandanJishi = 0;
        LiandanJishu = 0;
        OverDelayTimes = _OverDelayTimes;
        Liandan = false;
    }



    public AudioSource FireAudio;
    //发射子弹类型
    int ZiDanTypeNum = 18;
    //public GameObject _player;


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

        if (ZiDanTypeNum == 23)
        {
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
        }else if (ZiDanTypeNum == 21)
        {
            ZiDanName = "TX_zidan21";
        }else if (ZiDanTypeNum == 22)
        {
            ZiDanName = "TX_zidan21";
        }else if (ZiDanTypeNum == 28)
        {
            ZiDanName = "TX_zidan22";
        }


        GameObject zidan = GetZiDan();
        Vector2 v1 = ZidanFX();
        if(ZiDanTypeNum != 28)
        {
            zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v1, 10);
        }
        

        if (ZiDanTypeNum == 20|| ZiDanTypeNum == 23)
        {
            //高速子弹
            zidan.GetComponent<Rigidbody2D>().velocity = GlobalTools.GetVector2ByV2(v1, 40);
        }
        else if (ZiDanTypeNum == 2)
        {
            //3子弹
            SanLianSanDan(v1, 1, 40);
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
        }else if (ZiDanTypeNum == 22)
        {
            SanLianSanDan(v1, 1, 30);
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
        print("************************************************ZiDanName   " + ZiDanName);
        GameObject zidan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(ZiDanName) as GameObject);
        zidan.name = ZiDanName;
        //print("  zidan "+zidan);

        if (ZiDanTypeNum == 15 || ZiDanTypeNum == 16)
        {
            //火焰弹 
            zidan.GetComponent<OnLziHit>().SetCanHit();
        }
        zidan.transform.position = zidanDian1.position;
        if(zidan.GetComponent<TX_zidan>())zidan.GetComponent<TX_zidan>().CloseAutoFire();
        zidan.transform.localScale = this.transform.localScale;
        zidan.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        return zidan;
    }




}
