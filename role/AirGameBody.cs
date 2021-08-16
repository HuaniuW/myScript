using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class AirGameBody : GameBody {

    // Use this for initialization
    //void Start () {
    //print(this.DBBody == null);
    //if(DBBody == null) DBBody = GetComponentInChildren<UnityArmatureComponent>();
    //roleDate = GetComponent<RoleDate>();
    //print(this.rigidbody2D)
    //this.TestsI();
    //GetStart();
    //}


    [Header("默认的 移动动作")]
    public string RunACName = "run_3";
    protected override void FirstStart()
    {
        RUN = RunACName;
        if (!GetDB().animation.HasAnimation(RUN)) RUN = "run_3";
    }

   


    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float TCDistance = 0.13f;


    [Header("顶部探测点")]
    public UnityEngine.Transform groundCheckTop;

    [Header("冲击烟幕")]
    public ParticleSystem CJYanmu;

    bool _isHitTop = false;
    public bool IsHitTop
    {
        get
        {
            if (groundCheckTop == null) return false;
            Vector2 start = groundCheckTop.position;
            Vector2 end = new Vector2(start.x, start.y + TCDistance);
            Debug.DrawLine(start, end, Color.yellow);
            _isHitTop = Physics2D.Linecast(start, end, groundLayer);
            return _isHitTop;
        }
    }


    [Header("底部探测点")]
    public UnityEngine.Transform groundCheckDown;

    bool _isHitDown = false;
    public bool IsHitDown
    {
        get
        {
            if (groundCheckDown == null) return false;
            Vector2 start = groundCheckDown.position;
            Vector2 end = new Vector2(start.x, start.y - TCDistance);
            Debug.DrawLine(start, end, Color.yellow);
            _isHitDown = Physics2D.Linecast(start, end, groundLayer);
            return _isHitDown;
        }
    }


    [Header("探测地面图层")]
    public LayerMask TCGroundLayer;



    // Update is called once per frame
    void Update () {
        //if (this.tag != "Player") print("   ??IsHitWall     " + IsHitWall);
        this.GetUpdate();
	}

    public override void Testss()
    {
        print("AirGamebody!");
    }

    public void Test22()
    {
        print("222");
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
        //isYanchi = false;
        isBackUp = false;
        isBackUping = false;
        isQianhua = false;
        isQianhuaing = false;
        isAcing = false;
        _acName = "";
        //isYanchi = false;
        isSkilling = false;
        isSkillOut = false;
        if (roleDate) roleDate.isBeHiting = false;
    }

    

    protected override void Stand()
    {
        if (CJYanmu) CJYanmu.Stop();
        if (GetComponent<RoleDate>().isDie) return;
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //print("@@@@@@@@@@@@@@@@@  >  "+DBBody.animation.lastAnimationName+"   atking "+isAtking+"  ??stand  "+STAND);
        if (IsZJToStandType)
        {
            if (DBBody.animation.lastAnimationName != STAND || (DBBody.animation.lastAnimationName == STAND && DBBody.animation.isCompleted))
            {
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //print(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ fadein stand!!!!!! ");
                //DBBody.animation.FadeIn(STAND, 0.2f);
            }
        }
        else
        {
            //print(" stand!!!! jinlaile  mei!!!! "+ DBBody.animation.lastAnimationName);
            if (DBBody.animation.lastAnimationName != STAND)
            {
                //DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //print(" @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ fadein stand!!!!!! ");
                DBBody.animation.FadeIn(STAND, 0.6f);
            }
        }
       

        isDowning = false;
        if (newSpeed.x > slideNum)
        {
            newSpeed.x = slideNum - 1;
        }
        else if (newSpeed.x < -slideNum)
        {
            newSpeed.x = -slideNum + 1;
        }

        playerRigidbody2D.velocity = newSpeed;
    }

    [Header("飞行怪物 移动时候的 声音")]
    public AudioSource RunAudio;

    public override void Run()
    {
        //print("isJumping   "+ isJumping+ "    isDowning  "+ isDowning+ "   isBeHiting  " + roleDate.isBeHiting+ "isInAiring" + isInAiring+ "   isDodgeing  " + isDodgeing);
        //print("is run--------------------------------------------> "+RUN);
        //if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (roleDate.isBeHiting) {
            if (RunAudio && RunAudio.isPlaying) RunAudio.Stop();
            return;
        }

        //print(">>>>>>>>>>>>>>>>>>>>>>>>>>> !!!! run  为什么进不来！！！！ "+ DBBody.animation.lastAnimationName+"  播放速率  "+ DBBody.animation.timeScale+"   ----  "+isAcing);

        if (DBBody.animation.lastAnimationName != RUN)
        {
            //print("?????run     "+ DBBody.animation.lastAnimationName +"     RUN是什么动作  "+RUN);
            DBBody.animation.GotoAndPlayByFrame(RUN);
            if (RunAudio) RunAudio.Play();
            //print("??run  "+ DBBody.animation.lastAnimationName);
        }
        else
        {
            if (!DBBody.animation.isPlaying) {
                //print("@@@@@@  进入这里 引动了 ");
                DBBody.animation.Play();
            }
           
        }
      
    }

    public override void InAir()
    {
        //print("hi");
    }

    //public override bool IsGround
    //{
    //    get
    //    {
    //        return true;
    //    }
    //}

    public override bool IsEndGround
    {
        get
        {
            return false;
        }
    }

    protected override void Yanmu()
    {
        
    }


    //public bool IsDieFlyOut = false;
    //public bool IsDieRotation = false;
    

    bool isDieAc = false;
    public override void GetDie()
    {
        //playerRigidbody2D.velocity = Vector2.zero;
        //GetComponent<AIAirRunNear>().ResetAll();
        if(CJYanmu) CJYanmu.Play();
        if (DBBody.animation.lastAnimationName != DIE) {
            if (IsDieFlyOut)
            {
                GetPlayerRigidbody2D().gravityScale = 4;
                if (hitKuai) hitKuai.SetActive(false);
                //print("------------------------------------------> " + GetPlayerRigidbody2D().freezeRotation);
                //print("废除取 之前 我的 速度   "+ GetPlayerRigidbody2D().velocity);
                if(roleDate.TiXing<=2)BeHitFlyOut(100);
                //if(IsDieFlyOut) GetPlayerRigidbody2D().freezeRotation = false;
            }


            //print("  DIE "+DIE);
            if (!isDieAc)DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
            isDieAc = true;

            if (roleAudio)
            {
                //print("  die audio!!!!! ");
                if(roleDate.enemyType == "boss")
                {
                    if (roleAudio.die_1) roleAudio.die_1.Play();
                }
                else if (GlobalTools.GetRandomNum() >= 30)
                {
                    //roleAudio.PlayAudio("die_1");
                    if (roleAudio.die_1) roleAudio.die_1.Play();
                }
            }


            if (IsDieBoomOutObj) DieBoomOutObj();
        }
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT), this);
        if (isDieRemove) StartCoroutine(IEDieDestory(5f));
    }

    public override void HasBeHit(float chongjili = 0,bool IsOther = true)
    {
        if (!DBBody) return;
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;

        //if (GetComponent<AIZhaohuan>())
        //{
        //    print("beihit in zhaohuan!!!");
        //    GetComponent<AIZhaohuan>().LiveInZhaohuan();
        //}

        if (roleAudio && roleDate.live>=0)
        {
            if (GlobalTools.GetRandomNum() >= 40)
            {
                roleAudio.PlayAudio("BeHit_1");
            }
        }


        if (isInAiring)
        {
            if (DBBody.animation.HasAnimation(BEHITINAIR))
            {
                DBBody.animation.GotoAndPlayByFrame(BEHITINAIR, 0, 1);
                //if (GetComponent<AIBase>()) GetComponent<AIBase>().AIGetBeHit();
                return;
            }
        }
        if(DBBody.animation.HasAnimation(BEHIT)) DBBody.animation.GotoAndPlayByFrame(BEHIT, 0, 1);
        //if (GetComponent<AIBase>()) GetComponent<AIBase>().AIGetBeHit();
        beHitNum++;

       

    }





    //void BeHitFlyOut(float power)
    //{
    //    if (GlobalTools.FindObjByName("player")) {
    //        //playerRigidbody2D.AddForce(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
    //        GetZongTuili(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
    //    }
       
    //}

    public override void RunLeft(float _moveSpeedX = 0, bool isWalk = false)
    {
        //print("r "+isAtking);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (!isWalk && bodyScale.x == -1)
        {
            bodyScale.x = 1;
            //TurnLeft();
            this.transform.localScale = bodyScale;
            //playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x * 0.01f, playerRigidbody2D.velocity.y);
            AtkReSet();
        }

        if (isAtking) return;

        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        if (_moveSpeedX == 0) _moveSpeedX = -moveSpeedX;
        //playerRigidbody2D.AddForce(new Vector2(-_moveSpeedX, 0));
        playerRigidbody2D.velocity = new Vector2(-_moveSpeedX, playerRigidbody2D.velocity.y);
        //print("推动力LLL  "+_moveSpeedX + "    sudu " + playerRigidbody2D.velocity+"  scaleX  "+this.transform.localScale.x);
       
        Run();

    }

    public float moveSpeedX =4;
    public float moveSpeedY = 4;

    public override void RunRight(float _moveSpeedX = 0, bool isWalk = false)
    {
        //print("l " + isAtking);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (!isWalk && bodyScale.x == 1)
        {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
            //TurnRight();
            //playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x * 0.01f, playerRigidbody2D.velocity.y);
            AtkReSet();
        }

        if (isAtking) return;
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        if (_moveSpeedX == 0) _moveSpeedX = moveSpeedX;
        playerRigidbody2D.velocity = new Vector2(_moveSpeedX, playerRigidbody2D.velocity.y);
        //playerRigidbody2D.AddForce(new Vector2(_moveSpeedX, 0));
        //print("推动力right  " + _moveSpeedX+"  ? "+ moveSpeedX + "    sudu "+ playerRigidbody2D.velocity+"  朝向 "+ this.transform.localScale.x);


        //print("run right!!!!!!");

        Run();
    }
   

    public virtual void RunY(float moveSpeedY)
    {
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (isAtking) return;
        isRunYing = true;
        //resetAll();
        isAtkYc = false;
        //playerRigidbody2D.AddForce(new Vector2(playerRigidbody2D.velocity.x, moveSpeedY));
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, moveSpeedY);
        //playerRigidbody2D.AddForce(new Vector2(0, moveSpeedY));
        //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + moveSpeedY);
        //float spU = horizontalDirection > 0 ? 0.04f : -0.04f;
        //this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + spU);
        Run();
    }

    public virtual void GetStop()
    {
        playerRigidbody2D.velocity = Vector2.zero;
    }

    protected override void GetBeHit()
    {
        playerRigidbody2D.velocity *= 0.9f;
        if (CJYanmu) CJYanmu.Play();
        //playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x*0.9f, playerRigidbody2D.velocity.y * 0.9f);
        //print("-------------------------------------------------------->  "+ playerRigidbody2D.velocity);

        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            GetPause(0.7f);
            //print("wo bei hit in   in      *********************************IsBehitJingkong     " + IsBehitJingkong);
            if (IsGround) GetStand();
            //惊恐
            if (IsBehitJingkong) JingKong();
            //呆住
            if (IsDaiXie) DaiXie();
        }
        else
        {
            // print(" isBeHiting! 但是没有进入 behit 动作 "+DBBody.animationName);
            if (!DBBody.animation.HasAnimation(BEHITINAIR) && !DBBody.animation.HasAnimation(BEHIT))
            {
                roleDate.isBeHiting = false;
                GetPause(0.7f);
                //print("wo bei hit in   in      *********************************IsBehitJingkong     " + IsBehitJingkong);
                if (IsGround) GetStand();
                //惊恐
                if (IsBehitJingkong) JingKong();
                //呆住
                if (IsDaiXie) DaiXie();
                return;
            }
        }
    }



    [Header("是否在 被攻击后 惊恐")]
    public bool IsBehitJingkong = false;

    protected bool IsJingKonging = false;
    protected void JingKong()
    {
        //print("1111 IsJingKonging   "+ IsJingKonging);
        if (IsJingKonging) return;
        //print("22222222222 !!!!!!!!!!   " );
        IsJingKonging = true;
        //print("wo kao jinlaimei !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        if (!DBBody.animation.HasAnimation("jinghuang_1")) return;
        //print("********************************************************************************   惊慌 ");
        RUN = "jinghuang_1";
        STAND = "jinghuang_1";
        maxSpeedX += 10;
    }



    [Header("是否在 玩家die后 嘲笑")]
    public bool IsPlayerDieLaugh = false;
    protected bool IsLaughing = false;
    public void Laughing()
    {
        if (IsLaughing) return;
        IsLaughing = true;
        if (!DBBody.animation.HasAnimation("run_4")) return;
        RUN = "run_4";
        STAND = "run_4";
        maxSpeedX -= 4;
        if (maxSpeedX < 0) maxSpeedX = 1;
    }



    [Header("是否 有 呆住")]
    public bool IsDaiXie = false;
    protected bool IsDaiXieing = false;
    protected void DaiXie()
    {
        if (IsDaiXieing) return;
        IsDaiXieing = true;
        if (!DBBody.animation.HasAnimation("daixie_1")) return;
        RUN = "daixie_1";
        STAND = "daixie_1";
        maxSpeedX -= 4;
        if (maxSpeedX < 0) maxSpeedX = 1;
    }




    public override void GetAtk(string atkName = null)
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing || isAcing) return;
        //阻止了快落地攻击时候的bug
        //这里会导致AI回跳 进入落地动作而不能进入atk动作 所以回跳的跳起在动画里面做 不在程序里面给Y方向推力
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (!isAtk)
        {
            isAtk = true;
            isAtking = true;
            isTXShow = true;
            isAtkYc = true;
            yanchi = 0;
            jisuqi = 0;

            if (isInAiring)
            {
                atkZS = DataZS.jumpAtkZS;
            }
            else
            {
                atkZS = DataZS.atkZS;
            }

            if (atkName == null)
            {
                vOAtk.GetVO(atkZS[(int)atkNums]);
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
            }
            else
            {
                //GetPause(0.5f,0.5f);
                string _atkName = atkName;
                if (atkName.Split('|').Length != 1)
                {
                    _atkName = atkName.Split('|')[0];
                    float times = float.Parse(atkName.Split('|')[1].Split('-')[0]);
                    float scales = 0.5f;
                    if (atkName.Split('|')[1].Split('-').Length != 1) scales = float.Parse(atkName.Split('|')[1].Split('-')[1]);
                    GetPause(times, scales);

                }
                vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(_atkName, DataZS.GetInstance()));
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);

                if (vOAtk.qishouYC!=0)
                {
                    GetPause(vOAtk.qishouYC,0.01f);
                }
            }

            MoveVX(vOAtk.xF, true);
        }

    }

}
