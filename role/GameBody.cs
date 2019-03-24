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
    [Range(0, 150)]//数值滑杆 限定最大最小值
    public float xForce;

    //目前垂直速度
    float speedY;

    [Header("水平最大速度")]
    public float maxSpeedX;

    Vector2 newSpeed;

    [Header("垂直向上的推力")]
    public float yForce;

    [Header("感应地板的距离")]
    [Range(0, 1)]
    public float distance;

    [Header("侦测地板的射线起点")]
    public UnityEngine.Transform groundCheck;


    [Header("感应与面前墙的距离")]
    [Range(0, 1)]
    public float distanceMQ = 0.13f;

    [Header("侦测面前墙的射线起点")]
    public UnityEngine.Transform qianmian;


    [Header("当前动作名字")]
    public string CurrentAcName;

    [Header("地面图层")]
    public LayerMask groundLayer;

    [Header("是否着地")]
    public bool grounded;

    [Header("是否碰到面前的墙")]
    public bool hidWalled;

    [Header("停下后X方向的剩余滑动速度")]
    public float slideNum = 3;

    [Header("反弹跳X方向的力")]
    public float wallJumpXNum = 800;

    Vector3 newPosition;

    [Header("是否die回收")]
    public bool isDieRemove = true;


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

    protected Vector3 bodyScale;
    public Vector3 GetBodyScale()
    {
        return bodyScale;
    }

    protected bool isRunLefting = false;

    protected bool isRunRighting = false;

    public bool isInAiring = false;
    public bool isDowning = false;

    public bool isJumping = false;
    //起跳
    protected bool isQiTiao = false;

    public void ResetAll()
    {
        //isRun = false;
        isRunLefting = false;
        isRunRighting = false;
        //isInAiring = false;
        //isDowning = false;
        //在空中被击中 如果关闭跳跃bool会有落地bug
        //isJumping = false;
        // isJumping2 = false;
        //isJump2 = false;
        //isQiTiao = false;
        isAtk = false;
        isAtking = false;
        atkNums = 0;
        isAtkYc = false;
        isYanchi = false;
        isBackUping = false;
        isAcing = false;
        isYanchi = false;
    }

    protected RoleDate roleDate;


    protected string RUN = "run_3";
    protected string STAND = "stand_1";
    protected const string RUNBEGIN = "runBegin_1";
    protected const string RUNSTOP = "runStop_1";
    protected const string JUMPUP = "jumpUp_1";
    protected const string JUMPDOWN = "jumpDown_1";
    protected const string JUMPHITWALL = "jumpHitWall_1";
    protected const string DOWNONGROUND = "downOnGround_1";
    protected const string JUMP2DUAN = "jump2Duan_1";
    protected const string ATK = "atk_";
    protected string DODGE1 = "dodge_1";
    protected string DODGE2 = "dodge_2";
    protected string BEHIT = "beHit_1";
    protected string DIE = "die_1";
    //回跳跃动作
    protected string BACKUP = "backUp_1";
    protected string WALK = "walk_1";
    protected const string BEHITINAIR = "beHitInAir_1";

    protected bool isBackUp = false;
    protected bool isBackUping = false;
    public void GetBackUp()
    {
        if (roleDate.isBeHiting) return;
        if (!DBBody.animation.HasAnimation(BACKUP)) return;
        if (!isBackUp && IsGround && !isBackUping)
        {
            ResetAll();
            isBackUp = true;
            if (DBBody.animation.lastAnimationName != BACKUP) DBBody.animation.GotoAndPlayByFrame(BACKUP, 0, 1);

            isBackUping = true;
            roleDate.isCanBeHit = false;
            newSpeed.x = 0;
            newSpeed.y = 0;
            playerRigidbody2D.velocity = newSpeed;
            //print("huitiao");
            BackJumpVX(500);
            //打开有bug
            //MoveVY(200);
        }

    }

    void GetBackUping()
    {
        if (DBBody.animation.lastAnimationName == BACKUP && DBBody.animation.isCompleted)
        {
            isBackUping = false;
            isBackUp = false;
            roleDate.isCanBeHit = true;
        }
    }

    public bool GetBackUpOver()
    {
        return !isBackUping;
    }



    public void GetSkill1()
    {
        if (GlobalTools.FindObjByName("PlayerUI").name=="PlayerUI")
        {
            ShowSkill("PlayerUI/skill1");
        }
        else
        {
            ShowSkill("PlayerUI(Clone)/skill1");
        }
    }

    public void GetSkill2()
    {
        if (GlobalTools.FindObjByName("PlayerUI").name == "PlayerUI")
        {
            ShowSkill("PlayerUI/skill2");
        }
        else
        {
            ShowSkill("PlayerUI(Clone)/skill2");
        }
    }
        

    HZDate jn;
    void ShowSkill(string urlName)
    {
        if (IsHitWall) return;
        if (roleDate.isBeHiting || roleDate.isDie || isDodgeing || isAtking || isBackUping ||isAcing) return;
        //print(" >>>   "+ urlName);
        jn = GlobalTools.FindObjByName(urlName).GetComponent<SkillBox>().GetSkillHZDate();
        if (jn != null)
        {
            if (jn.IsCDOver())
            {
                if (roleDate.lan - jn.xyLan < 0) return;
                if (roleDate.live - jn.xyXue < 1) return;
                roleDate.lan -= jn.xyLan;
                roleDate.live -= jn.xyXue;
                jn.StartCD();
                //if(Globals.isDebug)print("---------------------------> 释放技能！！");
                //this.GetComponent<GetHitKuai>().GetKuai("jn_yueguang","1");
                if (jn.skillACName != null && DBBody.animation.HasAnimation(jn.skillACName))
                {
                    //***找到起始特效点 找骨骼动画的点 或者其他办法
                    GetAcMsg(jn.skillACName);
                    playerRigidbody2D.velocity = Vector2.zero;
                    if (jn.ACyanchi > 0)
                    {
                        GetPause(jn.ACyanchi);
                        //***人物闪过去的 动作 +移动速度  还有多发的火球类的特效
                    }
                }
                else {
                    //测试用 正式的要配动作
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName, true);
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
    
    bool isDodge = false;
    protected bool isDodgeing = false;

    public void GetDodge1()
    {
        if (roleDate.isBeHiting||isAcing) return;
        if (DBBody.animation.lastAnimationName == DOWNONGROUND || DBBody.animation.lastAnimationName == JUMPUP) return;

        if (isInAiring)
        {
            DODGE2 = "shanjin_1";
        }
        else
        {
            DODGE2 = "dodge_2";
        }

        if (!isDodge)
        {
            ResetAll();
            isDodge = true;
            isDodgeing = true;
            roleDate.isCanBeHit = false;
            //print("-->x  " + playerRigidbody2D.velocity.x);
            if (playerRigidbody2D.velocity.x >= 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(400);
                }
            }
            else if (playerRigidbody2D.velocity.x < 0)
            {
                if (DBBody.animation.lastAnimationName != DODGE2)
                {
                    DBBody.animation.GotoAndPlayByFrame(DODGE2, 0, 1);
                    MoveVX(400);
                }
            }
        }
    }


    void Dodge1()
    {
        if (isInAiring)
        {
            playerRigidbody2D.gravityScale = 0;
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, -1);
        }
        else
        {
            playerRigidbody2D.gravityScale = 4.5f;
        }


        if ((DBBody.animation.lastAnimationName == DODGE1 || DBBody.animation.lastAnimationName == DODGE2) && DBBody.animation.isCompleted)
        {
            isDodge = false;
            isDodgeing = false;
            roleDate.isCanBeHit = true;
            playerRigidbody2D.gravityScale = 4.5f;
        }
    }


    [Header("侦测地板的射线起点2")]
    public UnityEngine.Transform groundCheck2;
    //路面是否尽头
    bool isEndGround = false;
    public virtual bool IsEndGround
    {
        get
        {
            if (groundCheck2 == null) return false;
            Vector2 start = groundCheck2.position;
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
            if (groundCheck2 == null) return false;
            Vector2 start = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y + 0.8f);
            Debug.DrawLine(start, end, Color.yellow);
            isHitWall = Physics2D.Linecast(start, end, groundLayer);
            return isHitWall;
        }
    }


    //在玩家底部是一条短射线 碰到地板说明落到地面 
    public virtual bool IsGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }


    bool IsHitMQWall
    {
        get
        {
            Vector2 start = qianmian.position;
            Vector2 end = new Vector2(start.x - distanceMQ * bodyScale.x, start.y);
            Debug.DrawLine(start, end, Color.red);
            hidWalled = Physics2D.Linecast(start, end, groundLayer);
            if (IsGround) hidWalled = false;
            return hidWalled;
        }
    }


    public void ControlSpeed()
    {
        speedX = playerRigidbody2D.velocity.x;
        speedY = playerRigidbody2D.velocity.y;
        //钳制 speedX 被限制在 -maxSpeedX  maxSpeedX 之间
        float newSpeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        //if (horizontalDirection == 0) newSpeedX/=10;
        newSpeed.x = newSpeedX;
        newSpeed.y = speedY;
        //获取向量速度
        playerRigidbody2D.velocity = newSpeed;
    }

    public virtual void RunLeft(float horizontalDirection, bool isWalk = false)
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
            this.transform.localScale = bodyScale;
            AtkReSet();
        }

        if (isAtking) return;

        //resetAll();
        isAtkYc = false;
        isRunLefting = true;
        isRunRighting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        //print("hihihi");
        Run();

    }

    public void TurnRight()
    {
        print(">>>>    " + this.transform.localScale);
        bodyScale.x = -1;
        this.transform.localScale = bodyScale;
        print(">>>>2    " + this.transform.localScale);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.1f, 0));
        if (bodyScale.x == 1) {
            bodyScale.x = -1;
            this.transform.localScale = bodyScale;
        }

    }

    public void TurnLeft()
    {
        if (bodyScale.x == -1)
        {
            bodyScale.x = 1;
            this.transform.localScale = bodyScale;
        }


    }

    public virtual void RunRight(float horizontalDirection, bool isWalk = false)
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
            AtkReSet();
        }

        if (isAtking) return;
        //resetAll();
        isAtkYc = false;
        isRunRighting = true;
        isRunLefting = false;

        playerRigidbody2D.AddForce(new Vector2(xForce * horizontalDirection, 0));
        Run();
    }

    public void ReSetLR()
    {
        isRunRighting = false;
        isRunLefting = false;
    }

    protected virtual void Run()
    {
        //print("isJumping   "+ isJumping+ "    isDowning  "+ isDowning+ "   isBeHiting  " + roleDate.isBeHiting+ "isInAiring" + isInAiring+ "   isDodgeing  " + isDodgeing);
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;

        //if (DBBody.animation.lastAnimationName == RUN|| DBBody.animation.lastAnimationName == STAND) return;

        if (DBBody.animation.lastAnimationName != RUN)
        {
            DBBody.animation.GotoAndPlayByFrame(RUN);
        }


        //if (DBBody.animation.lastAnimationName == RUNBEGIN && DBBody.animation.isCompleted)
        //{
        //    DBBody.animation.GotoAndPlayByFrame(RUN);
        //}
        //if (DBBody.animation.lastAnimationName != RUNBEGIN && DBBody.animation.lastAnimationName != RUN)
        //{
        //    DBBody.animation.GotoAndPlayByFrame(RUNBEGIN, 0, 1);
        //}
    }

    void Walk()
    {
        if (isJumping || isInAiring || isDowning || isDodgeing || roleDate.isBeHiting) return;
        if (DBBody.animation.lastAnimationName != WALK && DBBody.animation.isCompleted)
        {
            DBBody.animation.GotoAndPlayByFrame(WALK);
        }
    }

    bool isJump2 = false;
    bool isJumping2 = false;
    //bool isQiTiao = false;
    public void GetJump()
    {
        if (roleDate.isBeHiting) return;
        if (isDodgeing) return;
        if (!isJumping)
        {
            isJumping = true;
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

    void MoveXByPosition(float xDistance)
    {
        newPosition.x += xDistance;
        this.transform.localPosition = newPosition;
    }


    void BackJumpVX(float vx)
    {
        var _vx = Mathf.Abs(vx);
        if (bodyScale.x < 0)
        {
            playerRigidbody2D.AddForce(Vector2.left * _vx);
        }
        else if (bodyScale.x > 0)
        {
            playerRigidbody2D.AddForce(Vector2.right * _vx);
        }
        //playerRigidbody2D.velocity = newSpeed;
    }

    void MoveVX(float vx, bool isNoAbs = false)
    {

        var _vx = Mathf.Abs(vx);
        if (isNoAbs) _vx = vx;
        playerRigidbody2D.velocity = Vector2.zero;
        //newSpeed.x = 0;
        if (bodyScale.x < 0)
        {
            playerRigidbody2D.AddForce(new Vector2(vx, 0));
        }
        else if (bodyScale.x > 0)
        {
            playerRigidbody2D.AddForce(new Vector2(-vx, 0));
        }
        playerRigidbody2D.velocity = newSpeed;
    }

    void MoveVY(float vy)
    {
        //Vector2 v2 = playerRigidbody2D.velocity;
        //playerRigidbody2D.velocity = new Vector2(v2.x,0);
        playerRigidbody2D.AddForce(Vector2.up * vy);
        playerRigidbody2D.velocity = newSpeed;
    }

    void Jump()
    {
        if (isAtking || isDodgeing || roleDate.isBeHiting) return;
        if (isInAiring)
        {
            if (isJump2 && DBBody.animation.lastAnimationName != JUMP2DUAN)
            {
                isJump2 = false;
                if (DBBody.animation.lastAnimationName == JUMPHITWALL) {
                    newPosition = this.transform.localPosition;
                    if (bodyScale.x == 1)
                    {
                        MoveXByPosition(0.1f);
                        playerRigidbody2D.AddForce(Vector2.right * wallJumpXNum);
                    }
                    else
                    {
                        MoveXByPosition(-0.1f);
                        playerRigidbody2D.AddForce(Vector2.left * wallJumpXNum);
                    }
                }
                isJumping2 = true;
                DBBody.animation.GotoAndPlayByFrame(JUMP2DUAN, 0, 1);
                newSpeed.y = 0.1f;
                playerRigidbody2D.velocity = newSpeed;
                playerRigidbody2D.AddForce(Vector2.up * yForce);
                return;
            }
        }


        if (IsGround && !roleDate.isBeHiting && !isAtking && DBBody.animation.lastAnimationName != JUMPHITWALL &&
            DBBody.animation.lastAnimationName != JUMP2DUAN &&
            DBBody.animation.lastAnimationName != DOWNONGROUND &&
            DBBody.animation.lastAnimationName != JUMPUP)
        {
            // print("?????>>>   "+DBBody.animation.lastAnimationName+"     "+isQiTiao);
            isQiTiao = false;
            DBBody.animation.GotoAndPlayByFrame(JUMPUP, 0, 1);
        }

        if (IsGround && !isQiTiao && DBBody.animation.lastAnimationName == JUMPUP && DBBody.animation.isCompleted)
        {
            isQiTiao = true;
            //print(yForce+"   u  "+ Vector2.up);
            playerRigidbody2D.AddForce(Vector2.up * yForce);
            return;
        }
    }

    protected virtual void InAir()
    {
        // print(DBBody.animation.lastAnimationName+"   speedy  "+ newSpeed.y);
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

        if (IsGround&&!isBackUping&&(isQiTiao || DBBody.animation.lastAnimationName == BEHIT || DBBody.animation.lastAnimationName == JUMPDOWN|| DBBody.animation.lastAnimationName == JUMP2DUAN|| DBBody.animation.lastAnimationName == JUMPHITWALL))
        {
            //落地动作
            if (DBBody.animation.lastAnimationName != DOWNONGROUND)
            {
                //print("1");
                DBBody.animation.GotoAndPlayByFrame(DOWNONGROUND, 0, 1);
                isAtkYc = false;
                isAtking = false;
                isAtk = false;
            }
        }

        if (isAtking)
        {
            if (isInAiring)
            {
                //playerRigidbody2D.gravityScale = 2f;
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x*0.9f , playerRigidbody2D.velocity.y*0.3f);
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
            if(roleDate.isBeHiting)return;
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

   
    void Stand()
    {
        if (DBBody.animation.lastAnimationName == DOWNONGROUND) return;
        //print(">  "+DBBody.animation.lastAnimationName);
        if (DBBody.animation.lastAnimationName != STAND|| (DBBody.animation.lastAnimationName == STAND&& DBBody.animation.isCompleted)) {
            //print("--");
            DBBody.animation.GotoAndPlayByFrame(STAND);
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

    public void GetStand()
    {
        ResetAll();
        if(DBBody) Stand();
        if(playerRigidbody2D) playerRigidbody2D.velocity = Vector2.zero;
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


    TheTimer _theTimer;

    // Use this for initialization
    protected void Start () {
        GetStart();
    }

    protected void GetStart()
    {
        //Tools.timeData();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        DBBody = GetComponentInChildren<UnityArmatureComponent>();
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
    }

	public virtual void GetDie()
    {
        //print("indie!!!!!!!!!!!!!!!!");
        if(DBBody.animation.lastAnimationName != DIE) DBBody.animation.GotoAndPlayByFrame(DIE, 0, 1);
        //print("回收");
        //对象池无法移除对象 原因不明
        //ObjectPools.GetInstance().IEDestory2ByTime(this.gameObject, 1f);
        //this.gameObject.SetActive(false);
        ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.DIE_OUT), this);
        if (isDieRemove)StartCoroutine(IEDieDestory(2f));
    }

    public IEnumerator IEDieDestory(float time)
    {
        //Debug.Log("time   "+time);
        //yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(time);
        //playerRigidbody2D.velocity = Vector2.zero;
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
        if(this.tag == "Player")
        {
            ObjectEventDispatcher.dispatcher.dispatchEvent(new UEvent(EventTypeName.GAME_OVER), this);
        }
        DestroyImmediate(this, true);
    }


    public void GetPause(float pauseTime = 0.1f)
    {
        DBBody.animation.timeScale = 0.03f;
        if(_theTimer) _theTimer.GetStopByTime(pauseTime);
    }


    // Update is called once per frame
    void Update () {
        GetUpdate();
    }

    protected void GetUpdate()
    {
        CurrentAcName = DBBody.animation.lastAnimationName;
        //脚下的烟幕
        Yanmu();

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


        if (_theTimer != null && !_theTimer.IsPauseTimeOver())
        {
            return;
        }
        else
        {
            DBBody.animation.timeScale = 1;
        }


        if (roleDate.isBeHiting)
        {
            GetBeHit();
            return;
        }

        if (isBackUping)
        {
            GetBackUping();
            return;
        }


        //print(_theTimer);


        if (isDodgeing)
        {
            Dodge1();
            return;
        }



        ControlSpeed();



        InAir();



        if (isAcing)
        {
            GetAcMsg(_acName);
            return;
        }

       

        if (isJumping)
        {
            Jump();
        }

        if (isAtking)
        {
            Atk();
        }

        //print(this.tag);
        //if (this.tag != "AirEnemy") print("hi");
        if (!roleDate.isBeHiting && !isInAiring && !isDowning && !isRunLefting && !isRunRighting && !isJumping && !isAtking && !isDodgeing && !isAtkYc)
        {
            //if (this.tag != "AirEnemy") print("stand"+"  ? "+isRunLefting);
            Stand();
        }
    }

    public virtual void Testss()
    {
        //print("Gamebody!");
    }

    public virtual void HasBeHit(float chongjili = 0)
    {
        if (DBBody.animation.lastAnimationName == DODGE1) return;
        ResetAll();
        roleDate.isBeHiting = true;
        if (isInAiring)
        {
            if (DBBody.animation.HasAnimation(BEHITINAIR))
            {
                DBBody.animation.GotoAndPlayByFrame(BEHITINAIR, 0, 1);
                return;
            }
        }
        DBBody.animation.GotoAndPlayByFrame(BEHIT, 0, 1);
    }

    protected virtual void GetBeHit()
    {
        if((DBBody.animation.lastAnimationName == BEHIT|| DBBody.animation.lastAnimationName == BEHITINAIR) && DBBody.animation.isCompleted) {
            roleDate.isBeHiting = false;
            if (IsGround) GetStand();
        }
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
            _yanmu2.Play();
            return;
        }


        //print("x " + GetComponent<Rigidbody2D>().velocity.x);

        if (IsHitMQWall && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 3) {
            
            _yanmu2.Play();
            if(GetComponent<RoleAudio>()) GetComponent<RoleAudio>().PlayHitWallDown(true);
        }
        else
        {
            _yanmu2.Stop();
            if (GetComponent<RoleAudio>()) GetComponent<RoleAudio>().PlayHitWallDown(false);
        }
    }



    //1.动作名 调用 函数  动作名 和 特效名字
    //2.获取VO信息  包含 位置 相对位置（给每个角色基本信息加宽高补偿 缩放补偿） 是否需要补偿释放（有些技能特效不需要 寻找相对位置和缩放）
    //2.判断是否包含动作 有一类技能无动作可以直接释放
    //3.




    //一个是延迟 一个是连击的给出时间  连击给出时间还是得在延迟内   给个时间差做减法 在减法内 不能控制 超过减法可以接连招 跑完回复站立


    //动作控制流程
    int acNums = 0;
    //是否在动作延迟
    bool isYanchi = false;
    //动作滞留延迟
    int yanchiNum = 10;
    int yanchiMaxNum = 20;
    //延迟行动尺度
    public int canMoveNums = 100;
    protected bool isAcing = false;
	string _acName;
    public string GetAcMsg(string acName)
    {

        //获取技能VO
        //GetSkillVOByName(acName);
        //print("------------------------------------------------------------------  "+acName);
        if (!DBBody.animation.HasAnimation(acName)) return null;
        //print("------------------------------------------------------------------22  " + acName);
        
        if (DBBody.animation.lastAnimationName == acName && DBBody.animation.isCompleted)
        {
            acNums++;
            isYanchi = true;
            if (acNums > yanchiNum)
            {
                //可以切换招式
            }

            //这里的延迟 将来会改到 vo里面直接取  招式切换也是 这个Ac主要针对 非攻击Ac
            if (acNums > yanchiMaxNum) {
                isAcing = false;
                isYanchi = false;
                _acName = null;
                return "completed";
            }
            return "acYanChi";
        }

        if (DBBody.animation.HasAnimation(acName)&&DBBody.animation.lastAnimationName!=acName)
        {
            DBBody.animation.GotoAndPlayByFrame(acName, 0, 1);
            acNums = 0;
            isAcing = true;
			_acName = acName;
            return "start";
        }
        
        if(DBBody.animation.lastAnimationName == acName && DBBody.animation.isPlaying)
        {
			//print("acName "+_acName);
			return "playing_"+acNums;
        }
        
        return null;
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
    bool isAtk = false;
    public bool isAtking = false;
    string[] atkMsg;
    VOAtk vOAtk;
    Dictionary<string, string>[] atkZS;
    
    public void GetAtk(string atkName = null)
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
                vOAtk.GetVO(GetDateByName.GetInstance().GetDicSSByName(atkName, DataZS.GetInstance()));
                DBBody.animation.GotoAndPlayByFrame(vOAtk.atkName, 0, 1);
            }

            MoveVX(vOAtk.xF);
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

  


    //显示动作特效 龙骨的侦听事件
    private void ShowACTX(string type, EventObject eventObject)
    {
        //print("type:  "+type);
        if (type == EventObject.SOUND_EVENT)
        {
            //print("eventName:  "+eventObject.name);
            if (eventObject.name == "runG1") return;
            //if(eventObject.name=="run1"|| eventObject.name == "run2") _yanmu.Play();
            GetComponent<RoleAudio>().PlayAudio(eventObject.name);
        }


        if (type == EventObject.FRAME_EVENT)
        {
            if (eventObject.name == "ac")
            {
                //print("vOAtk.atkName   "+ vOAtk.txName);
                if (isAcing)
                {
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(jn.TXName, true);
                }
                else {
                    GetComponent<ShowOutSkill>().ShowOutSkillByName(vOAtk.txName);
                }
                
                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_002");
                //GetComponent<ShowOutSkill>().ShowOutSkillByName("dg_fk");
            }
        }
        
    }
    
    float jisuqi = 0;
    float yanchi = 0;

    //动作延迟  在延迟内 将isAtk=false 可以控制人物 而不会一直在动作尾不受控制
    protected bool isAtkYc = false;
    

    protected void AtkReSet()
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

    void Atk()
    {
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isPlaying)
        {
            //print(DBBody.animation.lastAnimationState);
        }
        if (DBBody.animation.lastAnimationName == vOAtk.atkName && DBBody.animation.isCompleted)
        {
            jisuqi = 0;
            yanchi++;
            if (yanchi > vOAtk.yanchi - canMoveNums) {
                isAtk = false;
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

}

