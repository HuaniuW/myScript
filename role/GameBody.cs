using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
using System;

public class GameBody : MonoBehaviour, IRole {
    //增加到十个特效 或者更多 看需求

    [Header("水平速度")]
    public float speedX;

    //水平方向的按键响应 Input.GetAxis
    const string HORIZONTAL = "Horizontal";

    [Header("水平推力")]
    [Range(0, 550)]//数值滑杆 限定最大最小值
    public float xForce;

    //目前垂直速度
    float speedY;

    [Header("水平最大速度")]
    public float maxSpeedX;

    [Header("Y最大速度")]
    public float MaxSpeedY =30;

    protected Vector2 newSpeed;

    [Header("垂直向上的推力")]
    public float yForce;

    [Header("感应地板的距离")]
    [Range(0, 1)]
    public float distance;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck3;


    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float distanceMQ = 0.13f;

    [Header("侦测面前墙的射线起点")]
    public UnityEngine.Transform qianmian;


    [Header("当前动作名字")]
    public string CurrentAcName;

    [Header("地面图层 包括机关")]
    public LayerMask groundLayer;

    [Header("地面图层 不包括机关")]
    public LayerMask groundLayer2;

    [Header("是否着地")]
    public bool grounded;

    [Header("是否碰到面前的墙")]
    public bool hidWalled;

    [Header("停下后X方向的剩余滑动速度")]
    public float slideNum = 3;

    [Header("反弹跳X方向的力")]
    public float wallJumpXNum = 700;

    protected Vector3 newPosition;

    [Header("是否die回收")]
    public bool isDieRemove = true;

    [Header("子弹的 发射点")]
    public UnityEngine.Transform zidanPos;

    [Header("是否需要 碰撞点 不需要的话直接是中点")]
    public bool IsNeedHitPos = true;


    protected float bodyHitTimesRecord = 0;
    //身体 保护持续时间
    protected float bodyHitProtectTimes = 2;

    public void StartBodyHitProtect()
    {
        bodyHitTimesRecord = 0;
        roleDate.isBodyCanBeHit = false;
    }

   


    //[Header("被 击中的 叫声   1")]
    //public AudioSource BeHitSound_1;

    //[Header("被 击中的 叫声   2")]
    //public AudioSource BeHitSound_2;


    protected Rigidbody2D playerRigidbody2D;
    public Rigidbody2D GetPlayerRigidbody2D()
    {
        if (!playerRigidbody2D) playerRigidbody2D = GetComponent<Rigidbody2D>();
        return playerRigidbody2D;
    }

    protected UnityArmatureComponent DBBody;
    public UnityArmatureComponent GetDB()
    {
        if (!DBBody) DBBody = GetComponentInChildren<UnityArmatureComponent>();
        return DBBody;
    }

    public bool IsGetStand()
    {
        return GetDB().animation.lastAnimationName == STAND;
    }


    public bool IsDownOnGround()
    {
        return GetDB().animation.lastAnimationName == DOWNONGROUND;        
    }

    protected Vector3 bodyScale;
    public Vector3 GetBodyScale()
    {
        return bodyScale;
    }

    public UnityEngine.Transform TalkPos;
    internal Vector2 GetTalkPos()
    {
        return TalkPos.position;
    }

    protected bool isRunLefting = false;
    public bool isRunYing = false;

    protected bool isRunRighting = false;

    public bool isInAiring = false;
    public bool isDowning = false;

    public bool isJumping = false;
    //起跳
    protected bool isQiTiao = false;

    public bool isTXShow = false;

    public bool IsJiasu = false;

    public virtual void ResetAll()
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
        if(playerRigidbody2D!=null && playerRigidbody2D.gravityScale!= _recordGravity) playerRigidbody2D.gravityScale = _recordGravity;

        acingTime = 0;

    }

    protected RoleDate roleDate;

    public float gravityScaleNums = 4.5f;
    protected float _recordGravity = 4.5f;
    protected string RUN = "run_3";
    protected string STAND = "stand_1";
    protected const string RUNBEGIN = "runBegin_1";
    protected const string RUNSTOP = "runStop_1";
    protected const string JUMPUP = "jumpUp_1";
    protected const string JUMPDOWN = "jumpDown_1";
    protected const string JUMPHITWALL = "jumpHitWall_1";
    protected string DOWNONGROUND = "downOnGround_1";
    protected const string JUMP2DUAN = "jump2Duan_1";
    protected const string ATK = "atk_";
    protected string DODGE1 = "dodge_1";
    protected string DODGE2 = "dodge_2";
    protected string BEHIT = "beHit_1";
    public string DIE = "die_1";
    //前滑动
    protected string QIANHUA = "qianhua_1";

    //回跳跃动作
    protected string BACKUP = "houshan_1";
    protected string WALK = "walk_1";
    protected const string BEHITINAIR = "beHitInAir_1";

    protected bool isBackUp = false;
    protected bool isBackUping = false;



    public string GetJumpUpACName()
    {
        return JUMPUP;
    }

    public string GetDownOnGroundACName()
    {
        return DOWNONGROUND;
    }


    public string GetJumpDownACName()
    {
        return JUMPDOWN;
    }


    public void GetBackUp(float vx = 15)
    {
        if (roleDate.isBeHiting) return;
        if (!DBBody.animation.HasAnimation(BACKUP)) return;
        if (!isBackUp && IsGround && !isBackUping)
        {
            ResetAll();
            isBackUp = true;
            if (DBBody.animation.lastAnimationName != BACKUP) DBBody.animation.GotoAndPlayByFrame(BACKUP, 0, 1);

            isBackUping = true;
            //roleDate.isCanBeHit = false;
            //newSpeed.x = 0;
            //newSpeed.y = 0;
            //playerRigidbody2D.velocity = newSpeed;
            //print("huitiao");
            playerRigidbody2D.velocity = Vector2.zero;
            BackJumpVX(vx);
            //打开有bug
            //MoveVY(200);
            print("进入 后闪动作！！！！！！");
        }

    }


    protected void GetBackUping()
    {
        print("  backUping!!!!! ");
        if (isBackUping)
        {
            print("时间 xiuzheng");
            buckUpXZTimes += Time.deltaTime;
            if (buckUpXZTimes >= 1)
            {
                BackUpOver();
            }
        }


        if (DBBody.animation.lastAnimationName == BACKUP && DBBody.animation.isCompleted)
        {
            BackUpOver();
        }
    }

    float buckUpXZTimes = 0;

    void BackUpOver()
    {
        isBackUping = false;
        isBackUp = false;
        roleDate.isCanBeHit = true;
        buckUpXZTimes = 0;
    }

   

    public bool GetBackUpOver()
    {
        return !isBackUping;
    }

    protected bool isQianhua = false;
    protected bool isQianhuaing = false;
    public void Qianhua(float vx = 20)
    {
        if (roleDate.isBeHiting) return;
        if (!DBBody.animation.HasAnimation(QIANHUA)) return;
        if (!isQianhua && IsGround && !isQianhuaing)
        {
            ResetAll();
            isQianhua = true;
            if (DBBody.animation.lastAnimationName != QIANHUA) DBBody.animation.GotoAndPlayByFrame(QIANHUA, 0, 1);
            isQianhuaing = true;
            roleDate.isCanBeHit = false;
            newSpeed.x = 0;
            newSpeed.y = 0;
            playerRigidbody2D.velocity = newSpeed;
            print("********************************************************前滑！！！ huitiao");
            BackJumpVX(200);
            MoveVX(vx,true);
            //打开有bug
            //MoveVY(200);
        }
    }


    protected void GetQianhuaing()
    {
        if (DBBody.animation.lastAnimationName == QIANHUA && DBBody.animation.isCompleted)
        {
            isQianhuaing = false;
            isQianhua = false;
            roleDate.isCanBeHit = true;
        }
    }

    public bool GetQianhuaOver()
    {
        return !isQianhuaing;
    }


    public bool IsHanAC(string ACName)
    {
        return DBBody.animation.HasAnimation(ACName);
    }

    public void GetSkill1()
    {
        if (GlobalTools.FindObjByName("PlayerUI").name=="PlayerUI")
        {
            //ShowSkill("PlayerUI/skill1");
        }
        else
        {
            //ShowSkill("PlayerUI(Clone)/skill1");
        }
    }

    public void GetSkill2()
    {
        if (GlobalTools.FindObjByName("PlayerUI").name == "PlayerUI")
        {
            //ShowSkill("PlayerUI/skill2");
        }
        else
        {
            //ShowSkill("PlayerUI(Clone)/skill2");
        }
    }

    //被动技能
    protected HZDate bdjn;
    //释放被动技能    补一个被动技能的释放  是否有被动技能 有的话 直接释放  被动技能是否 配有动作？
    public virtual void ShowPassiveSkill(GameObject hzObj) {
        bdjn = hzObj.GetComponent<UI_Skill>().GetHZDate();
        if (roleDate.lan - bdjn.xyLan < 0) return;
        if (roleDate.live - bdjn.xyXue < 1) return;
        if (!hzObj.GetComponent<UI_Skill>().isCanBeUseSkill()) return;
        roleDate.lan -= bdjn.xyLan;
        roleDate.live -= bdjn.xyXue;

        //徽章被动技能 发动  都给在 同时发生  有动作直接播放动作的同时 显示节能特效

        if (bdjn.skillACName != null && DBBody.animation.HasAnimation(bdjn.skillACName))
        {
            //***找到起始特效点 找骨骼动画的点 或者其他办法
            if (isInAiring)
            {
                GetAcMsg(bdjn.skillACNameInAir);
            }
            else
            {
                GetAcMsg(bdjn.skillACName);
            }
            


            print("技能释放动作//////////////////////////////////////////////////////    " + bdjn.skillACName);
            playerRigidbody2D.velocity = Vector2.zero;
            if (bdjn.ACyanchi > 0)
            {
                GetPause(bdjn.ACyanchi);
                //***人物闪过去的 动作 +移动速度  还有多发的火球类的特效
            }
            
        }
        else
        {
            print("被动技能释放、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、、！！！！！！");
            //测试用 正式的要配动作
            
        }
        GetComponent<ShowOutSkill>().ShowOutSkillByName(bdjn.TXName, true);
    }

    protected HZDate jn;
    public void ShowSkill(GameObject hzObj)
    {
        
        if (roleDate.isBeHiting || roleDate.isDie || isDodgeing || isAtking || isBackUping ||isAcing) return;
        //print(" >>>   释放技能");
        jn = hzObj.GetComponent<UI_Skill>().GetHZDate();// GlobalTools.FindObjByName(urlName).GetComponent<SkillBox>().GetSkillHZDate();


        if (jn.skillACName != "" && IsHitWall) return;


        if (jn.type == "bd") return;

      
        if (jn != null)
        {
            print(" jnCD?     "+ jn.IsCDOver());
            if (jn.IsCDOver())
            {
                if (roleDate.lan - jn.xyLan < 0) {
                    //蓝不够释放技能
                    ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.NO_HUN_PROMPT, null), this);
                    roleAudio.PlayAudioYS("Alert_1");
                    return;
                }

                if (roleDate.live - jn.xyXue < 1) {
                    //释放技能的血量不够
                    roleAudio.PlayAudioYS("Alert_1");
                    return;
                }
                print("hi! 释放技能");

                //在空中的话 没有主动技能释放动作  也返回   没有动作 无法释放  相当于控制无法使用该技能
                if (isInAiring && jn.type != "bd")
                {
                    if (jn.skillACName != "" && jn.skillACNameInAir == "")
                    {
                        print("在地面有 释放 动作 但是 在空中没有释放动作  在空中 无法释放");
                        return;
                    }

                }

                if (!hzObj.GetComponent<UI_Skill>().isCanBeUseSkill()) return;

                roleDate.lan -= jn.xyLan;
                roleDate.live -= jn.xyXue;
                //print("   硬直  "+jn.TempAddYingZhi);
                if(jn.TempAddYingZhi != 0)
                {
                    GetComponent<TempAddValues>().TempAddYZ(jn.TempAddYingZhi, jn.TempAddYingZhiTimes);
                    //print("------------------------> 提高硬直！！！！！！ ");
                }

                //临时提高硬直
                if (jn.TempShanghaiJianmianBili!=0)
                {
                    GetComponent<TempAddValues>().TempJianShangBL(jn.TempShanghaiJianmianBili,jn.tempJSTimes);
                }


                //jn.StartCD();
                if(Globals.isDebug)print("---------------------------> 释放技能！！"+ jn.skillACName);
                //this.GetComponent<GetHitKuai>().GetKuai("jn_yueguang","1");
                //是否包含 技能动作
                if (jn.skillACName != null && DBBody.animation.HasAnimation(jn.skillACName))
                {
                    //***找到起始特效点 找骨骼动画的点 或者其他办法

                    yanchiTime = jn.ACOveryanchi;
                    if (isInAiring)
                    {
                        //print("jn.name    "+ jn.name);
                        if(jn.name == "jn_feidao")
                        {
                            GetAcMsg(jn.skillACName);
                        }
                        else
                        {
                            GetAcMsg(jn.skillACNameInAir);
                            //临时悬停
                            GetComponent<TempAddValues>().TempXuanKong(jn.AirXTTimes);
                        }
                        
                    }
                    else
                    {
                        GetAcMsg(jn.skillACName);
                    }
                    
                    print("-----------------------------------------------------技能释放动作    "+jn.skillACName);

                    if(jn.AudioName!="")roleAudio.PlayAudio(jn.AudioName);

                    playerRigidbody2D.velocity = Vector2.zero;
                    if (jn.ACyanchi > 0)
                    {
                        GetPause(jn.ACyanchi,0);
                        //***人物闪过去的 动作 +移动速度  还有多发的火球类的特效
                    }
                }
                else {
                    //测试用 正式的要配动作
                    print("????????????????????????????????????????????????????????????    "+jn.TXName);
                    
                    if(jn.TXName == "TX_zidanDun"|| jn.TXName == "TX_shengmingye")
                    {
                        //生成一个 放到 主角身体里面
                        GameObject TX_zidanduan = ObjectPools.GetInstance().SwpanObject2(Resources.Load(jn.TXName) as GameObject);     //GlobalTools.GetGameObjectByName(jn.TXName);
                        //TX_zidanduan = Instantiate(TX_zidanduan);
                        print(TX_zidanduan.transform.position + "   玩家位置  "+ this.gameObject.transform.position);
                        TX_zidanduan.transform.position = this.gameObject.transform.position;
                        TX_zidanduan.transform.parent = this.gameObject.transform;
                        //print("特效name    "+ TX_zidanduan.name+"   weizhi  " + TX_zidanduan.transform.position);
                        //TX_zidanduan.transform.position = Vector3.zero;
                        //print("特效位置222222    " + TX_zidanduan.transform.position);
                        //TX_zidanduan.transform.position = new Vector2(0, 0);

                        if (jn.TXName == "TX_shengmingye")
                        {
                            roleDate.live += 400;
                        }
                    }
                    else
                    {
                        GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName, true);
                    }

                }


                //GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName,true);
            }
            else
            {
                if (Globals.isDebug) print("技能CD中。。。。。。");
                //***播放 声音
            }

        }
        else
        {
            if (Globals.isDebug) print("没有装配技能");
            //***播放声音
        }
    }

 



    protected bool isDodge = false;
    protected bool isDodgeing = false;
    protected bool isCanShanjin = true;

    public void GetDodge1()
    {

        if (isInAiring && isDodgeing && DBBody.animation.lastAnimationName == STAND)
        {
            playerRigidbody2D.gravityScale = gravityScaleNums;
        }

        if (roleDate.isBeHiting||isAcing||isDodgeing) return;
        

        if (DBBody.animation.lastAnimationName == DOWNONGROUND || DBBody.animation.lastAnimationName == JUMPUP|| DBBody.animation.lastAnimationName == JUMPHITWALL) return;
        if(!thePlayerUI) thePlayerUI = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>();
        if (!thePlayerUI.ui_shanbi.GetComponent<UI_Skill>().isCanBeUseSkill()) return;
        //if (!GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>().ui_shanbi.GetComponent<UI_Skill>().isCanBeUseSkill()) return;
        float testSpeed;
        if (isInAiring)
        {
            if (!isCanShanjin) return;
            isCanShanjin = false;
            DODGE2 = "shanjin_1";
            testSpeed = 30;
        }
        else
        {
            DODGE2 = "dodge_1";
            //DODGE2 = "shanjin_2";
            testSpeed = 16;
        }

        if (!isDodge)
        {
            ResetAll();
            isDodge = true;
            isDodgeing = true;
            //roleDate.isCanBeHit = false;
            //print("-->x  " + playerRigidbody2D.velocity.x);
            if (playerRigidbody2D.velocity.x >= 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(testSpeed, true);
                    //print("  闪进？？？？？ ");
                }
            }
            else if (playerRigidbody2D.velocity.x < 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(testSpeed, true);
                }
            }

            ShanjinTX();
        }
    }

    //闪进特效
    protected virtual void ShanjinTX()
    {

    }

    protected void Dodge1()
    {
        //ShanjinTX();
        if (isDodgeing && IsHitMQWall && isInAiring)
        {
            //碰到墙
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL, 0, 1);
            //isJump2 = false;
            isJumping2 = false;
            DodgeOver();
            return;
        }
        
        if ((DBBody.animation.lastAnimationName == DODGE1 || DBBody.animation.lastAnimationName == DODGE2) && DBBody.animation.isCompleted)
        {
            DodgeOver();
        }

        if (isInAiring&& isDodgeing)
        {
            //print("是否是连续的！！！");

            playerRigidbody2D.gravityScale = 0;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, -0.2f);

            if (!isShanjin)
            {
                isShanjin = true;
                shanjinjuli = this.transform.position.x;
            }
            if (Mathf.Abs(this.transform.position.x - shanjinjuli) >= 10)
            {
                DodgeOver();
                shanjinjuli = 0;
            }
        }
        else
        {
            playerRigidbody2D.gravityScale = gravityScaleNums;
        }
    }

    float shanjinjuli = 0;
    protected bool isShanjin = false;
    protected bool isCanJump = false;
    
    protected void DodgeOver()
    {
        if (isInAiring)
        {
            if (DBBody.animation.lastAnimationName != JUMPDOWN) DBBody.animation.GotoAndPlayByFrame(JUMPDOWN);
        }
        isDodge = false;
        isDodgeing = false;
        roleDate.isCanBeHit = true;
        isDowning = false;
        playerRigidbody2D.gravityScale = gravityScaleNums;
        shanjinjuli = 0;
        isShanjin = false;
        //print(">>>>>>进来没！！");
        ShanjinOverTX();
    }

    protected virtual void ShanjinOverTX()
    {

    }


    [Header("跳跃探测 点 1")]
    public UnityEngine.Transform JumpCheck1;

    public virtual bool IsCanJump1()
    {
        if (JumpCheck1 == null) return false;
        if (isInAiring) return false;
        Vector2 start = JumpCheck1.position;
        float __x = this.transform.localScale.x > 0 ? start.x - 1 : start.x + 1;
        Vector2 end = new Vector2(start.x, start.y - 10f);
        Vector2 end2 = new Vector2(__x, start.y);
        Debug.DrawLine(start, end, Color.white);
        bool isHitGroundDown = Physics2D.Linecast(start, end, groundLayer2);
        bool isHitGroundQian = Physics2D.Linecast(start, end2, groundLayer2);
        if (isHitGroundDown && !isHitGroundQian) return true;
        return false;
    }



    [Header("跳跃探测 点2 检测高墙用 ")]
    public UnityEngine.Transform JumpCheck2;

    public virtual bool IsCanJump2()
    {
        if (JumpCheck2 == null) return false;
        if (isInAiring) return false;
        Vector2 start = JumpCheck2.position;
        float __x = this.transform.localScale.x > 0 ? start.x - 1 : start.x + 1;
        Vector2 end = new Vector2(start.x, start.y - 0.2f);
        Vector2 end2 = new Vector2(__x, start.y);
        Debug.DrawLine(start, end, Color.red);
        bool isHitGroundDown = Physics2D.Linecast(start, end, groundLayer2);
        bool isHitGroundQian = Physics2D.Linecast(start, end2, groundLayer2);
        print("是否 碰到 下面    "+ isHitGroundDown);
        print("是否 碰到 *****前面    " + isHitGroundQian);


        if (!isHitGroundDown && !isHitGroundQian) return true;
        return false;
    }


    //检测是否是 向下 移动的 地板
    public virtual bool IsCanMoveDown()
    {
        if (qianmianjiance == null) return false;
        if (isInAiring) return false;
        Vector2 start = qianmianjiance.position;
        Vector2 end = new Vector2(start.x, start.y - 10f);
        Debug.DrawLine(start, end, Color.red);
        bool isHitGroundDown = Physics2D.Linecast(start, end, groundLayer2);
        return isHitGroundDown;
    }





    [Header("侦测地板的射线起点22222")]
    public UnityEngine.Transform qianmianjiance;
    //路面是否尽头
    bool isEndGround = false;
    public virtual bool IsEndGround
    {
        get
        {
            if (qianmianjiance == null) return false;
            if (isInAiring) return false;
            Vector2 start = qianmianjiance.position;
            Vector2 end = new Vector2(start.x, start.y - 0.7f);
            Debug.DrawLine(start, end, Color.red);
            isEndGround = Physics2D.Linecast(start, end, groundLayer);
            return !isEndGround;
        }
    }

    bool isHitWall = false;
    public bool IsHitWall
    {
        get
        {
            if (qianmianjiance == null) return false;
            Vector2 start = qianmianjiance.position;
            float __x = this.transform.localScale.x > 0 ? start.x - 1 : start.x + 1;
            Vector2 end = new Vector2(__x, start.y );
            Debug.DrawLine(start, end, Color.yellow);
            isHitWall = Physics2D.Linecast(start, end, groundLayer);
            //print("前面 检测点 是否 探测到撞墙 "+ isHitWall);
            if(isHitWall) isCanShanjin = true;
            return isHitWall;
        }
    }


    //在玩家底部是一条短射线 碰到地板说明落到地面 
    public virtual bool IsGround
    {
        get
        {
            //print("groundCheck 是否有这个 变量   "+ groundCheck);
            if (!groundCheck) return false;
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }


    protected bool IsHitMQWall
    {
        get
        {
            Vector2 start = qianmian.position;
            Vector2 end = new Vector2(start.x - distanceMQ * bodyScale.x, start.y);
            Debug.DrawLine(start, end, Color.red);
            hidWalled = Physics2D.Linecast(start, end, groundLayer);
            return hidWalled;
        }
    }



    public void ControlSpeed(float vx = 0)
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        //钳制 speedX 被限制在 -maxSpeedX  maxSpeedX 之间
        float newSpeedX;
        if (vx == 0) {
            newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        }
        else
        {
            newSpeedX = Mathf.Clamp(speedX, -vx, vx);
        }
        
        //if (horizontalDirection == 0) newSpeedX/=10;
        newSpeed.x = newSpeedX;
        newSpeed.y = speedY;
        //获取向量速度
        playerRigidbody2D.velocity = newSpeed;
    }

    /// <summary>
    /// 临时改变跑步动作和速度
    /// </summary>
    /// <param name="runName">跑步动作</param>
    /// <param name="changeMaxSpeedX">跑步速度</param>
    public virtual void RunACChange(string runName,float changeMaxSpeedX = 0)
    {
        RUN = runName;
        if (changeMaxSpeedX != 0) {
            maxSpeedX = changeMaxSpeedX;
        }
        
    }


    public string GetRunName()
    {
        print(" 我的默认 跑动 动作 "+RUN);
        return RUN;
    }

    public virtual void RunLeft(float horizontalDirection, bool isWalk = false)
    {
        if (DBBody.animation.lastAnimationName == STAND) DodgeOver();
        //print("r "+isAtking);
        //print("-------------->  isAcing   " + isAcing);
        isBackUping = false;
        
        if (roleDate.isBeHiting) return;
        if (isInAiring && (!isJumping || !isJumping2)) return;

        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;

        if (isTXShow) return;

        if (!isWalk && bodyScale.x == -1)
        {
            bodyScale.x = 1;
            this.transform.localScale = bodyScale;
            AtkReSet();
        }

        if (isAtking) return;
        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        //playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        GetZongTuili(new Vector2(xForce * horizontalDirection, 0));
        //print("hihihi");
        Run();

    }

    public void TurnDirection(GameObject _obj)
    {
        if (!_obj) return;
        if (this.transform.position.x < _obj.transform.position.x)
        {
            TurnRight();
        }
        else
        {
            TurnLeft();
        }
    }

    public virtual void TurnRight()
    {
        if (GetComponent<RoleDate>().isDie) return;
        this.transform.localScale = new Vector3(-1, 1,1);
       
    }

    public virtual void TurnLeft()
    {
        if (GetComponent<RoleDate>().isDie) return;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    public virtual void RunRight(float horizontalDirection, bool isWalk = false)
    {
        if (DBBody.animation.lastAnimationName == STAND) DodgeOver();
        //print("l " + isAtking);
        //print(" --------------------------- ???? isAcing  "+ isAcing);
        isBackUping = false;
        if (roleDate.isBeHiting) return;
        if (isInAiring && (!isJumping || !isJumping2)) return;
        if (isAcing) return;
        if (isDodgeing) return;
        if (!DBBody.animation.HasAnimation(WALK)) isWalk = false;
        if (isTXShow) return;
        if (!isWalk && bodyScale.x == 1)
        {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
            AtkReSet();
        }

        if (isAtking) return;
        
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        //playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        GetZongTuili(new Vector2(xForce * horizontalDirection, 0));
        //print("right "+ horizontalDirection + "  xForce "+xForce);
        Run();
    }

    public bool IsGuDingTuili = false;
    public virtual void GetTuili(float tuili, float times = 0, bool isGuDingTuili = false)
    {
        //playerRigidbody2D.velocity = Vector2.zero;
        //playerRigidbody2D.AddForce(new Vector2(tuili, 0));
        IsGuDingTuili = isGuDingTuili;
        if (isGuDingTuili) {
            _gudingTuiliTimeNums = 0;
            _gudingTuiliTime = times;
        } 
        GetZongTuili(new Vector2(tuili, 0),true);
    }


    protected float _gudingTuiliTime = 0;
    protected float _gudingTuiliTimeNums = 0;
    //固定推力 恢复
    protected void GuDingTuiLiHufu()
    {
        if (!IsGuDingTuili) return;
        _gudingTuiliTimeNums += Time.deltaTime;
        if(_gudingTuiliTimeNums>= _gudingTuiliTime)
        {
            _gudingTuiliTimeNums = 0;
            IsGuDingTuili = false;
        }
    }



    public virtual void GetZongTuili(Vector2 v2,bool IsSetZero = false)
    {
        //print(this.name+" ************************************************ 看看谁给的 力 "+v2);
        if (!playerRigidbody2D) return;
        //print("  22222  ");
        if(IsSetZero) playerRigidbody2D.velocity = Vector2.zero;
        playerRigidbody2D.AddForce(v2);
    }


    public virtual void GetTuiliACing(Vector2 v2, bool IsSetZero = false)
    {
        if (!playerRigidbody2D) return;
        if (IsSetZero) playerRigidbody2D.velocity = Vector2.zero;
        isAcing = true;
        playerRigidbody2D.AddForce(v2);
    }

    float acingTime = 0;
    float acTimeDelta = 1; 
    protected virtual void ACingTimes()
    {
        if (isAcing)
        {
            acingTime += Time.deltaTime;
            if (acingTime >= acTimeDelta)
            {
                print("  ACingTimes  延迟 还原？？？？？？？？？？？？  ");
                isAcing = false;
                acingTime = 0;
            }
        }
    }


    protected virtual void BeHitFlyOut(float power)
    {
        if (GlobalTools.FindObjByName("player"))
        {
            //print("die 飞出去 ！！！！！！！！！！！！");
            //playerRigidbody2D.AddForce(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
            GetZongTuili(GlobalTools.GetVector2ByPostion(this.transform.position, GlobalTools.FindObjByName("player").transform.position, 10) * GlobalTools.GetRandomDistanceNums(power));
        }

    }


    public void ReSetLR()
    {
        isRunRighting = false;
        isRunLefting = false;
        if(DBBody.animation.lastAnimationName == RUN)
        {
            //GetStand();
        }
    }

    public virtual void Run()
    {
        //print("   >>>>>>>> run   !!! ");
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        
        //print("isJumping   " + isJumping + "    isDowning  " + isDowning + "   isBeHiting  " + roleDate.isBeHiting + "isInAiring" + isInAiring + "   isDodgeing  " + isDodgeing);
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;
        //print(DBBody.animation.lastAnimationName + "  -------------->?????????              " + RUN);
        //print("??????   "+isRunLefting +"    "+isRunRighting);
        //if (DBBody.animation.lastAnimationName == RUN|| DBBody.animation.lastAnimationName == STAND) return;

        if (DBBody.animation.lastAnimationName != RUN)
        {
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }
    }

    void Walk()
    {
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName != WALK && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame(WALK);
        }
    }

    protected bool isJump2 = false;
    protected bool isJumping2 = false;
    //bool isQiTiao = false;
    public virtual void GetJump()
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        if (!isJumping)
        {
            if (isCanJump) {
                //isCanJump = false;
                //Jump();
                isJumping = true;
            }
        }
        else
        {
            if (!isJumping2)
            {
                isJump2 = true;
                isJumping2 = true;
            }
        }
    }

    protected void MoveXByPosition(float xDistance)
    {
        newPosition.x += xDistance;
        this.transform.localPosition = newPosition;
    }

    protected void MoveYByPosition(float yDistance)
    {
        newPosition.y += yDistance;
        this.transform.localPosition = newPosition;
    }


    public void BackJumpVX(float vx)
    {
        if (IsGuDingTuili) return;
        var _vx = Mathf.Abs(vx);
        if (GetComponent<RoleDate>().isDie) return;
        playerRigidbody2D.velocity = Vector2.zero;
        if (bodyScale.x < 0)
        {
            //playerRigidbody2D.AddForce(Vector2.left * _vx);
            GetZongTuili(Vector2.left * _vx,true);
            //newSpeed = new Vector2(-_vx, 0);
        }
        else if (bodyScale.x > 0)
        {
            //newSpeed = new Vector2(_vx, 0);
            //playerRigidbody2D.AddForce(Vector2.right * _vx);
            GetZongTuili(Vector2.right * _vx, true);
        }
        
        //playerRigidbody2D.velocity = newSpeed;
        //MoveVX(newSpeed,true);
    }

    /// <summary>
    /// X方向的加速度  推力或者 速度
    /// </summary>
    /// <param name="vx">速度数值大小</param>
    /// <param name="isSpeed">是否是速度 否则的话是推力计算</param>
    /// <param name="isNoAbs">是否去绝对值</param>
    protected void MoveVX(float vx, bool isSpeed = false ,bool isNoAbs = false)
    {

        if (IsGuDingTuili) return;
        var _vx = Mathf.Abs(vx);
        if (isNoAbs) _vx = vx;
        playerRigidbody2D.velocity = new Vector2(vx, playerRigidbody2D.velocity.y);//Vector2.zero;
        //newSpeed.x = 0;


        if (bodyScale.x < 0)
        {
            if (isSpeed) {
                newSpeed = new Vector2(vx, playerRigidbody2D.velocity.y);
            }
            else
            {
                //playerRigidbody2D.AddForce(new Vector2(vx, 0));
                GetZongTuili(new Vector2(vx, 0));
            }
            
        }
        else if (bodyScale.x > 0)
        {
            if (isSpeed) {
                newSpeed = new Vector2(-vx, playerRigidbody2D.velocity.y);
            }
            else
            {
                //playerRigidbody2D.AddForce(new Vector2(-vx, 0));
                GetZongTuili(new Vector2(-vx, 0));
            }
        }

        //print("---vx2    " + playerRigidbody2D.velocity);


        playerRigidbody2D.velocity = newSpeed;
        
    }

    protected void MoveVY(float vy)
    {
        GetZongTuili(Vector2.up * vy);
        playerRigidbody2D.velocity = newSpeed;
    }

    public void SetYSpeedZero()
    {
        Vector2 v2 = new Vector2(playerRigidbody2D.velocity.x, 0);
    }

    public void SetXSpeedZero()
    {
        Vector2 v2 = new Vector2(0,playerRigidbody2D.velocity.y);
    }

    public virtual void Jump()
    {
        if (isAtking || isDodgeing || roleDate.isBeHiting) return;
        if (isCanJump && !roleDate.isBeHiting && !isAtking && DBBody.animation.lastAnimationName != JUMPHITWALL &&
            DBBody.animation.lastAnimationName != JUMP2DUAN &&
            DBBody.animation.lastAnimationName != DOWNONGROUND &&
            DBBody.animation.lastAnimationName != JUMPUP)
        {
            isQiTiao = false;
            DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);
        }

        if (isCanJump && !isQiTiao && DBBody.animation.lastAnimationName == JUMPUP)
        {
            isQiTiao = true;
            isCanJump = false;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0);
            //playerRigidbody2D.AddForce(Vector2.up * yForce);
            GetZongTuili(Vector2.up * yForce);
            return;
        }


        if (isInAiring)
        {
            if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                isJump2 = false;
                if (DBBody.animation.lastAnimationName == JUMPHITWALL) {
                    newPosition = this.transform.localPosition;
                    if (bodyScale.x == 1)
                    {
                        //MoveXByPosition(0.1f);
                        //playerRigidbody2D.AddForce(Vector2.right * wallJumpXNum);
                        GetZongTuili(Vector2.right * wallJumpXNum);
                    }
                    else
                    {
                        //MoveXByPosition(-0.1f);
                        //playerRigidbody2D.AddForce(Vector2.left * wallJumpXNum);
                        GetZongTuili(Vector2.left * wallJumpXNum);
                    }
                }
                isJumping2 = true;
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0.1f;
                playerRigidbody2D.velocity = newSpeed;
                //playerRigidbody2D.AddForce(Vector2.up * yForce);
                GetZongTuili(Vector2.up * yForce);
                return;
            }
        }

    }


    //防止AI卡在 边角
    void LuodiXiuZheng()
    {
        if (this.tag == "Player") return;
        //落地修正  如果速度 很小 并且动作是下落动作 卡在 边路就 推下去
        if(DBBody.animation.lastAnimationName == JUMPDOWN&&Mathf.Abs(GetPlayerRigidbody2D().velocity.y)<=0.3f)
        {
            if(this.transform.localScale.x == -1)
            {
                //朝右
                GetPlayerRigidbody2D().AddForce(Vector2.right*10);
            }
            else
            {
                //朝左
                GetPlayerRigidbody2D().AddForce(Vector2.left * 10);
            }

        }
    }

    public virtual void InAir()
    {
        if (isDodgeing && IsHitMQWall && isInAiring)
        {
            //碰到墙
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL, 0, 1);
            //isJump2 = false;
            isJumping2 = false;
            isDowning = false;
            isDodgeing = false;
            isDodge = false;
            playerRigidbody2D.gravityScale = gravityScaleNums;
            return;
        }


        if (isDodgeing||isAcing) return;
        isInAiring = !IsGround;
        if (IsGround&&DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            //print("???????????");
            if (DBBody.animation.isCompleted)
            {
                //print("luodidongzuo zuowan");
                isDowning = false;
                isJumping = false;
                isJumping2 = false;
                isQiTiao = false;
                isJump2 = false;
                isAtkYc = false;
                //落地还原 不然 地上攻击会累加
                atkNums = 0;
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //GetStand();
            }
            return;
        }


        //print("isqitiao  "+isQiTiao);

        if (IsGround&&!isBackUping&&(DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
                isAtkYc = false;
                isAtking = false;
                isAtk = false;
                isJumping2 = false;
                if (roleAudio)
                {
                    roleAudio.PlayAudioYS("downOnGround");
                }
                MoveVX(0);
            }
        }

        LuodiXiuZheng();

        if (isAtking)
        {
            if (isInAiring)
            {
                //playerRigidbody2D.gravityScale = 2f;
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x*0.9f , playerRigidbody2D.velocity.y*0.9f);
            }
            return;
        }
        else {
            //playerRigidbody2D.gravityScale = 4.5f;
        }
        

        if (IsHitMQWall && isInAiring)
        {
            //碰到墙
            if (DBBody.animation.lastAnimationName != JUMPHITWALL) DBBody.animation.GotoAndPlayByFrame(JUMPHITWALL, 0, 1);
            //isJump2 = false;
            isJumping2 = false;
            isDowning = false;
            return;
        }
        


        if (isInAiring)
        {
            if(roleDate.isBeHiting|| DBBody.animation.lastAnimationName == BEHIT|| DBBody.animation.lastAnimationName==BEHITINAIR) return;
            if (newSpeed.y <= 0)
            {
                if (!isDowning)
                {
                    //下降
                    isDowning = true;
                    //还原落地攻击的BUG
                    isQiTiao = true;
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
            }
            else
            {
                if (isJumping2 && (DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL|| DBBody.animation.lastAnimationName == RUNBEGIN) && !DBBody.animation.isCompleted) return;
                if (DBBody.animation.lastAnimationName != JUMPDOWN)
                {
                    //上升
                    //print("shangsheng");
                    //newSpeed.y >0 的时候是上升  这个是起跳动作完成后 上升的时候 停留在下降的最后一帧 
                    //做动画的时候  下落动画第一帧就是 起跳最后一帧
                    DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                    DBBody.animation.Stop();
                    isDowning = false;
                }
            }
        }
    }


    [Header("返回 站立姿势 的 类型 true是直接goto false是动画过去（可能有bug）")]
    public bool IsZJToStandType = true;


    protected virtual void Stand()
    {
        //TurnRight();
        //if (DBBody.animation.lastAnimationName == DODGE2|| DBBody.animation.lastAnimationName == DODGE1) return;
        if (roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (isDowning) return;
        if (!DBBody.animation.HasAnimation(STAND))
        {
            STAND = "stand_1";
        }
        if (this.tag != "player") print(">  "+DBBody.animation.lastAnimationName+"   atking "+isAtking+"  isInAiring "+isInAiring);
        if (DBBody.animation.lastAnimationName != STAND|| (DBBody.animation.lastAnimationName == STAND&& DBBody.animation.isCompleted)) {
            print(" ******************************************* ---->inStands ");
            if (this.tag == "player") {
                //DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                DBBody.animation.FadeIn(STAND, 0.1f);
            }
            else
            {
                //DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //时间0.01f  0.1秒 慢了会报错（位置错误）
                //print("stand!!!");

                //DBBody.animation.FadeIn(STAND, 0.01f, 1);
                DBBody.animation.GotoAndPlayByFrame(STAND, 0, 1);
                //print(" ///////// fadein stand!!");
                //DBBody.animation.FadeIn(STAND, 0.5f, 1);
            }
            
        }


        //修正 防止卡死----------
        isDowning = false;
        isBackUp = false;
        isBackUping = false;



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

    public void GetStand(bool isSetSpeedZero = true)
    {
        
        ResetAll();
        //print("getStand!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!    isInAiring    "+isInAiring);
        if (DBBody) Stand();
        StopVSpeed();


    }


    public string GetStandACName()
    {
        return STAND;
    }

    public void StopVSpeed(bool isSetSpeedZero = true)
    {
        if (isSetSpeedZero && playerRigidbody2D)
        {
            playerRigidbody2D.velocity = Vector2.zero;
        }
    }

    public void SetV0()
    {
        isDodge = false;
        isDodgeing = false;
        isBackUp = false;
        isBackUping = false;
        if(roleDate) roleDate.GetInit();
        GetStand();
        if (playerRigidbody2D) playerRigidbody2D.velocity = Vector2.zero;
    }


    protected TheTimer _theTimer;

    // Use this for initialization
    protected void Start () {
        FirstStart();
        GetStart();
        //print("///////-------------------------------------  START  "+this.name);
    }

    protected virtual void FirstStart()
    {

    }


    public RoleAudio roleAudio;
    protected PlayerUI thePlayerUI;
    protected virtual void GetStart()
    {
        //Tools.timeData();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        _recordGravity = gravityScaleNums;
        DBBody = GetComponentInChildren<UnityArmatureComponent>();

        roleAudio = GetComponent<RoleAudio>();
        //print("DBBody   "+ DBBody);
        _theTimer = GetComponent<TheTimer>();
        //DBBody.AddEventListener(DragonBones.FrameEvent.MOVEMENT_FRAME_EVENT, this.onMOVEMENTBoneEvent, this);
        DBBody.AddDBEventListener(DragonBones.EventObject.FRAME_EVENT, this.ShowACTX);
        DBBody.AddDBEventListener(DragonBones.EventObject.SOUND_EVENT, this.ShowACTX);
        roleDate = GetComponent<RoleDate>();
        //DBBody.AddDBEventListener(EventObject.FRAME_EVENT, this.test);
        DBBody.AddDBEventListener("atks", this.Test);
        bodyScale = new Vector3(1, 1, 1);
        vOAtk = GetComponent<VOAtk>();
        this.transform.localScale = bodyScale;
        if(_yanmu2) _yanmu2.Stop();
        _yanmu.Stop();
        GetYuanColor();
        //thePlayerUI = GlobalTools.FindObjByName("PlayerUI").GetComponent<PlayerUI>();

        GameOver();
        _KuangDowny = -100;
        //kuang 的 底y位置 超出die
        StartCoroutine(IEDestory2ByTime(1f));
        //print("-/////////////////---------------------------------------------------------------------------PlayerStart"+roleDate);
    }

    bool IsCanOutYDie = false;
    public IEnumerator IEDestory2ByTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        GetKuangDownY();
    }

    protected float _KuangDowny = -100;
    void GetKuangDownY()
    {
        _KuangDowny = GlobalTools.FindObjByName("kuang").transform.position.y + GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>().bounds.extents.y - GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>().bounds.size.y-15;
        IsCanOutYDie = true;
        //print(""+ GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>().bounds.extents.y + " -------------------------------------------------> shendushiduoshao????     "+ GlobalTools.FindObjByName("kuang").GetComponent<BoxCollider2D>().bounds.size.y);
        //print(" *****************************************************************************************  "+ _KuangDowny);
    }

    [Header("是否 会die 当超出 底部限制的时候")]
    public bool IsNeedDieOutDownY = true;
    protected void OutDownYDie()
    {
        if (!IsCanOutYDie) return;
        if (Globals.isInPlot) return;
        if (!IsNeedDieOutDownY) return;
        //if (this.tag == "Player") print(this.transform.position.y + "  --------------------------- " + _KuangDowny);
        if (this.transform.position.y< _KuangDowny)
        {
            //if(this.tag == "Player")print(this.transform.position.y + "  --------------------------- "+ _KuangDowny);
            this.GetComponent<RoleDate>().live = 0;
        }
    }

    public void StopYanMu()
    {
        if (_yanmu2) _yanmu2.Stop();
        if (_yanmu) _yanmu.Stop();
    }

    public virtual void GameOver()
    {
        //ObjectEventDispatcher.dispatcher.addEventListener(EventTypeName.GAME_OVER, this.RemoveSelf);
    }


    public GameObject hitKuai;

    [Header("die 是否会旋转")]
    public bool IsDieRotation = false;

    [Header("die 是否飞出去")]
    public bool IsDieFlyOut = false;
    protected virtual void FeiChuqu()
    {
        //if (hitKuai) hitKuai.SetActive(false);
        if (IsDieRotation) {
            GetPlayerRigidbody2D().freezeRotation = false;
            //隐藏血条
        }
        BeHitFlyOut(50);
    }


    [Header("die 是否会被打散 爆炸")]
    public bool IsDieBoomOutObj = false;

    [Header("是否随机 爆掉 铠甲头盔等")]
    public bool IsRandOMboomOutObj = false;

    [Header("die 飞出去的 物品列表")]
    public List<string> FlyObjList = new List<string>() { };

   
    
    protected void DieBoomOutObj()
    {
        if (FlyObjList.Count == 0) {
            int objNums = 8 + GlobalTools.GetRandomNum(20);
            for (int i = 0; i < objNums; i++)
            {
                GameObject _obj = ObjectPools.GetInstance().SwpanObject2(Resources.Load("baozha_tuowei") as GameObject);
                _obj.transform.position = this.transform.position;
                _obj.GetComponent<FlyOutObj>().BeHitFlyOut(80+GlobalTools.GetRandomDistanceNums(30));
            }
            return;
        }
        
        //一件不飞
        int nums = FlyObjList.Count;// GlobalTools.GetRandomNum(FlyObjList.Count);
        for (int i=0;i<nums;i++)
        {
            if (IsRandOMboomOutObj)
            {
                if (GlobalTools.GetRandomNum() > 50) continue;
            }
            string BoneName = FlyObjList[i].Split('|')[0];
            string FlyObjName = FlyObjList[i].Split('|')[1];

            //print("---BoneName   "+ BoneName);
            //print(" gutou ?????   "+GetDB().armature.GetBone(BoneName)+"  骨头名字   "+GetDB().armature.GetBone(BoneName).name+"  "+ GetDB().armature.GetBone(BoneName).visible);

            //GetDB().armature.GetBone(BoneName).visible = false;
            //GetDB().armature.GetBone(BoneName).slot.visible = false;
            //(GetDB().armature.GetBone(BoneName).slot.display as GameObject).SetActive(false);

            if (GetDB().armature.GetSlot(BoneName) != null) {
                GetDB().armature.GetSlot(BoneName).displayIndex = -1;
            }

            GameObject _obj = ObjectPools.GetInstance().SwpanObject2(Resources.Load(FlyObjName) as GameObject); //GlobalTools.GetGameObjectByName(FlyObjName);
            if (_obj == null) _obj = ObjectPools.GetInstance().SwpanObject2(Resources.Load("ciwei_ci2") as GameObject);
            _obj.transform.position = this.transform.position;
            _obj.GetComponent<FlyOutObj>().BeHitFlyOut(80);
        }

    }


    [Header("die 爆出可以拾取的物品")]
    public bool IsGetDieOut = false;
    public virtual void GetDie()
    {

        if (DBBody.animation.lastAnimationName != DIE) {
            DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
            if (hitKuai) hitKuai.SetActive(false);
            if (IsDieBoomOutObj) DieBoomOutObj();
            if (IsDieFlyOut) FeiChuqu();
        }
        
        //print("回收");
        //对象池无法移除对象 原因不明
        //ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 1f);
        //this.gameObject.SetActive(false);
        if (!IsGetDieOut)
        {
            IsGetDieOut = true;
            
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT,roleDate.enemyType), this);
        }

        if (isDieRemove) StartCoroutine(IEDieDestory(2f));
    }

    public IEnumerator IEDieDestory(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        //playerRigidbody2D.velocity = Vector2.zero;
        
        //Destroy(this.gameObject);
        if(this.tag == "Player")
        {
            Time.timeScale = 1f;
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_OVER), this);
            Globals.isGameOver = true;
            //this.gameObject.SetActive(false);
        }
        else
        {
            if (this.GetComponent<RoleDate>().enemyType == "enemy") {
                //Destroy(this);
                this.gameObject.SetActive(false);
            }
            
            //DestroyImmediate(this, true);
        }
        //this.gameObject.SetActive(false);
        
        //Destroy(this);
    }

    //protected bool IsZongPauseStop = false;
    public void GetPause(float pauseTime = 0.1f, float scaleN = 0.5f, bool IsZongStop = false)
    {
        //这一块 就是 被击中 延迟  想其他办法解决
        //if(DBBody.animation.lastAnimationName!=BEHIT|| DBBody.animation.lastAnimationName != BEHITINAIR)
        //{
        //    pauseTime = 0.1f;
        //}

        if (IsZongStop)
        {
            DBBody.animation.timeScale = 0;
        }
        else
        {
            DBBody.animation.timeScale = scaleN;
        }

        //if(_theTimer) _theTimer.GetStopByTime(pauseTime);
        if (_theTimer) _theTimer.TimesAdd(pauseTime, TimeCallBack);
    }

    //-----附带伤害 和效果---------------------------------------------------
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

        if(n == 3)
        {
            //持续时间
            cxTime = float.Parse(fdStr.Split('_')[1]);
            //每秒伤害
            meimiaoSH = float.Parse(fdStr.Split('_')[2]);

        }

        if(fdName == "mabi")
        {
            //print("  mabi!!!!!!!!!!!!!  ");
            IsMabi = true;
           
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

        DBBody.animation.timeScale = 0.1f;
        GetPlayerRigidbody2D().velocity = Vector2.zero;
        //GetPlayerRigidbody2D().gravityScale = 0;

        if (_mbTimes>= _mbMaxTimes||roleDate.isDie)
        {
            IsMabi = false;
            DBBody.animation.timeScale = 1;
            _mbTimes = 0;
            _mbMaxTimes = 0;
            //GetPlayerRigidbody2D().gravityScale = 4.5f;
        }
    }


    void FuDaiXiaoguos()
    {
        FDMaBi();
    }



    public void SetACPlayScale(float scaleN)
    {
        DBBody.animation.timeScale = scaleN;
    }




    void TimeCallBack(float n)
    {
        DBBody.animation.timeScale = 1;
      
    }


    // Update is called once per frame
    void Update () {
        GetUpdate();
    }

    Color _ycolor = Color.white;
    void GetYuanColor()
    {
        List<DragonBones.Bone> bones = GetDB().armature.GetBones();
        foreach (DragonBones.Bone o in bones)
        {
            if (o.slot != null)
            {
                if (o.slot.display != null)
                {
                    _ycolor = (o.slot.display as GameObject).GetComponent<Renderer>().material.color;
                    return;
                }
            }
        }
    }


    public void GetBoneColorChange(Color color)
    {
        List<DragonBones.Bone> bones = GetDB().armature.GetBones();
        //print(" ///////////////////////////////////////////////////     被攻击改变颜色！！！！  ");
        foreach(DragonBones.Bone o in bones)
        {
            //print("name:  " + o.GetType()+o.slot);

            if (o.slot != null)
            {
                //if(o.slot.display!=null &&(o.slot.display as GameObject).GetComponent<Renderer>()!=null&& (o.slot.display as GameObject).GetComponent<Renderer>().material!=null&& (o.slot.display as GameObject).GetComponent<Renderer>().material.color!=null != null) (o.slot.display as GameObject).GetComponent<Renderer>().material.color = Color.red;
                //print("???    "+o.slot.name);
                if (o.slot.display != null) {
                    //print("??     "+color);
                    (o.slot.display as GameObject).GetComponent<Renderer>().material.color = color;
                    //print(" ------>    "+ (o.slot.display as GameObject).GetComponent<Renderer>().material.color);
                } 

                //print(GetComponent<Renderer>().material.GetTextureOffset);
                //return;
            }
            //print(o.slot._SetColor(DragonBones.ColorTransform));
        }
    }


    public void BeHitColorChange()
    {
        GetBoneColorChange(Color.red);
        //GetBoneColorChange(Color.red);
        StartCoroutine(IReSetBeHitColor(0.23f));
    }


    public IEnumerator IReSetBeHitColor(float time)
    {
        yield return new WaitForSeconds(time);
        //print("变白！！！");
        GetBoneColorChange(_ycolor);
    }

    public void SetInitColor(Color _color2)
    {
        _ycolor = _color2;
    }


    protected virtual void IsCanShanjinAndJump()
    {
        if (IsGround || IsHitMQWall) {
            isCanShanjin = true;
            isJumping2 = false;
        }

        if (IsGround) {
            isJumping2 = false;
            isCanJump = true;
        }
        

    }


    
    //减速 按比例
    public virtual void Jiansu(float bili,float jsTime = 1)
    {
        GetPlayerRigidbody2D().velocity *= bili;
    }


    public bool TSACControl = false;
    protected virtual void GetUpdate()
    {
        //print(this.name+ "  isDowning:"+isDowning+ "  roleDate.isBeHiting:"+ roleDate.isBeHiting+ "  isAcing:"+ isAcing+ "  isDodgeing:"+ isDodgeing+"   acName:"+ DBBody.animation.lastAnimationName+" isInAiring:"+isInAiring+" isJumping:"+isJumping);

        if (TSACControl) return;
        CurrentAcName = DBBody.animation.lastAnimationName;
        //脚下的烟幕
        Yanmu();

        //附带效果
        FuDaiXiaoguos();

        //固定推力 恢复计时
        GuDingTuiLiHufu();


        if (roleDate.isDie)
        {
            if(GetComponent<DieOut>()&& !GetComponent<DieOut>().IsNeedDieSlowAC) DBBody.animation.timeScale = 1;
            GetDie();
            return;
        }

        

        if (roleDate.live <= 0)
        {
            roleDate.isDie = true;
        }


        OutDownYDie();


        if (Globals.isInPlot) return;

        BeHitslowing();

        if (roleDate.isBeHiting)
        {
            //print(" 被攻击了 控制 推力  beHiting!!!!!!!!");
            ControlSpeed(30);
            GetBeHit();
            //DBBody.animation.timeScale = 1;
            return;
        }

        //if (this.tag != "Player") ACingTimes();

        //if (CurrentAcName != BEHIT || CurrentAcName != BEHITINAIR || CurrentAcName != DIE)
        //{
        //    //DBBody.animation.timeScale = 1;
        //}


        //if (_theTimer != null && !_theTimer.IsPauseTimeOver())
        //{
        //    return;
        //}
        //else
        //{
        //    //DBBody.animation.timeScale = 1;
        //}




        if (isQianhuaing)
        {
            //print("isQianhuaing");
            //防止加速过快 限制最大速度
            ControlSpeed(60);
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
            ControlSpeed(35);
            Dodge1();
            return;
        }

        
        if(!IsJiasu && !isAtking)ControlSpeed();

       



        InAir();
        IsCanShanjinAndJump();

        if (isAcing)
        {
            //print("isAcing  "+isAcing);
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


    protected virtual void InStand()
    {
        if (!roleDate.isBeHiting && !isQianhuaing && !isInAiring && !isDowning && !isRunLefting && !isRunRighting&&!isRunYing && !isJumping && !isJumping2 && !isAtking && !isDodgeing && !isAtkYc && !isBackUping)
        {
            //if (this.tag != "diren") print("stand" + "  ? " + isRunLefting + "   " + DBBody.animation.lastAnimationName);

            Stand();
            //print("回到 stand   isatking> " + isAtking + "  isAtk " + isAtk + "  isDown " + isDowning + "  isJump2  " + isJump2 + "  isjumping2 " + isJumping2 + "  isjumping " + isJumping);
        }
    }

    public virtual void Testss()
    {
        //print("Gamebody!");
    }

    public int beHitNum = 0;
    public virtual void HasBeHit(float chongjili = 0, bool IsOther = true)
    {
        if (DBBody == null) return;
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;
        //if (GetComponent<AIBase>()) GetComponent<AIBase>().AIGetBeHit();

        if (isInAiring)
        {
            if (DBBody.animation.HasAnimation(BEHITINAIR))
            {
                DBBody.animation.GotoAndPlayByFrame(BEHITINAIR, 0, 1);
                return;
            }
        }
        DBBody.animation.GotoAndPlayByFrame(BEHIT, 0, 1);
        beHitNum++;

        if (chongjili == -10)
        {
            BeHitSlowByTimes();
        }
    }


    protected float _BeHitSlowTimes = 1;
    protected float _BeHitSlowTimesNums = 0;
    protected float _slowScale = 0;


    public void BeHitSlowByTimes(float beHitSlowTimes = 4,float slowScale = 0f)
    {
        _BeHitSlowTimesNums = 0;
        _BeHitSlowTimes = beHitSlowTimes;
        _slowScale = slowScale;

        _IsBeHitSlowing = true;
        DBBody.animation.timeScale = slowScale;
        //GetPause(beHitSlowTimes, slowScale);
    }

    public bool _IsBeHitSlowing = false;
    protected void BeHitslowing()
    {
        //print("  ------------------------------------slowingBeHitslowing ");
        if (!_IsBeHitSlowing) return;
        DBBody.animation.timeScale = _slowScale;
        if (_BeHitSlowTimesNums >= _BeHitSlowTimes)
        {
            _IsBeHitSlowing = false;
            DBBody.animation.timeScale = 1;
            _BeHitSlowTimesNums = 0;
        }
        
        _BeHitSlowTimesNums += Time.deltaTime;
        //print( this.name+  "  ------------------------------------------??????BeHitslowingBeHitslowingBeHitslowingBeHitslowing "+ _BeHitSlowTimes+"   ???  "+ _BeHitSlowTimesNums+ "    ????????? DBBody.animation.timeScale   "+ DBBody.animation.timeScale+ "  lastAnimationName:   " + DBBody.animation.lastAnimationName);
    }


    protected virtual void GetBeHit()
    {
        //print("jinde shi nage???");
        if((DBBody.animation.lastAnimationName == BEHIT|| DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted) {
            roleDate.isBeHiting = false;
            if (IsGround) GetStand();
        }
        else
        {
            //print(" isBeHiting! 但是没有进入 behit 动作 ");
        }

        

    }

    //反击 被攻击动作清零
    public void FanJiBeHitReSet()
    {
        isSkilling = false;
        isSkillOut = false;
        IsSFSkill = false;
        roleDate.isBeHiting = false;
        roleDate.isBeHit = false;
    }

    
    public ParticleSystem _yanmu;
    public ParticleSystem _yanmu2;
    protected virtual void Yanmu()
    {
        //print("wokao!!!!!");
        if (IsGround && !isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            _yanmu.Play();
            return;
        }


        if(IsGround && DBBody.animation.lastAnimationName == DOWNONGROUND)
        {
            _yanmu.Play();
            return;
        }

        if (IsGround && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 3)
        {
            //print("wokao!!!!!   " + IsGround + "  v  " + Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
            _yanmu.Play();
        }
        else
        {
            //print("hi   "+_yanmu.isStopped);
            _yanmu.Stop();
        }


        //if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
        //{
          //  _yanmu2.Play();
            //return;
        //}

        if (IsHitMQWall && isInAiring&& DBBody.animation.lastAnimationName == JUMPHITWALL&& !DBBody.animation.isCompleted) {
            //碰到墙
            if (_yanmu2) _yanmu2.Play();
            return;
        }


        //print("x " + GetComponent<Rigidbody2D>().velocity.x);

        if (IsHitMQWall && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 3) {
            
            if(_yanmu2) _yanmu2.Play();
            //if(GetComponent<RoleAudio>()) GetComponent<RoleAudio>().PlayHitWallDown(true);
        }
        else
        {
            if (_yanmu2) _yanmu2.Stop();
            //if (GetComponent<RoleAudio>()) GetComponent<RoleAudio>().PlayHitWallDown(false);
        }
    }



    //1.动作名 调用 函数  动作名 和 特效名字
    //2.获取VO信息  包含 位置 相对位置（给每个角色基本信息加宽高补偿 缩放补偿） 是否需要补偿释放（有些技能特效不需要 寻找相对位置和缩放）
    //2.判断是否包含动作 有一类技能无动作可以直接释放
    //3.




    //一个是延迟 一个是连击的给出时间  连击给出时间还是得在延迟内   给个时间差做减法 在减法内 不能控制 超过减法可以接连招 跑完回复站立


    //动作控制流程
    float acNums = 0;
    //是否在动作延迟
    protected bool isACCompletedYanchi = false;
    //动作滞留延迟
    int yanchiNum = 10;
    float yanchiMaxNum = 20;
    //延迟行动尺度
    [Header("攻击后的延迟 数值越大 出招越快 延迟时间越短")]
    public int canMoveNums = 100;
    public bool isAcing = false;
	protected string _acName;
    float yanchiTime = 0;


    public void GetACMsgOverYC(float ycTimes)
    {
        acNums = 0;
        yanchiTime = ycTimes;
    }

    public string GetAcMsg(string acName,int type = 1,float FadeInTimes = 0.2f)
    {
        if (acName == null) return null;
        //获取技能VO
        //GetSkillVOByName(acName);
        //if(this.name == "player")print(this.name+"  》》》》》》------------  "+acName);
        if (!DBBody.animation.HasAnimation(acName)) return null;
        //print("------------------------------------------------------------------22  " + acName);
        
        //print("1111111111");
        if (DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted)
        {
            acNums+= Time.deltaTime;
            yanchiMaxNum = yanchiTime;
            //if (this.name == "player") print(this.name+"   动作 已完成------------------------------------------------------------------  " + acName);
            //isACCompletedYanchi = true;
            if (acNums > yanchiNum)
            {
                //可以切换招式
            }

            //if (yanchiTime != 0)
            //{
            //    if (!isACCompletedYanchi) {
            //        isACCompletedYanchi = true;
            //        print("  动作延迟   "+yanchiTime);
            //        GetComponent<TheTimer>().TimesAdd(yanchiTime, AcYanChiCallBack);
            //    }
                
            //    return "acYanChi";
            //}
            //print("22222222222");
            //这里的延迟 将来会改到 vo里面直接取  招式切换也是 这个Ac主要针对 非攻击Ac
            if (acNums > yanchiMaxNum) {
                isAcing = false;
                isACCompletedYanchi = false;
                _acName = null;
                acNums = 0;
                if (isInAiring)
                {
                    if(DBBody.animation.HasAnimation(JUMPDOWN)) DBBody.animation.GotoAndPlayByFrame(JUMPDOWN, 0, 1);
                }
                //if (this.name == "player") print("  -------动作完成isAcing   " + isAcing+ "     lastAnimationName    "+ DBBody.animation.lastAnimationName);
                return "completed";
            }
            return "acYanChi";
        }
        //print("DBBody.animation.lastAnimationName     " + DBBody.animation.lastAnimationName);
        if (DBBody.animation.HasAnimation(acName)&&DBBody.animation.lastAnimationName!=acName)
        {
            if(type == 1)
            {
                DBBody.animation.GotoAndPlayByFrame(acName, 0, 1);
            }
            else
            {
                DBBody.animation.FadeIn(acName, FadeInTimes, 1);
            }
            
            acNums = 0;
            isAcing = true;
			_acName = acName;

            //if (this.name == "player") print(this.name+"   acName  进来没？？？？  ACName     "+ acName);
            return "start";
        }
        //if (this.name == "player") print(this.name+"   ?????? lastAnimationName  " + DBBody.animation.lastAnimationName);
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isPlaying)
        {
            //if (this.name == "player") print( this.name+ "  *******  acName "+_acName+"   ---  "+isAcing);
			return "playing_"+acNums;
        }
        
        return null;
    }


    void AcYanChiCallBack(float n)
    {
        //print("------------------------------------->    callback   延迟结束！！！！！ ");
        isAcing = false;
        isACCompletedYanchi = false;
        _acName = null;
        yanchiTime = 0;
    }

    private void GetSkillVOByName(string acName)
    {
        //throw new NotImplementedException();
    }

    

    public void SpeedXStop()
	{
		if (playerRigidbody2D != null) playerRigidbody2D.velocity = Vector3.zero;
		//throw new NotImplementedException();
	}

	public void SetACingfalse()
	{
		isAcing = false;
		//throw new NotImplementedException();
	}

    public void TestsI()
    {
        print("hi 接口！！");
    }


    protected float atkNums = 0;
    protected bool isAtk = false;
    public bool isAtking = false;
    string[] atkMsg;
    protected VOAtk vOAtk;
    protected Dictionary<string, string>[] atkZS;
    
    public virtual void GetAtk(string atkName = null)
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing||isAcing) return;
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
                //print("----------------------------------atkname  "+atkName);
                //atk_1201|0.1-0.1        延迟时间-减速的速度
                if (atkName.Split('|').Length > 1)
                {
                    _atkName = atkName.Split('|')[0];
                    float times = float.Parse(atkName.Split('|')[1].Split('-')[0]);
                    float scales = 0.5f;
                    if (atkName.Split('|')[1].Split('-').Length!=1) scales = float.Parse(atkName.Split('|')[1].Split('-')[1]);
                    GetPause(times, scales);
                }
                vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(_atkName, DataZS.GetInstance()));
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);


                if (vOAtk.AudioName != "")
                {
                    roleAudio.PlayAudioYS(vOAtk.AudioName);
                }
                else
                {
                    if (roleAudio.AudioAtk_1)
                    {
                        if (GlobalTools.GetRandomNum() > 50)
                        {
                            if (GlobalTools.GetRandomNum() > 50)
                            {
                                print("  -----------------------------> AudioAtk_1 ");
                                roleAudio.PlayAudioYS("AudioAtk_1");
                            }
                            else
                            {
                                print("  -----------------------------> AudioAtk_22222222222 ");
                                roleAudio.PlayAudioYS("AudioAtk_2");
                            }
                        }
                    }
                }

               
               


            }

            MoveVX(vOAtk.xF,true);
            if (newSpeed.y < 0)
            {
                newSpeed.y = 1;
                playerRigidbody2D.velocity = newSpeed;
                MoveVY(vOAtk.yF);
            }
            //print(newSpeed.y);
            //获取XY方向的推力 
            //print(DBBody.animation.animations);

        }

    }

    void Test(string type, EventObject eventObject)
    {
        //print(type+" ???time  "+eventObject);
    }

    public bool IsSFSkill = false;

    protected bool isSkilling = false;
    public void GetSkillBeginEffect(string skillName) {
        //判断是否有该技能
        //获取技能信息 
        //vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(skillName, DataZS.GetInstance()));
        isSkilling = true;
        //播放技能动作
        vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(skillName, DataZS.GetInstance()));

        //GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName, true);
        //DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
        //GetAtk(skillName);
        GetAcMsg(vOAtk.atkName);
        if (roleAudio.SkillAudio_1) roleAudio.PlayAudioYS("SkillAudio_1");
         isSkillOut = true;
    }

  

    protected bool isSkillOut = false;

    //显示动作特效 龙骨的侦听事件
    protected virtual void ShowACTX(string type, EventObject eventObject)
    {

        if (IsSFSkill) return;
        //print("type:  "+type);
        //print("eventObject  ????  " + eventObject);
        //print("___________________________________________________________________________________________________________________________name    "+ eventObject.name+"    name  "+DBBody.animation.lastAnimationName);
        if (type == EventObject.SOUND_EVENT)
        {
            //print("eventName:  "+eventObject.name);
            //print(playerRigidbody2D.velocity.x+"  ?    "+this.transform.localScale);
            if (eventObject.name == "jumpHitWall") {
                if ((this.transform.localScale.x == 1 && playerRigidbody2D.velocity.x >= 1) || (this.transform.localScale.x == -1 && playerRigidbody2D.velocity.x <= -1)) return;
            } 
            if (eventObject.name == "runG1") return;
            if (eventObject.name == "downOnGround" && DBBody.animation.lastAnimationName!=DOWNONGROUND) return;
            //if(eventObject.name=="run1"|| eventObject.name == "run2") _yanmu.Play();
            GetComponent<RoleAudio>().PlayAudio(eventObject.name);
        }


        if (type == EventObject.FRAME_EVENT)
        {

            if (eventObject.name == "jn_begin") {
                //释放准备动作的特效
                if (isSkilling) {
                    GetComponent<ShowOutSkill>().ShowOutSkillBeginEffectByName(vOAtk.skillBeginEffect);
                    isSkilling = false;
                }
            }


            if (eventObject.name == "ac")
            {
                if (isAcing)
                {
                    
                    if (isSkillOut) {
                        GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName, true);
                        isSkillOut = false;
                    }
                    else
                    {
                        if (jn == null) {
                            print("******************************************************************************** 技能为空 ");
                            print("******************************************************************************** 技能为空 ");
                            return;
                        }
                        
                        print("--------------------------------------------------"+ jn.TXName +"----------------------------------------------->2222222222222222222222  vOAtk.txName  " + vOAtk.txName);
                        //技能释放点
                        GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName, true);
                        isTXShow = false;
                    }
                   
                }
                else {
                    //GetPause(0.1f);
                    //print("vOAtk.txName    " + vOAtk.txName);
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName,false,vOAtk);
                    isTXShow = false;
                }
                
                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_002");
                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_fk");
            }
        }
        
    }

    public bool GetIsACing()
    {
        return isAcing;
    }
    
    protected float jisuqi = 0;
    protected float yanchi = 0;

    //动作延迟  在延迟内 将isAtk=false 可以控制人物 而不会一直在动作尾不受控制
    protected bool isAtkYc = false;




    protected int kuaisujishu = 0;
    protected int kuaisujishuNums = 0;
    protected bool IsKuaisuAtkReset = false;
    public void IsAtkKuaijiReSet()
    {
        kuaisujishuNums = 0;
        if (!IsKuaisuAtkReset&& atkNums < atkZS.Length-1) IsKuaisuAtkReset = true;
    }


    protected void AtkKuaijiReSet()
    {
        if (GetComponent<RoleDate>().isBeHiting) return;
        if (!IsKuaisuAtkReset) return;
        kuaisujishuNums++;
        if(kuaisujishuNums>= kuaisujishu)
        {
            IsKuaisuAtkReset = false;
            kuaisujishuNums = 0;
            jisuqi = 0;
            isAtk = false;
            isAtking = false;
            isAtkYc = false;
            yanchi = 0;
            atkNums++;
            if (atkNums >= atkZS.Length) atkNums = 0;
        }
    }


    public void AtkReSet()
    {
        jisuqi = 0;
        isAtk = false;
        isAtking = false;
        isAtkYc = false;
        yanchi = 0;
        atkNums = 0;
    }

    public virtual void InFightAtk()
    {

    }

    protected virtual void Atk()
    {
        //print(this.name+ "  ???????????????攻击  atk     "+vOAtk.atkName);
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {
            //print(DBBody.animation.lastAnimationState);
        }
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isCompleted)
        {
            //print("攻击动作完成！！！！！！！！！！！！！！！！！！   "+this.name);
            jisuqi = 0;
            yanchi++;
            if (yanchi > vOAtk.yanchi - canMoveNums) {
                isAtk = false;
                //SpeedXStop();
                if(this.transform.tag!="Player")isAtking = false;
            }

            if (yanchi == 1)
            {
                if (atkNums <= atkZS.Length)
                {
                    atkNums++;
                }
                if (atkNums == atkZS.Length) atkNums = 0;
            }
            if (yanchi >= vOAtk.yanchi)
            {
                //超过延迟时间 失去连击
                isAtking = false;
                isAtkYc = false;
                yanchi = 0;
                atkNums = 0;
                //playerRigidbody2D.gravityScale = 4.5f;
            }
            InFightAtk();
        }

        //保险措施 
        if(DBBody.animation.lastAnimationName != vOAtk.atkName)
        {
            isAtking = false;
            yanchi = 0;
            atkNums = 0;
            //playerRigidbody2D.gravityScale = 4.5f;
        }
    }


    public bool IsAtkOver()
    {
        return !isAtking;
    }

    public virtual void GetSit2()
    {

    }

}

