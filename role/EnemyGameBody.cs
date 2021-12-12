using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;



public class EnemyGameBody : GameBody {

    // Use this for initialization
    //void Start () {
    //       GetStart();
    //   }
    [Header("默认的 移动动作")]
    public string RunACName = "run_1";

    protected override void FirstStart()
    {
        RUN = RunACName;
        if (!GetDB().animation.HasAnimation(RUN)) RUN = "run_3";
        //GetBoneColorChange(Color.white);
        //print("-------------------->   "+RUN);
    }

    // Update is called once per frame


    

    void Update () {
        if (isFighting)
        {
            OutFighting();
        }
        this.GetUpdate();
    }

    bool isFighting = false;
    //切换站立姿势 切换跑动姿势
    void ChangeStandAndRunAC()
    {
        STAND = "stand_1";
        CheckIsHasAC();
    }

    void ChangeACNum(int nums)
    {
        if (nums == 1)
        {
            STAND = "stand_3";
            RUN = "run_3";
        }
        else if (nums == 2)
        {
            STAND = "stand_2";
            RUN = "run_1";
        }
        else if (nums == 3)
        {
            STAND = "stand_2";
            RUN = "run_2";
        }
        else if (nums == 4)
        {
            STAND = "stand_1";
            RUN = "run_3";
        }
        CheckIsHasAC();
    }

    public bool IsUseRun1 = false;
    /// <summary>
    /// 检测是否包含 站立和跑动动作 不包含就还愿初始动作
    /// </summary>
    void CheckIsHasAC()
    {
        if (!DBBody.animation.HasAnimation(STAND)) STAND = "stand_1";
        if (!DBBody.animation.HasAnimation(RUN)) {
            if (!IsUseRun1) {
                RUN = "run_3";
            }
            else
            {
                RUN = "run_1";
            }
        }
        
    }

    //void ChangeRunAC()
    //{
    //    int nums = Random.Range(0, 3);
    //    nums += 1;
    //    RUN = "run_" + nums;
    //    if (!DBBody.animation.HasAnimation(RUN)) RUN = "run_3";
    //}

    int inFightNums = 0;
    //脱离战斗状态
    void OutFighting()
    {
        inFightNums++;

        if (inFightNums >= 1000)
        {
            //print(inFightNums);
            ChangeACNum(4);
            DBBody.animation.GotoAndPlayByFrame(STAND);
            isFighting = false;
        }
    }


    override public void InFightAtk()
    {
        //print("hellooooooooooooooooooooooooooooooooooo!!");
        inFightNums = 0;
        isFighting = true;
        //print("atkNums   " + atkNums);
        ChangeStandAndRunAC();
        //if (atkNums == 2)
        //{
        //    //ChangeACNum(3);
        //}else if (atkNums == 3)
        //{
        //    //ChangeACNum(2);
        //}
    }

    public override void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
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
        //isYanchi = false;
        isSkilling = false;
        isSkillOut = false;
        isDodge = false;
        isDodgeing = false;
        isTXShow = false;
        if (roleDate) roleDate.isBeHiting = false;
        if (playerRigidbody2D != null && playerRigidbody2D.gravityScale != gravityScaleNums) playerRigidbody2D.gravityScale = gravityScaleNums;


        isAcing2 = false;
        _acName2 = "";
        _isHasAtkTX = false;
    }

    override public void HasBeHit(float chongjili = 0, bool IsOther = true)
    {
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;
        inFightNums = 0;
        //切换进入战斗
        InFightAtk();
        ChangeStandAndRunAC();
        //print("speedX   "+ speedX);
        //print(this.name+"  22--->  "+ playerRigidbody2D.velocity.x+"     冲击力是多少 "+chongjili);
        if (chongjili > 700)
        {
            BEHIT = "beHit_3";
            ChangeACNum(2);
        }
        else
        {
            //判断是哪种被击中 改变被击中的动作
            //判断是否包含动作 
            float rnum = Random.Range(0, 2);
            BEHIT = rnum >= 1 ? "beHit_1" : "beHit_2";
        }
        if (!DBBody.animation.HasAnimation(BEHITINAIR)) BEHIT = "beHit_1";

        //print(speedX);
        //if (GetComponent<AIBase>()) GetComponent<AIBase>().AIGetBeHit();

        if (isInAiring)
        {
            if (DBBody.animation.HasAnimation(BEHITINAIR))
            {
                print("behit in air!!!!!!");
                DBBody.animation.GotoAndPlayByFrame(BEHITINAIR, 0, 1);
                return;
            }
        }
        DBBody.animation.GotoAndPlayByFrame(BEHIT, 0, 1);

        if (roleAudio)
        {
            string BeHitStr = roleAudio.AUDIOBEHIT_1;
            if (roleAudio.BeHit_2 != null)
            {
                if(GlobalTools.GetRandomNum()>50) BeHitStr = roleAudio.AUDIOBEHIT_2;
            }
            roleAudio.PlayAudioYS(BeHitStr);
        }

        print("behit!!!!!!");
        beHitNum++;
    }

    override protected void GetBeHit()
    {
        if ((DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted)
        {
            roleDate.isBeHiting = false;
            if (IsGround) GetStand();
        }
    }


    bool isAcing2 = false;
    //抑制导光特效的 攻击东旭哦事件 是怎么配套特效的？
    public bool _isHasAtkTX = false;
    string _acName2 = "";
    public void GetACByName(string acName,bool isHasAtkTX = false)
    {
        if (!isAcing2)
        {
            isAcing2 = true;
            if (!IsHasAC(acName))
            {
                isAcing2 = false;
                _isHasAtkTX = false;
                _acName2 = "";
                return;
            }

            _acName2 = acName;
            _isHasAtkTX = isHasAtkTX;
            if (DBBody.animation.HasAnimation(_acName2) && DBBody.animation.lastAnimationName != acName)
            {
                DBBody.animation.GotoAndPlayByFrame(acName, 0, 1);
            }
        }
    }

    public bool IsHasAC(string acName)
    {
        return DBBody.animation.HasAnimation(acName);
    }

    void ACing()
    {
        if (DBBody.animation.lastAnimationName == _acName2&& DBBody.animation.isCompleted)
        {
            isAcing2 = false;
            _isHasAtkTX = false;
            _acName2 = "";
        }
    }



    protected override void ShowACTX(string type, EventObject eventObject)
    {
        //print("type:  "+type);
        if (IsSFSkill) return;
        if (type == EventObject.SOUND_EVENT)
        {
            print("sound  ????? eventName:  "+eventObject.name);
            //print(playerRigidbody2D.velocity.x+"  ?    "+this.transform.localScale);
            if (eventObject.name == "jumpHitWall")
            {
                if ((this.transform.localScale.x == 1 && playerRigidbody2D.velocity.x >= 1) || (this.transform.localScale.x == -1 && playerRigidbody2D.velocity.x <= -1)) return;
            }
            if (eventObject.name == "runG1") return;
            if (eventObject.name == "downOnGround" && DBBody.animation.lastAnimationName != DOWNONGROUND) return;
            //if(eventObject.name=="run1"|| eventObject.name == "run2") _yanmu.Play();
            GetComponent<RoleAudio>().PlayAudio(eventObject.name);
        }


        if (type == EventObject.FRAME_EVENT)
        {
            if (eventObject.name == "jn_begin")
            {
                //释放准备动作的特效
                if (isSkilling)
                {
                    GetComponent<ShowOutSkill>().ShowOutSkillBeginEffectByName(vOAtk.skillBeginEffect);
                    isSkilling = false;
                }
            }


            if (eventObject.name == "ac")
            {
                if (_isHasAtkTX) return;
                if (isAcing)
                {
                    if (isSkillOut)
                    {
                        GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName, true);
                        isSkillOut = false;
                    }
                    else
                    {
                        //技能释放点
                        print("  jn "+jn);
                        if (jn == null) return;
                        GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName, true);
                        isTXShow = false;
                    }

                }
                else
                {
                    //print(">>>普通攻击的特效名字        "+vOAtk.txName);
                    //GetPause(0.1f);
                    //if (vOAtk.AudioName != "")
                    //{
                    //    print("  >>>>攻击音效名字  "+ vOAtk.AudioName);
                    //    roleAudio.PlayAudio(vOAtk.AudioName);
                    //}
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName);
                    isTXShow = false;
                }

                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_002");
                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_fk");
            }
        }

    }


    protected override void GetUpdate()
    {
        //print(this.name+ "  isDowning:"+isDowning+ "  roleDate.isBeHiting:"+ roleDate.isBeHiting+ "  isAcing:"+ isAcing+ "  isDodgeing:"+ isDodgeing+"   acName:"+ DBBody.animation.lastAnimationName+" isInAiring:"+isInAiring+" isJumping:"+isJumping);

        //if (!DBBody) return;

        CurrentAcName = DBBody.animation.lastAnimationName;
        //print("CurrentAcName    "+ CurrentAcName);
        //if (Globals.isPlayerDie)
        //{
        //    print("-----------------------------stand!     "+STAND);
        //    if (CurrentAcName != STAND)
        //    {
        //        ResetAll();
                
        //        //DBBody.animation.GotoAndStopByFrame(STAND);
        //        GetAcMsg(STAND);
        //        //Time.timeScale
        //    }
        //    //GetStand();
        //    return;
        //}

        //脚下的烟幕
        Yanmu();

        GuDingTuiLiHufu();

        if (roleDate.isDie)
        {
            DBBody.animation.timeScale = 1;
            GetDie();
            return;
        }

        if (roleDate.live <= 0)
        {
            roleDate.isDie = true;
        }




        if (Globals.isInPlot) return;


        if (roleDate.isBeHiting)
        {
            //print(this.name + "  ***********************************************  " + GetPlayerRigidbody2D().velocity+"   是否固定 推力中  "+IsGuDingTuili);
            ControlSpeed(30);
            GetBeHit();
            return;
        }



        if (_theTimer != null && !_theTimer.IsPauseTimeOver())
        {
            return;
        }
        else
        {
            DBBody.animation.timeScale = 1;
        }




        if (isQianhuaing)
        {
            //print("isQianhuaing");
            //防止加速过快 限制最大速度
            ControlSpeed(20);
            GetQianhuaing();
            return;
        }


        if (isBackUping)
        {
            //print("isBackUping");
            ControlSpeed(14);
            GetBackUping();
            return;
        }


        //print(_theTimer);


        if (isDodgeing)
        {
            //print("isDodgeing");
            Dodge1();
            return;
        }


        if (!IsJiasu && !isAtking&&!isAcing) ControlSpeed();





        InAir();
        IsCanShanjinAndJump();


        if (isAcing2)
        {
            ACing();
            return;
        }


        if (isAcing)
        {
            //print("isAcing");
            GetAcMsg(_acName);
            return;
        }



        /* if (isJumping)
         {
             Jump();
         }*/

        if (isAtking)
        {
            //print("isAtking");
            Atk();
        }


        //print("isQianhuaing>  " + isQianhuaing+ "/n isDowning "+ isDowning+ " /n isJumping2  "+ isJumping2+ " /n  isJumping  "+ isJumping+"  ");



        //print(this.tag);
        //if (this.tag != "diren") print("hi");
        InStand();
    }



}
