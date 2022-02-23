using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class JijiaGamebody : GameBody
{
    PlayerUI _playerUI;
    [Header("推进器 是否用完")]
    public bool IsJijiaGKOver = false;
    protected override void GetStart()
    {
        base.GetStart();
        roleDate = GetComponent<PlayerRoleDate>();
        //print("  xueliang------   "+roleDate.live);
        //roleDate.live = 2000;
        Globals.IsInJijia = true;
        if (IsInOnGroundSrop) {
            InOnGroundStop();
            GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<UI_Nengliangtiao>().HideSelf();
            if (IsJijiaGKOver)
            {
                //隐藏 推进器
                GetComponent<Jijia_Zhutuiqis>().HideZhutuiqi();
            }
        }
        else
        {
            ShowJijiaUI();
            GetPlayerRigidbody2D().gravityScale = 0;
            IsInAirFly = true;
            IsQifeiOver = true;
            Globals.IsInJijia = false;
            TX_penhuo1.Play();
            TX_penhuo2.Play();
            //2个喷火长短不一 保证循环不断
            A_Penhuo.Play();
            A_Penhuo2.Play();
        }
        
        //print("jiajia  "+ this.transform.localScale.x);
        ACReset();
        //if (this.transform.localScale.x == -1) GetComponent<GameBody>().TurnRight();
    }


    int daodanNums = 20;

    UI_Nengliangtiao _nengliangtiao;
    private void ShowJijiaUI()
    {
        _playerUI = GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<PlayerUI>();
        _playerUI.Txt_ganraodan.gameObject.SetActive(true);
        _playerUI.Txt_ganraodan.text = GanraodanNums.ToString();

        _playerUI.Txt_jipao.gameObject.SetActive(true);
        _playerUI.Txt_jipao.text = JipaoNums.ToString();

        _playerUI.Txt_daodan.gameObject.SetActive(true);
        _playerUI.Txt_daodan.text = daodanNums.ToString();


        GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<UI_lantiao>().HideSelf();
        //GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<UI_Nengliangtiao>().ShowSelf();
        _nengliangtiao = GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<UI_Nengliangtiao>();
        _nengliangtiao.ShowSelf();
    }


    //OnGroundStop 状态  骨骼中的机甲前板隐藏



    string ONGROUNDSTOP = "OnGroundStop_1";
    string ONJIJIA = "OnJijia_3";
    


    [Header("***************************************************机甲********************************************************")]
    public GameObject QianBan;

    int QianbanIndex = 0;

    [Header("是否 停在地面")]
    public bool IsInOnGroundSrop = false;
    void InOnGroundStop()
    {
        GetDB().animation.GotoAndStopByFrame(ONGROUNDSTOP);
        //print("  jijia   ---ting "+GetDB().animation.lastAnimationName);
        QianBan.SetActive(true);
        QianbanIndex = GetDB().armature.GetSlot("jqianJigai").displayIndex;
        //隐藏 前甲板骨骼
        GetDB().armature.GetSlot("jqianJigai")._SetDisplayIndex(-1);
        IsQifeiOver = false;
    }









    bool isTest = false;
    [Header("上机甲 点")]
    public UnityEngine.Transform OnPos;

    void BoneOrderXiuzheng(string SoltName = "", string ACName = "")
    {
        if (!isTest)
        {
            isTest = true;
            GetDB().armature.GetSlot("辫子右")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("辫子左_1")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海左")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海中")._SetDisplayIndex(1);
            GetDB().armature.GetSlot("刘海右")._SetDisplayIndex(1);
        }
    }

    void Update()
    {
        //print("点前 血量 是多少？     "+roleDate.live);
        GetUpdate();
    }

    bool IsJijiaDie = false;
    protected override void GetUpdate() {
       
        Qifei();

        //print("   ---rolelive "+roleDate.live+"  ---   "+roleDate.isDie);
        //roleDate.live-=4;
        if (roleDate.live<=0)
        {
            roleDate.isDie = true;
            
            if (!IsJijiaDie)
            {
                IsJijiaDie = true;
                GetComponent<Jijia_Zhutuiqis>().GetDieBaozhaKuai(GetPlayerRigidbody2D().velocity.x);
                GetPlayerRigidbody2D().gravityScale = 1;
            }
            StandInAir();
            GetJijiaDie();
            return;
        }

        if(IsQifeiOver) InAirUpdate();
    }


    void GetJijiaDie()
    {
        GaosuPenhuoStop();
        float __x = GetPlayerRigidbody2D().velocity.x * 0.98f;
        GetPlayerRigidbody2D().velocity = new Vector2(__x, GetPlayerRigidbody2D().velocity.y);
        GetDie();
    }


    public override void GetDie()
    {

        print(" jijia die!!!!!");
        if (!IsGetDieOut)
        {
            IsGetDieOut = true;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.CAMERA_SHOCK, "z2-0.8"), this);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT, roleDate.enemyType), this);
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.STOP_DAOJISHI, null), this);
            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.STOP_DAOJISHI, this.StopDJS);

            if (isDieRemove) StartCoroutine(IEDieDestory(2f));
        }

        
    }


    public override IEnumerator IEDieDestory(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        //playerRigidbody2D.velocity = Vector2.zero;


        print("@@@@@@@@@@@@  this.tag  "+this.tag);

        //Destroy(this.gameObject);
        if (this.tag == "Player"|| this.tag == GlobalTag.PlayerJijiaObj)
        {
            Time.timeScale = 1f;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_OVER), this);
            Globals.isGameOver = true;
            //this.gameObject.SetActive(false);
        }
        else
        {
            if (this.GetComponent<RoleDate>().enemyType == "enemy")
            {
                //Destroy(this);
                this.gameObject.SetActive(false);
            }

            //DestroyImmediate(this, true);
        }
        //this.gameObject.SetActive(false);

        //Destroy(this);
    }




    bool IsQifeiOver = false;
    void Qifei()
    {
        if (IsQifeiOver) return;
        //登上机甲完成
        OnJijiaOver();
        //点火
        Dianhuo();
        //起飞一段高度
        QifeiUp();
    }

    //乘坐机甲
    public void OnJiajia()
    {
        //throw new NotImplementedException();
        GetDB().armature.GetSlot("jqianJigai")._SetDisplayIndex(QianbanIndex);
        GetDB().animation.GotoAndPlayByFrame(ONJIJIA,0,1);
        QianBan.SetActive(false);
        IsOnJiaOver = true;
        A_Guanmen.Play();
        ShowJijiaUI();
    }

    public void GetUI()
    {
        GlobalTools.FindObjByName(GlobalTag.MAINCAMERA).GetComponent<CameraController>().GetTargetObj(this.transform);
        OnJiajia();
        GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<XueTiao>().GetGameObj(GetComponent<RoleDate>());
    }


    bool IsOnJiaOver = false;
    bool IsJijiaQidongAudioPlay = false;
    void OnJijiaOver()
    {
        if (!IsOnJiaOver) return;
       
        if (DBBody.animation.lastAnimationName == ONJIJIA && DBBody.animation.isCompleted)
        {
            IsDianhuo = true;
            IsOnJiaOver = false;
            TXDianhuo();
            //GetComponent<PlayerRoleDate>().GetStart();
            //GlobalTools.FindObjByName(GlobalTag.PLAYERUI).GetComponent<XueTiao>().GetTargetObj(this.gameObject);
            
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.PLAYER_ZT), this.gameObject);
            //GetComponent<PlayerRoleDate>().maxLive = 2000;
            //GetComponent<PlayerRoleDate>().live = 2000;
            if (!IsJijiaQidongAudioPlay)
            {
                IsJijiaQidongAudioPlay = true;
                //A_JijiaQidong.Play();
                A_Dianhuo.Play();
            }
        }
    }

    [Header("喷火特效 1")]
    public ParticleSystem TX_penhuo1;
    [Header("喷火特效 2")]
    public ParticleSystem TX_penhuo2;

    [Header("启动烟幕 1")]
    public ParticleSystem TX_qidongYanmu1;
    [Header("启动烟幕 2")]
    public ParticleSystem TX_qidongYanmu2;

    [Header("启动烟幕 左")]
    public ParticleSystem TX_qidongYanmuL;
    [Header("启动烟幕 右")]
    public ParticleSystem TX_qidongYanmuR;

    void TXDianhuo()
    {
        TX_penhuo1.Play();
        TX_penhuo2.Play();
        TX_qidongYanmu1.Play();
        TX_qidongYanmu2.Play();
        //2个喷火长短不一 保证循环不断
        A_Penhuo.Play();
        A_Penhuo2.Play();

    }


    [Header("********高速 喷火特效1")]
    public ParticleSystem TX_penhuoGS1;
    [Header("高速 喷火特效2")]
    public ParticleSystem TX_penhuoGS2;
    [Header("高速 喷火特效3")]
    public ParticleSystem TX_penhuoGS3;
    void GaosuPenhuo()
    {
        TX_penhuoGS1.Play();
        TX_penhuoGS2.Play();
        TX_penhuoGS3.Play();
    }

    void GaosuPenhuoStop()
    {
        if (TX_penhuoGS1.isPlaying)
        {
            TX_penhuoGS1.Stop();
            TX_penhuoGS2.Stop();
            TX_penhuoGS3.Stop();
        }
    }


   









    bool IsDianhuo = false;
    float DianhuoJishi = 4;
    float DianhuoJishiNums = 0;
    void Dianhuo()
    {
        if (!IsDianhuo) return;
        //print("???>>>>>");
        DianhuoJishiNums +=Time.deltaTime;
        if (DianhuoJishiNums>= DianhuoJishi)
        {
            //IsQifeiOver = true;
            IsDianhuo = false;
            _chushiY = this.transform.position.y;
            IsQifeiUp = true;
            GetDB().animation.FadeIn(STANDINAIR,0.4f);
            GetPlayerRigidbody2D().gravityScale = 0;
            TX_qidongYanmu1.Stop();
            TX_qidongYanmuL.Play();
            TX_qidongYanmuR.Play();
            TX_qidongYanmu2.Stop();

        }
    }

    bool IsQifeiUp = false;
    float _chushiY = 0;
    void QifeiUp()
    {
        if (!IsQifeiUp) return;
        float __y = this.transform.position.y + 0.1f;
        this.transform.position = new Vector2(this.transform.position.x,__y);
        //print("fly Up!!!     "+ this.transform.position.y+ "  _chushiY   "+ _chushiY);
        if (this.transform.position.y - _chushiY >= 7)
        {


        }


        if (this.transform.position.y- _chushiY >= 20)
        {
            _chushiY = 0;
            IsQifeiUp = false;
            IsQifeiOver = true;
            Globals.isInPlot = false;
            IsInAirFly = true;
            ACReset();
            //print("  ?>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>  qifeiwancheng!!!! ");


        }
    }
   


    void JijiaQifeiReset()
    {
        IsJijiaQidongAudioPlay = false;


        DianhuoJishiNums = 0;
        _chushiY = 0;
        IsDianhuo = false;
        IsQifeiOver = false;
        IsQifeiUp = false;
        IsOnJiaOver = false;
    }

    [Header("上机舱关门**************机甲音效******")]
    public AudioSource A_Guanmen;

    [Header("机甲启动")]
    public AudioSource A_JijiaQidong;

    [Header("自检1")]
    public AudioSource A_Zijian1;

    [Header("自检2")]
    public AudioSource A_Zijian2;

    [Header("自检3")]
    public AudioSource A_Zijian3;

    [Header("点火")]
    public AudioSource A_Dianhuo;

    [Header("喷火")]
    public AudioSource A_Penhuo;

    [Header("喷火2 2个声音长短不一循环 保证没中断音")]
    public AudioSource A_Penhuo2;



    protected override void ShowACTX(string type, EventObject e)
    {

       
        if (type == EventObject.SOUND_EVENT)
        {
            print("  type   "+ type+"     "+e.name);
            if (e.name == "jijiaChibangJiance"|| e.name == "jijiaHuojianJiance")
            {
                if (GlobalTools.GetRandomNum() > 50)
                {
                    A_Zijian2.Play();
                }
                else
                {
                    A_Zijian1.Play();
                }
            }
        }


        if (type == EventObject.FRAME_EVENT)
        {

            if (e.name == "jn_begin")
            {
              
            }


            if (e.name == "ac")
            {
                
            }
        }
    }






    //机甲在空中 飞行 模式
    string STANDINAIR = "standInAir_1";
    string FLYQIAN = "qianFei_1";
    string FLYHOU = "houFei_1";
    string FLYUPORDOWN = "standInAir_1";

    bool IsInAirFly = false;

    
    


    void InAirUpdate()
    {
        //print("  InAirUpdate  "+ IsInAirFly);
        if (!IsInAirFly) return;


        IsHitWallFront();

        SFNengliangPao();
        if (IsShowNengliangPao)
        {
            IsGaosuFly = false;
            FlyAC();
            return;
        }

        //SFGanraodan();
        SFGanraodan2();
        SFJipao();
        

        UpDownXianding();

        if (IsGaosuFly)
        {
            FlyGaosuUpdate();
            return;
        }

        FlyAC();


        
    }


    float CFlySpeed = 0;
    void IsHitWallFront()
    {
        //print(" ------前面撞墙时候的速度 1111-----  " + GetPlayerRigidbody2D().velocity.x);
        
        if (IsHitMQWall)
        {
            print(" CFlySpeed   "+ CFlySpeed + "  前面撞墙时候的速度  "+ GetPlayerRigidbody2D().velocity.x);
            float SpeedD = CFlySpeed - GetPlayerRigidbody2D().velocity.x;
            if (SpeedD >= 80)
            {
                print("前面撞墙时候的速度     ----------高速撞墙 撞毁！！");
                roleDate.live = 0;
            }else if (SpeedD>=40)
            {
                print("前面撞墙时候的速度     ----------高速撞墙 *********受伤！！");
                roleDate.live -= 500;
            }
            print(" ------前面撞墙时候的速度   "+GetPlayerRigidbody2D().velocity.x);
        }
        CFlySpeed = GetPlayerRigidbody2D().velocity.x;
    }





    public UnityEngine.Transform TopPos;
    public UnityEngine.Transform DownPos;
    //上下边界限定
    void UpDownXianding()
    {
        if (TopPos)
        {
            if (this.transform.position.y > TopPos.position.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, TopPos.transform.position.y, this.transform.position.z);
            }
        }

        if (DownPos)
        {
            if (this.transform.position.y < DownPos.position.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, DownPos.transform.position.y, this.transform.position.z);
            }
        }
    }


    

    float tuili = 1;


  

    float xF = 0;
    void FlyAC()
    {
        //print(" xForce  "+ IsQianfei);
        if (IsGaosuFlying&& _nengliangtiao.GetNengliang() > 0) return;
        xF = xForce;
        if (IsQianfei && (IsUp || IsDown))
        {
            print("前斜");
            xF = xForce * 0.6f;
        }


        if (IsQianfei)
        {
            if(!IsShowNengliangPao)ACFlyQian();
            //xF = xForce * 1.2f;
            GetZongTuili(new Vector2(xF * tuili, 0));
        }
        else if (IsHoufei)
        {
            if (!IsShowNengliangPao) ACFlyHou();
            xF = xForce * 0.2f;
            //print("后飞 ---->");
            GetZongTuili(new Vector2(xF * -tuili, 0));
        }
        else
        {
            IsQianfei = false;
            IsHoufei = false;
            //playerRigidbody2D.velocity = new Vector2(0,playerRigidbody2D.velocity.y);
            if(!IsShowNengliangPao)StandInAir();
        }

        if (IsUp)
        {
            xF = xForce * 0.6f;
            GetZongTuili(new Vector2(0, xF * tuili));
        }
        else if (IsDown)
        {
            xF = xForce * 0.6f;
            GetZongTuili(new Vector2(0, -xF * tuili));
        }
        else
        {
            IsUp = false;
            IsDown = false;

            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, playerRigidbody2D.velocity.y*0.8f);
        }

        ControlSpeed(QianfeiSpeed);
    }

    float QianfeiSpeed = 25;


    public override void ControlSpeed(float vx = 0)
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        //print("进入 速度限制！！！！！！  speedX  " + speedX+ "       maxSpeedX ---    " + maxSpeedX);
        //钳制 speedX 被限制在 -maxSpeedX  maxSpeedX 之间
        float newSpeedX;
        float newSpeedY;
        if (vx == 0)
        {
            newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
            newSpeedY = Mathf.Clamp(speedY, -maxSpeedX, maxSpeedX);
        }
        else
        {
            newSpeedX = Mathf.Clamp(speedX, -vx, vx);
            newSpeedY = Mathf.Clamp(speedY, -vx, vx);
        }

        //if (horizontalDirection == 0) newSpeedX/=10;
        newSpeed.x = newSpeedX;
        newSpeed.y = newSpeedY;
        //获取向量速度
        playerRigidbody2D.velocity = newSpeed;
    }


    bool IsACFlyQian = false;
    void ACFlyQian()
    {
        if (!IsACFlyQian)
        {
            IsACFlyQian = true;
            IsACFlyHou = false;
            IsInStandInAir = false;
            GetPlayerRigidbody2D().velocity = Vector2.zero;
            if (GetDB().animation.lastAnimationName != FLYQIAN)
            {
                //GetDB().animation.Stop();
                //GetDB().animation.FadeIn(FLYQIAN, 0.1f);
                GetDB().animation.GotoAndPlayByFrame(FLYQIAN);
            }
        }
    }

    bool IsACFlyHou = false;
    void ACFlyHou()
    {
        if (!IsACFlyHou)
        {
            IsACFlyHou = true;
            IsACFlyQian = false;
            IsInStandInAir = false;
            if (GetDB().animation.lastAnimationName != FLYHOU)
            {
                //GetDB().animation.Stop();
                //GetDB().animation.FadeIn(FLYHOU, 0.1f);
                GetDB().animation.GotoAndPlayByFrame(FLYHOU);
            }
        }
    }


    string GAOSUFLYAC = "gaosuQianFei_1";

    bool IsInStandInAir = false;
    void StandInAir()
    {
        //if (!IsQianfei && !IsHoufei && !IsUp && !IsDown) playerRigidbody2D.velocity *= 0.99f;

        if (!IsQianfei && !IsHoufei&&!IsJijiaDie) playerRigidbody2D.velocity *= 0.99f;

        if (!IsInStandInAir)
        {
            IsInStandInAir = true;
            IsACFlyQian = false;
            IsACFlyHou = false;
            if (GetDB().animation.lastAnimationName != STANDINAIR)
            {
                //GetDB().animation.Stop();
                //GetDB().animation.FadeIn(STANDINAIR, 0.1f);
                GetDB().animation.GotoAndPlayByFrame(STANDINAIR);
            }
        }
    }





    


    void StopGaosuFly()
    {
        if (IsGaosuFlying)
        {
            IsGaosuFlying = false;
            GaosuPenhuoStop();
            GetDB().animation.FadeIn(STANDINAIR, 0.1f);
        }
    }

    bool IsGaosuFlying = false;
    void FlyGaosuUpdate()
    {
        if (!IsGaosuFlying)
        {
            IsGaosuFlying = true;
            if (GetDB().animation.lastAnimationName != GAOSUFLYAC) GetDB().animation.FadeIn(GAOSUFLYAC, 0.3F);
        }

        GaosuFly();

        //print(GetDB().animation.lastAnimationState.isCompleted+"  ?? "+ GetDB().animation.lastAnimationState.totalTime+ "  currentTime-- " + GetDB().animation.lastAnimationState.currentTime+ "  currentPlayTimes- " + GetDB().animation.lastAnimationState.currentPlayTimes);
        //print(GetDB().animation.lastAnimationName+ "  GAOSUFLYAC    "+ GAOSUFLYAC+ "     DBBody.animation.isCompleted    "+ GetDB().animation.isCompleted);
        if (GetDB().animation.lastAnimationName == GAOSUFLYAC && GetDB().animation.lastAnimationState.totalTime - GetDB().animation.lastAnimationState.currentTime<=0.05f)
        {
            if (TX_penhuoGS1.isStopped)
            {
                //print("   ---->>>>>>>>>>> ");
                GaosuPenhuo();
            }
        }

        
    }


    string GaosuFlyUpOrDown = "gaosuShangfei_1";
    void GaosuUpDownFly()
    {
        if (GetDB().animation.lastAnimationName != GaosuFlyUpOrDown) GetDB().animation.GotoAndPlayByFrame(GaosuFlyUpOrDown);
    }

    void GaosuQainFly()
    {
        if (GetDB().animation.lastAnimationName != GAOSUFLYAC)
        {
            //GetDB().animation.Stop();
            //GetDB().animation.FadeIn(STANDINAIR, 0.1f);
            GetDB().animation.GotoAndPlayByFrame(GAOSUFLYAC);
        }
    }



    void GaosuFly()
    {
        //如果 高速喷火特效 没有播放 return
        if (TX_penhuoGS1.isStopped) return;
        xF = xForce*2;
        if (IsQianfei && (IsUp || IsDown))
        {
            print("前斜");
            xF = xForce*2 * 0.5f;
        }


        GetZongTuili(new Vector2(xF * tuili, 0));

        //if (IsQianfei)
        //{
        //    //ACFlyQian();
        //    //xF = xForce * 1.2f;
            
        //}
        //else if (IsHoufei)
        //{
         
        //}
        //else
        //{
        //    IsQianfei = false;
        //    IsHoufei = false;
        //    //playerRigidbody2D.velocity = new Vector2(0,playerRigidbody2D.velocity.y);
        //    StandInAir();
        //}

        if (IsUp)
        {
            xF = xForce * 0.6f*2;
            GetZongTuili(new Vector2(0, xF * tuili));
            GaosuUpDownFly();
        }
        else if (IsDown)
        {
            xF = xForce * 0.6f*2;
            GetZongTuili(new Vector2(0, -xF * tuili));
            GaosuUpDownFly();
        }
        else
        {
            IsUp = false;
            IsDown = false;

            GaosuQainFly();

            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, playerRigidbody2D.velocity.y * 0.8f);
        }

        ControlSpeed(MaxSpeed);

    }

    float MaxSpeed = 88;

    void ACReset()
    {
        IsUp = false;
        IsDown = false;
        IsQianfei = false;
        IsHoufei = false;

        IsACFlyQian = false;
        IsACFlyHou = false;
        IsInStandInAir = false;

    }

  





    public void InStandInAir()
    {
        IsUp = false;
        IsDown = false;
        IsHoufei = false;
        IsQianfei = false;

        IsACFlyQian = false;
        StandInAir();
    }

    //********操作部分***************************************************************
    //机炮
    bool IsJipao = false;
    public void Jipao(bool IsTrue = true)
    {
        IsJipao = IsTrue;
    }

    bool IsQianfei = false;
    public void FlyQian(bool isTrue = true)
    {

        IsQianfei = isTrue;
        if (isTrue) IsHoufei = false;
        //print(" -------------->???????  IsQianfei " + IsQianfei);
    }

    bool IsHoufei = false;
    public void FlyHou(bool isTrue = true)
    {
        IsHoufei = isTrue;
        if (isTrue) IsQianfei = false;
        //print(" -------------->???????  FlyHou " + IsHoufei);
    }

    bool IsUp = false;
    public void FlyUp(bool isTrue = true)
    {
        IsUp = isTrue;
        if (isTrue) IsDown = false;
    }

    bool IsDown = false;
    public void FlyDown(bool isTrue = true)
    {
        IsDown = isTrue;
        if (isTrue) IsUp = false;
    }

    public void Ganraodan(bool IsTrue = true)
    {
        IsSFGanraodan = IsTrue;
    }

    bool IsFSGZDaodan = false;
    public void GZDaodan(bool IsTrue = true)
    {
        //IsSFGanraodan = IsTrue;
        if(!IsFSGZDaodan && IsTrue)
        {
            IsFSGZDaodan = true;
            ShowGZDaodan();
        }
        if (!IsTrue) IsFSGZDaodan = false;

    }

    string GZDaodanName = "DD_GZDaodanPlayer";            
    private void ShowGZDaodan()
    {
        if (daodanNums == 0) {
            MeiGanraodan.Play();
            return;
        }
        daodanNums--;
        _playerUI.Txt_daodan.text = daodanNums.ToString();
        GameObject daodan = GlobalTools.GetGameObjectInObjPoolByName(GZDaodanName);
        daodan.transform.position = new Vector3(this.transform.position.x,this.transform.position.y-1,this.transform.position.z);
        daodan.transform.parent = this.transform.parent;
        daodan.GetComponent<DD_GZDaodanPlayer>().SetQSSpeedX(this.GetComponent<Rigidbody2D>().velocity.x);
    }






    //高速飞行
    bool IsGaosuFly = false;
    public void GaosuFly(bool isTrue = true)
    {
        if (!_nengliangtiao)
        {
            return;
        }
        if (!IsGaosuFlying && _nengliangtiao.GetNengliang() <= 0)
        {
            if (isTrue)
            {
                FlyQian(true);
                ACFlyQian();
            }

            return;
        }

        if (_nengliangtiao.GetNengliang() <= 0)
        {
            //播放 没能量声音
            //_nengliangtiao.StopXiaohaoNengliang();
            //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.ZHUTUIQI_TUOLI, this.ZhutuiqiTuoli);
            //助推器 脱离
            if (!IsPaoFuyouxiang)
            {
                IsPaoFuyouxiang = true;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZHUTUIQI_TUOLI), this);
            }
            //ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZHUTUIQI_TUOLI), this);
            //抛弃助推器后 速度加到40
            QianfeiSpeed = 40;
            isTrue = false;
            //抛助推器

        }

        if (isTrue)
        {
            _nengliangtiao.XiaohaoNengliang();
        }
        else
        {
            _nengliangtiao.StopXiaohaoNengliang();
        }
        IsGaosuFly = isTrue;
        //print("  ???????>>>>>>>GaosuFlyGaosuFly "+ isTrue);
        if (!isTrue)
        {
            StopGaosuFly();
        }
    }













    [Header("机炮 发射点")]
    public UnityEngine.Transform JipaoFSPos;
    int JipaoNums = 1000;
    float JipaoJishi = 0.02f;
    float JipaoJishiNums = 0;
    public void SFJipao()
    {
        if (JipaoNums <= 0)
        {
            if (IsJipao) MeiGanraodan.Play();
            IsJipao = false;
        }

        if (!IsJipao) return;
        JipaoJishiNums += Time.deltaTime;
        if (JipaoJishiNums >= JipaoJishi)
        {
            JipaoNums--;
            _playerUI.Txt_jipao.text = JipaoNums.ToString();
            JipaoJishiNums = 0;
            GetJipao();
        }
    }

    void GetJipao()
    {
        //JipaoFSPos;
        float __x = 140;
        float __y = 0;
        GameObject o = GlobalTools.GetGameObjectByName("TX_Jipao");
        o.name = "TX_Jipao";
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(__x, __y);
        o.transform.position = new Vector3(JipaoFSPos.position.x+1, JipaoFSPos.position.y + 0.3f - GlobalTools.GetRandomDistanceNums(0.6f), JipaoFSPos.position.z); //JipaoFSPos.position;
        o.transform.parent = this.transform.parent;
    }





   

    [Header("干扰弹 发射点")]
    public UnityEngine.Transform GanraoDanPos;
    bool IsSFGanraodan = false;
    int GanraoDanNums = 20;
    float jishi = 0.02f;
    float jishiNums = 0;
    //public void SFGanraodan()
    //{
    //    if (!IsSFGanraodan) return;
    //    jishiNums += Time.deltaTime;
    //    if (jishiNums>= jishi)
    //    {
    //        jishiNums = 0;
    //        GanraoDanNums--;
    //        print(" 干扰弹 "+ GanraoDanNums);
    //        if (GanraoDanNums == 0)
    //        {
    //            GanraoDanNums = 20;
    //            IsSFGanraodan = false;
    //        }
    //        Gaoraodan();
    //    }
    //}


    [Header("***没有干 扰弹了")]
    public AudioSource MeiGanraodan;

    int GanraodanNums = 200;
    void SFGanraodan2()
    {
        if (GanraodanNums <= 0) {
            if (IsSFGanraodan) MeiGanraodan.Play();
            IsSFGanraodan = false;
        }
        
        if (!IsSFGanraodan) return;
        jishiNums += Time.deltaTime;
        if (jishiNums >= jishi)
        {
            GanraodanNums--;
            _playerUI.Txt_ganraodan.text = GanraodanNums.ToString();
            jishiNums = 0;
            Gaoraodan();
        }
    }

    void Gaoraodan()
    {
        float __x = 5 + GlobalTools.GetRandomDistanceNums(9);
        float __y = 4 + GlobalTools.GetRandomDistanceNums(16);
        GameObject o = GlobalTools.GetGameObjectByName("TX_GanraoDan");
        o.name = "TX_GanraoDan";
        o.GetComponent<Rigidbody2D>().velocity = new Vector2(__x, __y);
        o.transform.position = GanraoDanPos.position;
        o.transform.parent = this.transform.parent;
    }


    void JijiaUI()
    {
        
    }


    void DiuZhutuiqi()
    {

    }


    string ACNengliangPaoName = "nengliangPao_1";
    public GameObject TX_NengliangPao;
    bool IsShowNengliangPao = false;
    //能量炮
    public void ShowNengliangPao(bool IsTrue = true)
    {
        IsShowNengliangPao = IsTrue;
        if (!IsTrue)
        {
            _nengliangtiao.StopXiaohaoNengliang();
        }
        print("   ??????---- "+ IsShowNengliangPao);
    }

    bool IsPaoFuyouxiang = false;
    float Nengliangjishi = 0;

    void SFNengliangPao()
    {
        if (roleDate.isDie)
        {
            TX_NengliangPao.GetComponent<Skill_JiguangSaoshe>().JiguangStop();
            _nengliangtiao.StopXiaohaoNengliang();
            return;
        }

        if (IsShowNengliangPao&&_nengliangtiao.GetNengliang() <= 0) {
            IsShowNengliangPao = false;
            TX_NengliangPao.GetComponent<Skill_JiguangSaoshe>().JiguangStop();
            if (!IsPaoFuyouxiang)
            {
                IsPaoFuyouxiang = true;
                ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.ZHUTUIQI_TUOLI), this);
            }
            
            return;
        }

        if (IsShowNengliangPao)
        {
            
            print("   nengliangpao???!!!!! "+isAcing);
            isAcing = true;
            if (GetComponent<GameBody>().GetDB().animation.lastAnimationName!= ACNengliangPaoName)
            {
                GetComponent<GameBody>().GetDB().animation.FadeIn(ACNengliangPaoName, 0.1f, 1);
                
                GaosuPenhuoStop();
                Nengliangjishi = 0;
            }
            else
            {
                Nengliangjishi += Time.deltaTime;
                if (Nengliangjishi >= 0.2f)
                {
                    _nengliangtiao.XiaohaoNengliangNengliangPao();
                    TX_NengliangPao.GetComponent<Skill_JiguangSaoshe>().JiguangStart();
                }
            }
        }
        else
        {
            
            //print("jiguang Stop!!!!");
            TX_NengliangPao.GetComponent<Skill_JiguangSaoshe>().JiguangStop();
            //GetComponent<GameBody>().GetDB().animation.FadeIn(STANDINAIR, 0.2f, 1);
        }
    }







    public override void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        isRunYing = false;
        //isInAiring = false;
        isDowning = false;
        //在空中被击中 如果关闭跳跃bool会有落地bug
        //isJumping = false;
        // isJumping2 = false;
        //isJump2 = false;
        //isQiTiao = false;
        isAtk = false;
        isAtking = false;
        atkNums = 0;
        isAtkYc = false;
        isACCompletedYanchi = false;
        isBackUp = false;
        isBackUping = false;
        isQianhua = false;
        isQianhuaing = false;
        isAcing = false;
        _acName = "";
        //isYanchi = false;
        isSkilling = false;
        IsSFSkill = false;
        isSkillOut = false;
        isDodge = false;
        isDodgeing = false;
        isTXShow = false;
        yanchiTime = 0;
        IsJiasu = false;
        if (roleDate) roleDate.isBeHiting = false;
        //if (playerRigidbody2D != null && playerRigidbody2D.gravityScale != _recordGravity) playerRigidbody2D.gravityScale = _recordGravity;

        acingTime = 0;

        IsInACingYancheng = false;
        IsHuaFang = false;


        GedangReset();

        //if (GetDB()) GetDB().animation.Reset();

    }




}
